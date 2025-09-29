using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Semih.Conversations;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Semih.Conversations
{
    public class ConversationAppService : ApplicationService, IConversationAppService
    {
        private readonly IRepository<Conversation, Guid> _conversationRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        private readonly ICurrentUser _currentUser;

        public ConversationAppService(
            IRepository<Conversation, Guid> conversationRepository,
            IRepository<IdentityUser, Guid> userRepository,
            ICurrentUser currentUser)
        {
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<ConversationDto> CreateAsync(CreateConversationDto input)
        {
            Guid? doctorId = null;
            if (_currentUser.Id.HasValue)
            {
               var currentUserEntity = await _userRepository.FindAsync(_currentUser.Id.Value);
               if (currentUserEntity?.CreatorId != null)
               {
                   doctorId = currentUserEntity.CreatorId.Value;
               }
            }
            var conversation = new Conversation(
                GuidGenerator.Create(),
                input.CustomerId,
                input.CustomerQuestion,
                input.AiResponse,
                doctorId
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
            
            if (input.DoctorId.HasValue)
            {
                queryable = queryable.Where(x => x.DoctorId == input.DoctorId);
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
            // Get the patient/customer user information
            var customer = await _userRepository.FindAsync(conversation.CustomerId);
            
            string doctorName = null;
            Guid? doctorId = conversation.DoctorId; // Use the stored DoctorId from the conversation
            
            // Get doctor information if DoctorId is available
            if (doctorId.HasValue)
            {
                var doctor = await _userRepository.FindAsync(doctorId.Value);
                if (doctor != null)
                {
                    doctorName = $"{doctor.Name} {doctor.Surname}";
                }
            }

            
            return new ConversationDto
            {
                Id = conversation.Id,
                CustomerId = conversation.CustomerId,
                CustomerName = customer != null ? $"{customer.Name} {customer.Surname}" : "Unknown Customer",
                CustomerQuestion = conversation.CustomerQuestion,
                AiResponse = conversation.AiResponse,
                DoctorNote = conversation.DoctorNote,
                IsCompleted = conversation.IsCompleted,
                ConversationStartTime = conversation.ConversationStartTime,
                ConversationEndTime = conversation.ConversationEndTime,
                DoctorId = doctorId,
                DoctorName = doctorName,
            };
        }
    }
}

