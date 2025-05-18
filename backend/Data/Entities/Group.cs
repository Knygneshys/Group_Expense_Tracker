using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Entities
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage ="Group name should be at least 3 letters long!")]
        public string Name { get; set; }
        public ICollection<Member>? Members { get; set; } = new List<Member>();

        public Group(int id, string name)
        {
            Id = id;
            Name = name;
        }
        // Used in testing
        public Group(string name)
        {
            Name = name;
        }

        public Group()
        {
        }
    }
}
