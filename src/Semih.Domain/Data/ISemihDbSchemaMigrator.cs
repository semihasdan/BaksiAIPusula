using System.Threading.Tasks;

namespace Semih.Data;

public interface ISemihDbSchemaMigrator
{
    Task MigrateAsync();
}
