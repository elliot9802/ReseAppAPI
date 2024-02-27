namespace Models
{
    public interface IComment
    {
        public Guid CommentId { get; set; }
        public string Comment { get; set; }
        public IPerson Person { get; set; }
        public IAttraction Attraction { get; set; }
    }
}