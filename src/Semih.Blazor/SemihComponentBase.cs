using Semih.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Semih.Blazor;

public abstract class SemihComponentBase : AbpComponentBase
{
    protected SemihComponentBase()
    {
        LocalizationResource = typeof(SemihResource);
    }
}
