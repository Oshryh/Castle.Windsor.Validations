using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Castle.Windsor.Validations
{
    /// <summary>
    /// A static class containing IWindsorContainer helper extension methods for validating dependencies.
    /// </summary>
    public class ContainerValidationsProvider
    {
        private ContainerValidationsProvider()
        { }

        private static readonly Lazy<ContainerValidationsProvider> LazyInstance = 
            new Lazy<ContainerValidationsProvider>(() => new ContainerValidationsProvider());

        public static ContainerValidationsProvider Instance => LazyInstance.Value;

        /// <summary>
        /// Validates all the registered services/dependencies in a container can be resolved, and throws a detailed
        /// exception with missing dependencies or other issues preventing resolution of dependencies.
        /// </summary>
        /// <param name="container">An IWindsorContainer to validate</param>
        public async Task ValidateAllDependenciesResolvableAsync(IWindsorContainer container)
        {
            await Task.Delay(10).ConfigureAwait(false);

            var containerServices = container.Kernel.GetAssignableHandlers(typeof(object))
                .OrderBy(handler => handler.ComponentModel.Dependencies.Count)
                .Where(handler => !handler.ComponentModel.RequiresGenericArguments)
                .SelectMany(handler => handler.ComponentModel.Services);

            var servicesExceptions = new Dictionary<Type, Exception>();

            foreach (var service in containerServices)
            {
                try
                {
                    if (container.Resolve(service) == null)
                        throw new Exception(
                            $"The service {service.FullName} was successfully resolved, but was returned as null.");
                }
                catch (Exception ex)
                {
                    servicesExceptions.Add(service, ex);
                }
            }

            if (servicesExceptions.Any())
                throw GetReadableException(servicesExceptions);
        }

        private Exception GetReadableException(Dictionary<Type, Exception> servicesExceptions)
        {
            var errorMessages = servicesExceptions
                .Select(p => GetReadableServiceResolutionException(p.Key, p.Value))
                .Aggregate((p1, p2) => p1 + Environment.NewLine + p2);

            var aggregatedExceptionErrorMessage = $"{servicesExceptions.Count} IOC services were not resolved:{Environment.NewLine}{errorMessages}";

            return new AggregateException(aggregatedExceptionErrorMessage, servicesExceptions.Values);
        }

        private string GetReadableServiceResolutionException(Type service, Exception resolutionException)
        {
            return
                "-------------------------------------------------------------------------------------------------"
                + Environment.NewLine
                + $"Service: {service.FullName}"
                + Environment.NewLine
                + $"Resolution exception: {resolutionException.Message}"
                + Environment.NewLine;
        }

    }
}
