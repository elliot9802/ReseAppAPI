namespace Models
{
    public class csComment : IComment, ISeed<csComment>, IEquatable<csComment>
    {
        public virtual Guid CommentId { get; set; }

        public virtual string Comment { get; set; }
        public bool Seeded { get; set; } = false;

        //One Comment can have one Person
        public virtual IPerson Person { get; set; } = null;

        //One Comment can have one Attraction
        public virtual IAttraction Attraction { get; set; } = null;

        #region constructors
        public csComment() { }
        public csComment(UserComment userComment)
        {
            CommentId = Guid.NewGuid();
            Comment = userComment.Comment;
            Seeded = true;
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(csComment other) => (other != null) ? ((Comment, Person, Attraction) ==
            (other.Comment, other.Person, other.Attraction)) : false;

        public override bool Equals(object obj) => Equals(obj as csComment);
        public override int GetHashCode() => (Comment, Person).GetHashCode();

        #endregion

        #region seeding
        public virtual csComment Seed(csSeedGenerator sgen)
        {
            CommentId = Guid.NewGuid();
            var _comment = sgen.Comment;
            Comment = _comment.Comment;
            Seeded = true;

            return this;
        }
        #endregion
    }
}