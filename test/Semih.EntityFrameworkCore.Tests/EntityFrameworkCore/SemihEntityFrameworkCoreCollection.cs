using Xunit;

namespace Semih.EntityFrameworkCore;

[CollectionDefinition(SemihTestConsts.CollectionDefinitionName)]
public class SemihEntityFrameworkCoreCollection : ICollectionFixture<SemihEntityFrameworkCoreFixture>
{

}
