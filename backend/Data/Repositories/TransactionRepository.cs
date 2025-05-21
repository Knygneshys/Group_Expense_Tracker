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
                    if (transaction.Recipients.Sum(rt => rt.Payment) != 100) { return null; }
                    foreach (TransactionRecipient recipient in transaction.Recipients)
                    {
                        amount = transaction.Amount * recipient.Payment / 100;
                        await ReadjustDebt(recipient.RecipientId, amount, transaction.SenderId);
                        recipient.Payment = amount;
                    }
                    break;
                case ('D'):
                    bool sumsAreEqual = transaction.Recipients.Sum(rt => rt.Payment) == transaction.Amount;
                    if (!sumsAreEqual) { return null; }
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
            if (member != null && senderId == 0)
            {
                member.Debt += amount;
                await _context.SaveChangesAsync();
            }
            else if (member != null && recipientId != senderId && member.Debt < 0)
            {
                member.Debt += amount;
                if (member.Debt > 0) { member.Debt = 0; }
                await _context.SaveChangesAsync();
            }
            else if (senderId != 0 && recipientId == 0) // if members sent money to the user
            {
                Member? m = await _context.Members.FindAsync(senderId);
                if (m != null)
                {
                    m.Debt -= amount;
                }
                await _context.SaveChangesAsync();
            }

        }


        public async Task<List<Transaction>?> GetAllFromGroup(int groupId)
        {
            return await _context.Transactions.Where(t => t.GroupId == groupId).Include(t => t.Recipients).ToListAsync();
        }
    }
}
