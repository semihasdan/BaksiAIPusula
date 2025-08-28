using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Semih.Conversations
{
    public class Conversation : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; set; } // Links to Tenant (Customer)
        public string CustomerQuestion { get; set; }
        public string AiResponse { get; set; }
        public string? DoctorNote { get; set; }
        public bool IsCompleted { get; set; } // True when doctor has added their note
        public DateTime ConversationStartTime { get; set; }
        public DateTime? ConversationEndTime { get; set; }

        protected Conversation()
        {
            // For EF Core
        }

        public Conversation(
            Guid id,
            Guid customerId,
            string customerQuestion,
            string aiResponse) : base(id)
        {
            CustomerId = customerId;
            CustomerQuestion = customerQuestion;
            AiResponse = aiResponse;
            IsCompleted = false;
            ConversationStartTime = DateTime.UtcNow;
        }

        public void AddDoctorNote(string doctorNote)
        {
            DoctorNote = doctorNote;
            IsCompleted = true;
            ConversationEndTime = DateTime.UtcNow;
        }
    }
}