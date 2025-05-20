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
