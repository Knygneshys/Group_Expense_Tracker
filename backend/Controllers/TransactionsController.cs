using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend.Data.Entities;
using backend.Data.Entities;
using Backend.Data.Interfaces;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transRepo;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transRepo = transactionRepository;
        }

        // GET
        [HttpGet("{groupId:int}")]
        public async Task<ActionResult<List<Transaction>>> GetAllByGroupId(int groupId)
        {
            var transactions = await _transRepo.GetAllFromGroup(groupId);
            if (transactions == null) { return NotFound(); }

            return transactions;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
        {
            var t = await _transRepo.CreateAsync(transaction);
            if (t == null) { return BadRequest(); }

            return t;
        }
    }
}
