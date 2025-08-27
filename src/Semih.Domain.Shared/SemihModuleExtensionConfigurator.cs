﻿﻿﻿﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Semih;

public static class SemihModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        /* You can change max lengths for properties of the
         * entities defined in the modules used by your application.
         *
         * Example: Change user and role name max lengths

           AbpUserConsts.MaxNameLength = 99;
           IdentityRoleConsts.MaxNameLength = 99;

         * Notice: It is not suggested to change property lengths
         * unless you really need it. Go with the standard values wherever possible.
         *
         * If you are using EF Core, you will need to run the add-migration command after your changes.
         */
    }

    private static void ConfigureExtraProperties()
    {
        /* You can configure extra properties for the
         * entities defined in the modules used by your application.
         *
         * This class can be used to define these extra properties
         * with a high level, easy to use API.
         *
         * Example: Add a new property to the user entity of the identity module

           ObjectExtensionManager.Instance.Modules()
              .ConfigureIdentity(identity =>
              {
                  identity.ConfigureUser(user =>
                  {
                      user.AddOrUpdateProperty<string>( //property type: string
                          "SocialSecurityNumber", //property name
                          property =>
                          {
                              //validation rules
                              property.Attributes.Add(new RequiredAttribute());
                              property.Attributes.Add(new StringLengthAttribute(64) {MinimumLength = 4});

                              //...other configurations for this property
                          }
                      );
                  });
              });

         * See the documentation for more:
         * https://docs.abp.io/en/abp/latest/Module-Entity-Extensions
         */

        // Configure Identity User extra properties
        ObjectExtensionManager.Instance.Modules()
            .ConfigureIdentity(identity =>
            {
                identity.ConfigureUser(user =>
                {
                    user.AddOrUpdateProperty<int>(
                        "Age",
                        property =>
                        {
                            property.Attributes.Add(new RangeAttribute(0, 150));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );

                    user.AddOrUpdateProperty<double>(
                        "Height",
                        property =>
                        {
                            property.Attributes.Add(new RangeAttribute(0.5, 3.0));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );

                    user.AddOrUpdateProperty<double>(
                        "Weight",
                        property =>
                        {
                            property.Attributes.Add(new RangeAttribute(1, 500));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );

                    user.AddOrUpdateProperty<string>(
                        "Note",
                        property =>
                        {
                            property.Attributes.Add(new StringLengthAttribute(1000));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );
                });
            });

        // Configure Tenant Management extra properties
        ObjectExtensionManager.Instance.Modules()
            .ConfigureTenantManagement(tenantManagement =>
            {
                tenantManagement.ConfigureTenant(tenant =>
                {
                    tenant.AddOrUpdateProperty<int>(
                        "Age",
                        property =>
                        {
                            property.Attributes.Add(new RangeAttribute(0, 150));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );

                    tenant.AddOrUpdateProperty<double>(
                        "Height",
                        property =>
                        {
                            property.Attributes.Add(new RangeAttribute(0.5, 3.0));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );

                    tenant.AddOrUpdateProperty<double>(
                        "Weight",
                        property =>
                        {
                            property.Attributes.Add(new RangeAttribute(1, 500));
                            property.UI.OnTable.IsVisible = true;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );

                    tenant.AddOrUpdateProperty<string>(
                        "Note",
                        property =>
                        {
                            property.Attributes.Add(new StringLengthAttribute(1000));
                            property.UI.OnTable.IsVisible = false;
                            property.UI.OnCreateForm.IsVisible = true;
                            property.UI.OnEditForm.IsVisible = true;
                        }
                    );
                });
            });
    }
}
