using Volo.Abp.Settings;

namespace Semih.Settings;

public class SemihSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SemihSettings.MySetting1));
    }
}
