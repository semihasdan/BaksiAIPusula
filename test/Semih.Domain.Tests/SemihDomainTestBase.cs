using Volo.Abp.Modularity;

namespace Semih;

/* Inherit from this class for your domain layer tests. */
public abstract class SemihDomainTestBase<TStartupModule> : SemihTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
