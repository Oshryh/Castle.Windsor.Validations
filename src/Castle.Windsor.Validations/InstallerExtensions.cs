using System.Threading.Tasks;
using Castle.MicroKernel.Registration;

namespace Castle.Windsor.Validations
{
    public static class InstallerExtensions
    {

        public static async Task ValidateAllDependenciesResolvableAsync(this IWindsorInstaller installer)
        {
            using (var container = new WindsorContainer())
            {
                container.Install(installer);
                await container.ValidateAllDependenciesResolvableAsync().ConfigureAwait(false);
            }
        }

        public static void ValidateAllDependenciesResolvable(this IWindsorInstaller installer)
        {
            using (var container = new WindsorContainer())
            {
                container.Install(installer);
                container.ValidateAllDependenciesResolvable();
            }
        }
    }
}
