using System.Threading.Tasks;
using Semih.Localization;
using Semih.Permissions;
using Semih.MultiTenancy;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.Identity.Blazor;
using Semih.Permissions;
using Volo.Abp.Users;

namespace Semih.Blazor.Menus;

public class SemihMenuContributor : IMenuContributor
{
    private readonly IPermissionChecker _permissionChecker;

    public SemihMenuContributor(IPermissionChecker permissionChecker)
    {
        _permissionChecker = permissionChecker;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<SemihResource>();
        
        // Ana sayfa menüsü her zaman görünür
        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                SemihMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );
        
        // Doktor menüsü için izin kontrolü
        if (await _permissionChecker.IsGrantedAsync(SemihPermissions.Doctors.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    "Semih.Doctors",
                    "Doctor Menu",
                    url: "/doctors",
                    icon: "fa fa-user-md"
                )
            );
        }

        // Yönetim menüsü
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;
    
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);
    }
}
