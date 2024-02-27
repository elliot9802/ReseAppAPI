namespace Models
{
    public interface ILocation
    {
        public Guid LocationId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<IAttraction> Attractions { get; set; }
    }
}