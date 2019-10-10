using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Castle.Windsor.Validations
{
    /// <summary>
    /// A static class containing IWindsorContainer helper extension methods for validating dependencies.
    /// </summary>
    public static class ContainerExtensions
    {

        /// <summary>
        /// Validates all the registered services/dependencies in a container can be resolved, and throws a detailed
        /// exception with missing dependencies or other issues preventing resolution of dependencies.
        /// </summary>
        /// <param name="container">An IWindsorContainer to validate</param>
        public static void ValidateAllDependenciesResolvable(this IWindsorContainer container)
        {
            ValidateAllDependenciesResolvableAsync(container).Wait();
        }

        /// <summary>
        /// Validates all the registered services/dependencies in a container can be resolved, and throws a detailed
        /// exception with missing dependencies or other issues preventing resolution of dependencies.
        /// </summary>
        /// <param name="container">An IWindsorContainer to validate</param>
        public static async Task ValidateAllDependenciesResolvableAsync(this IWindsorContainer container)
        {
            await ContainerValidationsProvider.Instance.ValidateAllDependenciesResolvableAsync(container)
                .ConfigureAwait(false);
        }

    }
}
