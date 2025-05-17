
using System.ComponentModel.DataAnnotations;

namespace Group_Expense_Tracker.Server.Data.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide member name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide member surname!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please provide member debt amount!")]
        public float Debt { get; set; } = 0;

        [Required(ErrorMessage = "Please provide which group the member belongs to!")]
        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public Member(int id, string name, string surname, float debt, int groupId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Debt = debt;
            GroupId = groupId;
        }

        public Member() { }
    }
}
