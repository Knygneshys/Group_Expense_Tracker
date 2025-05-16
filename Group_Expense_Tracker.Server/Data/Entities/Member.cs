namespace Group_Expense_Tracker.Server.Data.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Debt { get; set; }

        
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public Member(int id, string name, string surname, float debt, int groupId)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Debt = debt;
            GroupId = groupId;
        }

        // Parameterless constructor for EF Core
        public Member() { }
    }
}
