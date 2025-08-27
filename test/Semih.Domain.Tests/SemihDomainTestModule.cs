using Volo.Abp.Modularity;

namespace Semih;

[DependsOn(
    typeof(SemihDomainModule),
    typeof(SemihTestBaseModule)
)]
public class SemihDomainTestModule : AbpModule
{

}
