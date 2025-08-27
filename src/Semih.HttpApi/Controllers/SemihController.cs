using Semih.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Semih.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SemihController : AbpControllerBase
{
    protected SemihController()
    {
        LocalizationResource = typeof(SemihResource);
    }
}
