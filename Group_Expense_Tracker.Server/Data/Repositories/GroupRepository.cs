using Group_Expense_Tracker.Server.Data.Entities;
using Group_Expense_Tracker.Server.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Group_Expense_Tracker.Server.Data.Repositories
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
            return await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Group>> GetAll()
        {
            return await _context.Groups.Include(g => g.Members).ToListAsync();
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
