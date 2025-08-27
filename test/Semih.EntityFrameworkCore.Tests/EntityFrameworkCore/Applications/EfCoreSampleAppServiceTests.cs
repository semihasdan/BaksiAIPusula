using Semih.Samples;
using Xunit;

namespace Semih.EntityFrameworkCore.Applications;

[Collection(SemihTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<SemihEntityFrameworkCoreTestModule>
{

}
