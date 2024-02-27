namespace Models.DTO
{
    public class csPersonCUdto
    {
        public Guid? PersonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Guid> CommentsId { get; set; }

        public csPersonCUdto() { }
        public csPersonCUdto(IPerson org)
        {
            PersonId = org.PersonId;
            FirstName = org.FirstName;
            LastName = org.LastName;

            CommentsId = org.Comments.Select(c => c.CommentId).ToList();
        }
    }

    public class csAttractionCUdto
    {
        public Guid? AttractionId { get; set; }

        public string AttractionTitle { get; set; }
        public string AttractionDescription { get; set; }
        public enAttractionCategory Category { get; set; }

        public Guid? LocationId { get; set; }
        public List<Guid> CommentsId { get; set; } 

        public csAttractionCUdto()
        {
        }
        public csAttractionCUdto(IAttraction org)
        {
            AttractionId = org.AttractionId;
            Category = org.Category;
            AttractionTitle = org.AttractionTitle;
            AttractionDescription = org.AttractionDescription;

            LocationId = org.Location.LocationId;
            CommentsId = org.Comments.Select(c => c.CommentId).ToList();
        }
    }

    public class csLocationCUdto
    {
        public Guid LocationId { get; set; }

        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<Guid> AttractionId { get; set; } = null;

        public csLocationCUdto()
        {
        }
        public csLocationCUdto(ILocation org)
        {
            LocationId = org.LocationId;
            StreetAddress = org.StreetAddress;
            City = org.City;
            Country = org.Country;

            AttractionId = org.Attractions?.Select(c => c.AttractionId).ToList();
        }
    }

    public class csCommentCUdto
    {
        public Guid CommentId { get; set; }

        public string Comment { get; set; }

        public virtual Guid AttractionId { get; set; }
        public virtual Guid PersonId { get; set; }



        public csCommentCUdto()
        {
        }
        public csCommentCUdto(IComment org)
        {
            CommentId = org.CommentId;
            Comment = org.Comment;

            AttractionId = org.Attraction.AttractionId;
            PersonId = org.Person.PersonId;
        }
    }

    public class csPersonCdto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class csAttractionCdto
    {
        public string AttractionTitle { get; set; }
        public string AttractionDescription { get; set; }
        public enAttractionCategory Category { get; set; }
    }
}
