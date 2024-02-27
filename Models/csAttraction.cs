namespace Models
{
    public class csAttraction : IAttraction, ISeed<csAttraction>, IEquatable<csAttraction>
    {
        public virtual Guid AttractionId { get; set; }
        public virtual bool Seeded { get; set; } = false;

        public virtual string AttractionTitle { get; set; }

        public virtual string AttractionDescription { get; set; }

        public virtual enAttractionCategory Category { get; set; }
        public virtual ILocation Location { get; set; } = null;
        public virtual List<IComment> Comments { get; set; } = new List<IComment>();

        #region constructors
        public csAttraction() { }
        public csAttraction(csAttraction org)
        {
            Seeded = org.Seeded;
            AttractionId = org.AttractionId;
            AttractionTitle = org.AttractionTitle;
            AttractionDescription = org.AttractionDescription;
            Category = org.Category;
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(csAttraction other) => (other != null) ? ((AttractionTitle, AttractionDescription, Category) ==
            (other.AttractionTitle, other.AttractionDescription, other.Category)) : false;

        public override bool Equals(object obj) => Equals(obj as csAttraction);
        public override int GetHashCode() => (AttractionTitle, AttractionDescription, Category).GetHashCode();
        #endregion

        #region Seeding
        public virtual csAttraction Seed(csSeedGenerator sgen)
        {
            AttractionId = Guid.NewGuid();
            AttractionTitle = sgen.AttractionTitle;
            AttractionDescription = sgen.Description.Description;
            Category = sgen.FromEnum<enAttractionCategory>();
            Seeded = true;

            return this;
        }
        #endregion
    }
}
