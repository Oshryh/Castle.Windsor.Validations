using System;
using System.Collections.Generic;
using System.Linq;

namespace Castle.Windsor.ContainerValidator
{
    public class ContainerDependenciesValidator
    {

        private readonly IWindsorContainer _container;

        public ContainerDependenciesValidator(IWindsorContainer container)
        {
            _container = container;
        }

        public void ValidateAllDependenciesResolvable()
        {
            var containerServices = _container.Kernel.GetHandlers()
                .OrderBy(p => p.ComponentModel.Dependencies.Count)
                .SelectMany(handler => handler.ComponentModel.Services);

            var exceptions = new List<Exception>();

            foreach (var service in containerServices)
            {
                try
                {
                    if (_container.Resolve(service) == null)
                        throw new Exception(
                            $"The service {service.FullName} was successfully resolved, but was returned as null.");
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
                throw GetReadableException(exceptions);
        }

        private static Exception GetReadableException(List<Exception> exceptions)
        {
            if (exceptions.Count == 1)
                return exceptions.First();

            var lineBreak = Environment.NewLine
                + "-------------------------------------------------------------------------------------------------"
                + Environment.NewLine;

            var errorMessages = exceptions
                .Select(p => p.Message)
                .Aggregate((p1, p2) => p1 + lineBreak + p2);

            var aggregatedExceptionErrorMessage = $"{exceptions.Count} IOC services were not resolved:{Environment.NewLine}{errorMessages}";

            return new AggregateException(aggregatedExceptionErrorMessage, exceptions);
        }

    }
}
