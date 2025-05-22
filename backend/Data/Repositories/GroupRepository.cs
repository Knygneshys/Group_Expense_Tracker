using backend.Data.Entities;
using backend.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly TrackerDbContext _context;
        public GroupRepository(TrackerDbContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateAsync(Group group)
        {
            _context.Groups.Add(@group);
            await _context.SaveChangesAsync();

            return group;
        }

        public async Task<Group?> FindByIdAsync(int id)
        {
            Group? group = await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(i => i.Id == id);
            if(group != null && group.Members != null)
            {
                group.Members = group.Members.Where(mem => mem.isDeleted == false).OrderByDescending(mem => mem.Debt).ToList();
            }

            return group;
        }

        public async Task<List<Group>> GetAll()
        {
            List<Group> groups = await _context.Groups.ToListAsync();

            foreach(Group group in groups)
            {
                group.Members = group.Members.Where(mem => mem.isDeleted == false).ToList();
            }

            return groups;
        }

        public async Task<Group> GetGroupWithoutMembers(int id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(i => i.Id == id);

            return group;
        }

        public async Task<List<Group>> GetGroupsFromPage(int pageNr)
        {
            const int groupCountInPage = 9;

            List<Group> groups = await _context.Groups.Skip(groupCountInPage * pageNr).Take(groupCountInPage).ToListAsync();

            if(groups == null)
            {
                return null;
            }

            return groups;
        }

        public async Task<decimal> GetGroupDebt(int id)
        {
            return await _context.Members
                .Where(member => member.GroupId == id && member.isDeleted == false)
                .SumAsync(member => member.Debt);
        }

        public bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }

        public async Task<Group?> RemoveAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return null;
            }

            if(await _context.Members.FindAsync(id) != null)
            {
                return null;
            }

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return @group;
        }

        public async Task<Group?> UpdateAsync(int id, Group group)
        {
            var existingGroup = await _context.Groups.FindAsync(id);
            if(existingGroup == null)
            {
                return null;
            }

            existingGroup.Name = group.Name;
            existingGroup.Members = group.Members;

            await _context.SaveChangesAsync();

            return existingGroup;
        }
    }
}
