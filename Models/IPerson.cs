namespace Models
{
    public interface IPerson
	{
        public Guid PersonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<IComment> Comments { get; set; }
    }
}

