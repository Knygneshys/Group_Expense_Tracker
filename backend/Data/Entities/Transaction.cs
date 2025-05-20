using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            Recipients = new List<TransactionRecipient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SenderId { get; set; }
        public decimal Amount { get; set; }
        public char SplitType { get; set; }
        public string Date { get; set; } = DateTime.Today.ToShortDateString();
        public List<TransactionRecipient> Recipients { get; set; }

        public Transaction(int id, int groupId, int senderId, decimal amount, char splitType, List<TransactionRecipient> recipients)
        {
            Id = id;
            GroupId = groupId;
            SenderId = senderId;
            Amount = amount;
            SplitType = splitType;
            Recipients = recipients;
        }

        public Transaction(int groupId, int senderId, decimal amount, char splitType, List<TransactionRecipient> recipients)
        {
            GroupId = groupId;
            SenderId = senderId;
            Amount = amount;
            SplitType = splitType;
            Recipients = recipients;
        }
    }
}
