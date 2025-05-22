using backend.Data.Entities;

namespace backend.Data.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> FindByIdAsync(int id);
        Task<List<Member>> GetAll();
        Task<Member?> RemoveAsync(int id);
        Task<Member> CreateAsync( Member member);
        bool MemberExists(int id);
        Task<Member?> UpdateAsync(int id, Member member);
        Task<List<Member>> GetAllByGroupId(int groupId);
        Task<List<Member>> GetAllByGroupIdAlongWithDeleted(int groupId);
    }
}
