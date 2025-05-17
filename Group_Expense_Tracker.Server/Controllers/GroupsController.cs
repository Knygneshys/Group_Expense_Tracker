using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group_Expense_Tracker.Server.Data;
using Group_Expense_Tracker.Server.Data.Entities;
using Group_Expense_Tracker.Server.Data.Interfaces;

namespace Group_Expense_Tracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepo;

        public GroupsController(IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _groupRepo.GetAll();
        }

        // GET: api/Groups/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var @group = await _groupRepo.FindByIdAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != @group.Id)
            {
                return BadRequest();
            }

            var updatedGroup = await _groupRepo.UpdateAsync(id, group);

            if(updatedGroup == null)
            {
                return NotFound();
            }

            return Ok(updatedGroup);
        }

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _groupRepo.CreateAsync(group);

            return CreatedAtAction("GetGroup", new { id = @group.Id }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var @group = await _groupRepo.RemoveAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _groupRepo.GroupExists(id);
        }
    }
}
