using Group_Expense_Tracker.Server.Data.Entities;
using Group_Expense_Tracker.Server.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Group_Expense_Tracker.Server.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly TrackerDbContext _context;
        public MemberRepository(TrackerDbContext context)
        {
            _context = context;
        }
        public async Task<Member> CreateAsync(int groupId, Member member)
        {
            member.GroupId = groupId;
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<Member?> FindByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<List<Member>> GetAll()
        {
            return await _context.Members.ToListAsync();
        }

        public bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }

        public async Task<Member?> RemoveAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if(member == null)
            {
                return null;
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<Member?> UpdateAsync(int id, Member member)
        {

            var existingMember = await _context.Members.FindAsync(id);

            if(existingMember == null) { return null; }

            existingMember.Name = member.Name;
            existingMember.Surname = member.Surname;
            existingMember.Debt = member.Debt;
            existingMember.GroupId = member.GroupId;
            existingMember.Group = member.Group;

            await _context.SaveChangesAsync();

            return existingMember;
        }
    }
}
