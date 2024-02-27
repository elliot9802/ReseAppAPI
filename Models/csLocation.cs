namespace Models
{
    public class csLocation : ILocation, ISeed<csLocation>, IEquatable<csLocation>
    {
        public virtual Guid LocationId { get; set; }
        public bool Seeded { get; set; } = false;

        public virtual string StreetAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }

        public virtual List<IAttraction> Attractions { get; set; } = null;

        #region constructors
        public csLocation() { }
        public csLocation(csLocation org)
        {
            LocationId = org.LocationId;
            StreetAddress = org.StreetAddress;
            City = org.City;
            Country = org.Country;
            Seeded = org.Seeded;
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(csLocation other) => (other != null) ? ((StreetAddress, City, Country) ==
            (other.StreetAddress, other.City, other.Country)) : false;

        public override bool Equals(object obj) => Equals(obj as csLocation);
        public override int GetHashCode() => (StreetAddress, City, Country).GetHashCode();

        #endregion

        #region seeding
        public virtual csLocation Seed(csSeedGenerator sgen)
        {
            LocationId = Guid.NewGuid();
            Country = sgen.Country;
            StreetAddress = sgen.StreetAddress(Country);
            City = sgen.City(Country);
            Seeded = true;

            return this;
        }
        #endregion
    }
}
