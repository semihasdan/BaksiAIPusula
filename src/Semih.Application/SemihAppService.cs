using Semih.Localization;
using Volo.Abp.Application.Services;

namespace Semih;

/* Inherit your application services from this class.
 */
public abstract class SemihAppService : ApplicationService
{
    protected SemihAppService()
    {
        LocalizationResource = typeof(SemihResource);
    }
}
