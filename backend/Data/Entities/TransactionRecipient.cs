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
        public int TransactionId { get; set; }
        public int RecipientId { get; set; }
        public float Payment { get; set; }
        public Transaction? Transaction { get; set; }

        public TransactionRecipient(int transactionId, int recipientId, float payment)
        {
            TransactionId = transactionId;
            RecipientId = recipientId;
            Payment = payment;
        }
    }
}
