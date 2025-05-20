using backend.Data.Entities;
using Backend.Data.Entities;
using Backend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TrackerDbContext _context;
        public TransactionRepository(TrackerDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            decimal amount;
            switch (transaction.SplitType)
            {
                case ('E'):
                    int memberCount = transaction.Recipients.Count;
                    var senderIsUser = transaction.SenderId == 0;
                    amount = senderIsUser ? transaction.Amount / memberCount : transaction.Amount / (memberCount - 1);
                    foreach (TransactionRecipient recipient in transaction.Recipients)
                    {
                        await ReadjustDebt(recipient.RecipientId, amount, transaction.SenderId);
                    }
                    break;
                case ('P'):
                    if(transaction.Recipients.Sum(rt => rt.Payment) != 100) { return null; }
                    foreach(TransactionRecipient recipient in transaction.Recipients)
                    {
                        amount = transaction.Amount * recipient.Payment / 100;
                        await ReadjustDebt(recipient.RecipientId, amount, transaction.SenderId);
                    }
                    break;
                case ('D'):
                    foreach (TransactionRecipient recipient in transaction.Recipients)
                    {
                        await ReadjustDebt(recipient.RecipientId, recipient.Payment, transaction.SenderId);
                    }
                    break;
            }
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        private async Task ReadjustDebt(int recipientId, decimal amount, int senderId)
        {
            Member? member = await _context.Members.FindAsync(recipientId);
            if (member != null && member.Id != senderId && member.Debt < 0)
            {
                member.Debt += amount;
                if (member.Debt > 0) { member.Debt = 0; }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Transaction>?> GetAllFromGroup(int groupId)
        {
            return await _context.Transactions.Where(t => t.GroupId == groupId).ToListAsync();
        }
    }
}
