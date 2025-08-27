using Semih.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Semih.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SemihEntityFrameworkCoreModule),
    typeof(SemihApplicationContractsModule)
)]
public class SemihDbMigratorModule : AbpModule
{
}
