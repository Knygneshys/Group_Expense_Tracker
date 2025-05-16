namespace Group_Expense_Tracker.Server.Data.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Group Group { get; set; }
    }
}
