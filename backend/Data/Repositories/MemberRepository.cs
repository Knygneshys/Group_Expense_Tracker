using backend.Data.Entities;
using backend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly TrackerDbContext _context;
        public MemberRepository(TrackerDbContext context)
        {
            _context = context;
        }
        public async Task<Member> CreateAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<Member?> FindByIdAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            return (member == null || member.isDeleted) ? null : member;
        }

        public async Task<List<Member>> GetAll()
        {
            return await _context.Members.Where(mem => mem.isDeleted == false).ToListAsync();
        }

        public async Task<List<Member>> GetAllByGroupId(int groupId)
        {
            return await _context.Members.Where(mem => mem.GroupId == groupId && mem.isDeleted == false).ToListAsync();
        }

        public bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id && e.isDeleted == false);
        }

        public async Task<Member?> RemoveAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if(member == null)
            {
                return null;
            }

            // _context.Members.Remove(member);
            member.isDeleted = true;
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<Member?> UpdateAsync(int id, Member member)
        {

            var existingMember = await _context.Members.FindAsync(id);

            if(existingMember == null || existingMember.isDeleted) { return null; }

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
