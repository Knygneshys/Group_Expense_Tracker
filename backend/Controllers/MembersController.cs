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

namespace Group_Expense_Tracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepo;
        public MembersController(IMemberRepository memberRepo)
        {
            _memberRepo = memberRepo;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _memberRepo.GetAll();
        }

        // GET: api/Members/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _memberRepo.FindByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // GET
        [HttpGet("ByGroup/{groupId:int}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembersByGroupId(int groupId)
        {
            var members = await _memberRepo.GetAllByGroupId(groupId);
            if(members == null) { return NotFound(); }

            return members;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != member.Id)
            {
                return BadRequest();
            }

            var updatedMember = await _memberRepo.UpdateAsync(id, member);

            if (updatedMember == null)
            {
                return NotFound();
            }

            return Ok(updatedMember);
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _memberRepo.CreateAsync(member);

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var member = await _memberRepo.RemoveAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _memberRepo.MemberExists(id);
        }
    }
}
