using Backend.Data.Entities;

namespace Backend.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<List<Transaction>?> GetAllFromGroup(int groupId);
    }
}
