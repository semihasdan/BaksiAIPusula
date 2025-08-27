using Volo.Abp.Modularity;

namespace Semih;

[DependsOn(
    typeof(SemihApplicationModule),
    typeof(SemihDomainTestModule)
)]
public class SemihApplicationTestModule : AbpModule
{

}
