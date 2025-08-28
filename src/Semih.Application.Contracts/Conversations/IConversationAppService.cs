using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Semih.Conversations
{
    public interface IConversationAppService : IApplicationService
    {
        Task<ConversationDto> CreateAsync(CreateConversationDto input);
        Task<ConversationDto> UpdateAsync(Guid id, UpdateConversationDto input);
        Task<ConversationDto> GetAsync(Guid id);
        Task<PagedResultDto<ConversationDto>> GetListAsync(GetConversationsInput input);
        Task<PagedResultDto<ConversationDto>> GetByCustomerIdAsync(Guid customerId);
    }
}