using backend.Data.Entities;

namespace backend.Data.Interfaces
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
