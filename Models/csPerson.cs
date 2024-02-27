namespace Models
{
    public class csPerson : IPerson, ISeed<csPerson>
    {
        public virtual Guid PersonId { get; set; }
        public bool Seeded { get; set; } = false;
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        //One Person can have many Comments
        public virtual List<IComment> Comments { get; set; } = new List<IComment>();

        #region constructors
        public csPerson() { }
        public csPerson(csPerson org)
        {
            PersonId = org.PersonId;
            FirstName = org.FirstName;
            LastName = org.LastName;
            Seeded = org.Seeded;
        }
        #endregion

        #region seeding
        public virtual csPerson Seed(csSeedGenerator sgen)
        {
            PersonId = Guid.NewGuid();
            FirstName = sgen.FirstName;
            LastName = sgen.LastName;
            Seeded = true;

            return this;
        }
        #endregion
    }
}