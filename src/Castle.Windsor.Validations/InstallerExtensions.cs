using System.Threading.Tasks;
using Castle.MicroKernel.Registration;

namespace Castle.Windsor.Validations
{

    /// <summary>
    /// A static class containing IWindsorInstaller helper extension methods for validating dependencies.
    /// </summary>
    public static class InstallerExtensions
    {

        /// <summary>
        /// Validates all the registered services/dependencies in an installer can be resolved, and throws a detailed
        /// exception with missing dependencies or other issues preventing resolution of dependencies.
        /// </summary>
        /// <param name="installer">An IWindsorInstaller to validate</param>
        public static async Task ValidateAllDependenciesResolvableAsync(this IWindsorInstaller installer)
        {
            using (var container = new WindsorContainer().Install(installer))
            {
                await container.ValidateAllDependenciesResolvableAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Validates all the registered services/dependencies in an installer can be resolved, and throws a detailed
        /// exception with missing dependencies or other issues preventing resolution of dependencies.
        /// </summary>
        /// <param name="installer">An IWindsorInstaller to validate</param>
        public static void ValidateAllDependenciesResolvable(this IWindsorInstaller installer)
        {
            ValidateAllDependenciesResolvableAsync(installer).Wait();
        }
    }
}
