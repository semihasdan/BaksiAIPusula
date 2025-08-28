using System;
using Volo.Abp.Application.Dtos;

namespace Semih.Conversations
{
    public class ConversationDto : EntityDto<Guid>
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerQuestion { get; set; }
        public string AiResponse { get; set; }
        public string? DoctorNote { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime ConversationStartTime { get; set; }
        public DateTime? ConversationEndTime { get; set; }
    }

    public class CreateConversationDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerQuestion { get; set; }
        public string AiResponse { get; set; }
    }

    public class UpdateConversationDto
    {
        public string DoctorNote { get; set; }
    }

    public class GetConversationsInput : PagedAndSortedResultRequestDto
    {
        public Guid? CustomerId { get; set; }
        public bool? IsCompleted { get; set; }
    }
}