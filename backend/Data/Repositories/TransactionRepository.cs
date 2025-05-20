using Backend.Data.Entities;
using Backend.Data.Interfaces;

namespace Backend.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task<Transaction> CreateAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>?> GetAllFromGroup(int id)
        {
            throw new NotImplementedException();
        }
    }
}
