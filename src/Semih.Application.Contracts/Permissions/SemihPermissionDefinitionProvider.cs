using Semih.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Semih.Permissions;

public class SemihPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SemihPermissions.GroupName);
        
        // Doctor permissions
        var doctorsPermission = myGroup.AddPermission(SemihPermissions.Doctors.Default, L("Permission:Doctors"));
        doctorsPermission.AddChild(SemihPermissions.Doctors.Create, L("Permission:Doctors.Create"));
        doctorsPermission.AddChild(SemihPermissions.Doctors.Edit, L("Permission:Doctors.Edit"));
        doctorsPermission.AddChild(SemihPermissions.Doctors.Delete, L("Permission:Doctors.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SemihResource>(name);
    }
}
