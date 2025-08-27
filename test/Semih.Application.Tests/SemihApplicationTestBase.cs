using Volo.Abp.Modularity;

namespace Semih;

public abstract class SemihApplicationTestBase<TStartupModule> : SemihTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
