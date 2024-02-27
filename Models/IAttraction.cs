namespace Models
{
    public enum enAttractionCategory { Nature, History, Entertainment, Culture, Adventure };

    public interface IAttraction
    {
        public Guid AttractionId { get; set; }
        public enAttractionCategory Category { get; set; }
        public string AttractionTitle { get; set; }
        public string AttractionDescription { get; set; }
        public List<IComment> Comments { get; set; }
        public ILocation Location { get; set; }

    }
}

