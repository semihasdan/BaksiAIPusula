using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Semih.Conversations;
using Volo.Abp.TenantManagement;

namespace Semih.Conversations
{
    public class ConversationAppService : ApplicationService, IConversationAppService
    {
        private readonly IRepository<Conversation, Guid> _conversationRepository;
        private readonly ITenantRepository _tenantRepository;

        public ConversationAppService(
            IRepository<Conversation, Guid> conversationRepository,
            ITenantRepository tenantRepository)
        {
            _conversationRepository = conversationRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<ConversationDto> CreateAsync(CreateConversationDto input)
        {
            var conversation = new Conversation(
                GuidGenerator.Create(),
                input.CustomerId,
                input.CustomerQuestion,
                input.AiResponse
            );

            conversation = await _conversationRepository.InsertAsync(conversation);
            
            return await MapToDto(conversation);
        }

        public async Task<ConversationDto> UpdateAsync(Guid id, UpdateConversationDto input)
        {
            var conversation = await _conversationRepository.GetAsync(id);
            conversation.AddDoctorNote(input.DoctorNote);
            
            conversation = await _conversationRepository.UpdateAsync(conversation);
            
            return await MapToDto(conversation);
        }

        public async Task<ConversationDto> GetAsync(Guid id)
        {
            var conversation = await _conversationRepository.GetAsync(id);
            return await MapToDto(conversation);
        }

        public async Task<PagedResultDto<ConversationDto>> GetListAsync(GetConversationsInput input)
        {
            var queryable = await _conversationRepository.GetQueryableAsync();
            
            if (input.CustomerId.HasValue)
            {
                queryable = queryable.Where(x => x.CustomerId == input.CustomerId);
            }

            if (input.IsCompleted.HasValue)
            {
                queryable = queryable.Where(x => x.IsCompleted == input.IsCompleted);
            }

            queryable = queryable.OrderByDescending(x => x.ConversationStartTime);
            
            var totalCount = await AsyncExecuter.CountAsync(queryable);
            
            var conversations = await AsyncExecuter.ToListAsync(
                queryable
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
            );

            var conversationDtos = new List<ConversationDto>();
            foreach (var conversation in conversations)
            {
                conversationDtos.Add(await MapToDto(conversation));
            }
            
            return new PagedResultDto<ConversationDto>(
                totalCount,
                conversationDtos
            );
        }
        public async Task<PagedResultDto<ConversationDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var queryable = await _conversationRepository.GetQueryableAsync();
            queryable = queryable.Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.ConversationStartTime);
            
            var totalCount = await AsyncExecuter.CountAsync(queryable);
            var conversations = await AsyncExecuter.ToListAsync(queryable);
            
            var conversationDtos = new List<ConversationDto>();
            foreach (var conversation in conversations)
            {
                conversationDtos.Add(await MapToDto(conversation));
            }
            
            return new PagedResultDto<ConversationDto>(
                totalCount,
                conversationDtos
            );
        }

        private async Task<ConversationDto> MapToDto(Conversation conversation)
        {
            var tenant = await _tenantRepository.FindAsync(conversation.CustomerId);
            
            return new ConversationDto
            {
                Id = conversation.Id,
                CustomerId = conversation.CustomerId,
                CustomerName = tenant?.Name ?? "Unknown Customer",
                CustomerQuestion = conversation.CustomerQuestion,
                AiResponse = conversation.AiResponse,
                DoctorNote = conversation.DoctorNote,
                IsCompleted = conversation.IsCompleted,
                ConversationStartTime = conversation.ConversationStartTime,
                ConversationEndTime = conversation.ConversationEndTime
            };
        }
    }
}

