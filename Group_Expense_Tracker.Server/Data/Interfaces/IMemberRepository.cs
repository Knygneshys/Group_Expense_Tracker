using Group_Expense_Tracker.Server.Data.Entities;

namespace Group_Expense_Tracker.Server.Data.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> FindByIdAsync(int id);
        Task<List<Member>> GetAll();
        Task<Member?> RemoveAsync(int id);
        Task<Member> CreateAsync(Member member);
        bool MemberExists(int id);
        Task<Member?> UpdateAsync(int id, Member member);
    }
}
