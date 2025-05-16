using Group_Expense_Tracker.Server.Data.Entities;

namespace Group_Expense_Tracker.Server.Data.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group?> FindByIdAsync(int id);
        Task<List<Group>> GetAll();
        Task<Group?> RemoveAsync(int id);
        Task<Group> CreateAsync(Group group);
        bool GroupExists(int id);
        Task<Group?> UpdateAsync(int id, Group group);
    }
}
