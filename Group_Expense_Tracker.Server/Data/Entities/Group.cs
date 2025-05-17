using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Group_Expense_Tracker.Server.Data.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage ="Group name should be at least 3 letters long")]
        public string Name { get; set; }
        public ICollection<Member>? Members { get; set; } = new List<Member>();

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Group()
        {
        }
    }
}
