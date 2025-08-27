using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Semih;

public static class SemihDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
                /* You can add extension properties to DTOs
                 * defined in the depended modules.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Object-Extensions
                 */

                // Add extension properties to Identity User DTOs
                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<IdentityUserDto, int>("Age")
                    .AddOrUpdateProperty<IdentityUserDto, double>("Height")
                    .AddOrUpdateProperty<IdentityUserDto, double>("Weight")
                    .AddOrUpdateProperty<IdentityUserDto, string>("Note");

                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<IdentityUserCreateDto, int>("Age")
                    .AddOrUpdateProperty<IdentityUserCreateDto, double>("Height")
                    .AddOrUpdateProperty<IdentityUserCreateDto, double>("Weight")
                    .AddOrUpdateProperty<IdentityUserCreateDto, string>("Note");

                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<IdentityUserUpdateDto, int>("Age")
                    .AddOrUpdateProperty<IdentityUserUpdateDto, double>("Height")
                    .AddOrUpdateProperty<IdentityUserUpdateDto, double>("Weight")
                    .AddOrUpdateProperty<IdentityUserUpdateDto, string>("Note");
        });
    }
}