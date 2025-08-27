using Microsoft.Extensions.Localization;
using Semih.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Semih.Blazor;

[Dependency(ReplaceServices = true)]
public class SemihBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SemihResource> _localizer;

    public SemihBrandingProvider(IStringLocalizer<SemihResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
