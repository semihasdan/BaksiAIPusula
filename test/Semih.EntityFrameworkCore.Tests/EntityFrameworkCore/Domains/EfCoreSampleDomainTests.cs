using Semih.Samples;
using Xunit;

namespace Semih.EntityFrameworkCore.Domains;

[Collection(SemihTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SemihEntityFrameworkCoreTestModule>
{

}
