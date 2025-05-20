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
            switch (transaction.SplitType)
            {
                case ('D'):
                    foreach (TransactionRecipient recipient in transaction.Recipients)
                    {
                        Member? member = await _context.Members.FindAsync(recipient.RecipientId);
                        if(member == null) { continue; }
                        if(member.Debt < 0)
                        {
                            member.Debt += recipient.Payment;
                            await _context.SaveChangesAsync();
                        }
                    }
                    break;
            }
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<List<Transaction>?> GetAllFromGroup(int groupId)
        {
            return await _context.Transactions.Where(t => t.GroupId == groupId).ToListAsync();
        }
    }
}
