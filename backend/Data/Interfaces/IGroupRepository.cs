using backend.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        Task<decimal> GetGroupDebt(int id);
        Task<List<Group>> GetGroupsFromPage(int pageNr);
        Task<Group> GetGroupWithoutMembers(int id);
    }
}
