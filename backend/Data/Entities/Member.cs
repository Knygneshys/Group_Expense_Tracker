
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data.Entities
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public bool isDeleted { get; set; } = false;


        public Member(int id, string name, string surname, float debt, int groupId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Debt = debt;
            GroupId = groupId;
        }

        public Member(string name, string surname, float debt, int groupId)
        {
            Name = name;
            Surname = surname;
            Debt = debt;
            GroupId = groupId;
        }

        public Member() { }

        public override bool Equals(object? obj)
        {
            return obj is Member member &&
                   Id == member.Id &&
                   Name == member.Name &&
                   Surname == member.Surname &&
                   Debt == member.Debt &&
                   GroupId == member.GroupId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Surname, Debt, GroupId);
        }
    }
}
