using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Local;
using Volo.Abp.TenantManagement;
using Volo.Abp.Users;

namespace Semih.TenantManagement
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ITenantAppService), typeof(TenantAppService), typeof(CustomTenantAppService))]
    public class CustomTenantAppService : TenantAppService
    {
        private readonly ICurrentUser _currentUser;

        public CustomTenantAppService(
            ITenantRepository tenantRepository, 
            ITenantManager tenantManager,
            IDataSeeder dataSeeder,
            IDistributedEventBus distributedEventBus,
            ILocalEventBus localEventBus,
            ICurrentUser currentUser) 
            : base(tenantRepository, tenantManager, dataSeeder, distributedEventBus, localEventBus)
        {
            _currentUser = currentUser;
        }

        public override async Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantsInput input)
        {
            // Get the current user's ID
            var currentUserId = _currentUser.Id;
            
            if (!currentUserId.HasValue)
            {
                return new PagedResultDto<TenantDto>(0, new List<TenantDto>());
            }
            
            // **********************************************************************************************************
            // Check if current user is admin - if so, return all tenants
            if (_currentUser.IsInRole("admin"))
            {
                // Admin users see all tenants - use base implementation directly
                return await base.GetListAsync(input);
            }
            // **********************************************************************************************************
            
            // Non-admin users: filter by creator
            // Get all tenants first using base implementation
            var allTenantsInput = new GetTenantsInput 
            { 
                MaxResultCount = int.MaxValue, 
                SkipCount = 0,
                Filter = input.Filter
            };
            
            var allTenantsResult = await base.GetListAsync(allTenantsInput);

            // Since TenantDto doesn't have CreatorId, we need to check from the entity
            // Get the actual tenant entities to check CreatorId
            var tenantRepository = LazyServiceProvider.LazyGetRequiredService<ITenantRepository>();
            var allTenantEntities = await tenantRepository.GetListAsync();
            
            // Filter tenant entities by current user's CreatorId
            var userTenantIds = allTenantEntities
                .Where(t => t.CreatorId == currentUserId)
                .Select(t => t.Id)
                .ToList();

            // Filter the DTOs based on the filtered IDs
            var userTenants = allTenantsResult.Items
                .Where(dto => userTenantIds.Contains(dto.Id))
                .ToList();

            // Apply sorting - keep it simple with just name sorting
            if (!string.IsNullOrWhiteSpace(input.Sorting) && input.Sorting.Contains("name", StringComparison.OrdinalIgnoreCase))
            {
                userTenants = input.Sorting.Contains("desc", StringComparison.OrdinalIgnoreCase)
                    ? userTenants.OrderByDescending(t => t.Name).ToList()
                    : userTenants.OrderBy(t => t.Name).ToList();
            }
            else
            {
                userTenants = userTenants.OrderBy(t => t.Name).ToList();
            }

            // Apply pagination
            var totalCount = userTenants.Count;
            var pagedTenants = userTenants
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            return new PagedResultDto<TenantDto>(totalCount, pagedTenants);
        }
    }
}