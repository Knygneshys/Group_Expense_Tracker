using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Data.Entities;
using backend.Data.Interfaces;

namespace backend.Controllers
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

        [HttpGet("NoMem/{id:int}")]
        public async Task<ActionResult<Group>> GetGroupWithoutMembers(int id)
        {
            return await _groupRepo.GetGroupWithoutMembers(id);
        }

        // GET
        [HttpGet("Page/{pageNr:int}")]
        public async Task<ActionResult<List<Group>>> GetGroupsFromPage(int pageNr)
        {
            List<Group> groups = await _groupRepo.GetGroupsFromPage(pageNr);

            if(groups == null)
            {
                return NotFound();
            }

            return groups;
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
        // GET: api/GroupDebt/5
        [HttpGet("GroupDebt/{id:int}")]
        public async Task<ActionResult<decimal>> GetGroupDebt(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var amount = await _groupRepo.GetGroupDebt(id);

            return amount;
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

            return await _groupRepo.CreateAsync(group);
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
