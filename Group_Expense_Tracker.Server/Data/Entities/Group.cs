namespace Group_Expense_Tracker.Server.Data.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Member>? Members { get; set; }

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
