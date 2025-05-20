using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities
{
    public class TransactionRecipient
    {
        public TransactionRecipient()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // public int TransactionId { get; set; }
        public int RecipientId { get; set; }
        public decimal Payment { get; set; } = 0;
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }

        public TransactionRecipient(int recipientId)
        {
            RecipientId = recipientId;
        }

        public TransactionRecipient(int recipientId, decimal payment) : this(recipientId)
        {
            Payment = payment;
        }
    }
}
