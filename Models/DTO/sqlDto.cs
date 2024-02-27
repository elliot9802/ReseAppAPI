using System;
namespace Models.DTO
{
    public class AttractionsWithoutCommentsDto
    {
        public Guid AttractionId { get; set; }
        public string AttractionTitle { get; set; }
        public string AttractionDescription { get; set; }
        public string strCategory { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Comment { get; set; }
    }

    public class AttractionDetailsDto
    {
        public string AttractionTitle { get; set; }
        public string AttractionDescription { get; set; }
        public string strCategory { get; set; }
        public string Comment { get; set; }
    }

    public class UsersAndCommentsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
    }

    public class DbInfoDto
    {
        public Guid AttractionId { get; set; }
        public Guid CommentId { get; set; }
        public Guid LocationId { get; set; }
        public Guid PersonId { get; set; }
    }

    public class CommentAttractionRequest
    {
        public Guid AttractionId { get; set; }
        public Guid PersonId { get; set; }
        public string Comment { get; set; }
    }

    public class sqlAllInfoDto
    {
        public List<AttractionsWithoutCommentsDto> AttractionsWithoutComments { get; set; } = new List<AttractionsWithoutCommentsDto>();
        public List<AttractionDetailsDto> AttractionDetails { get; set; } = new List<AttractionDetailsDto>();
        public List<UsersAndCommentsDto> UsersAndComments { get; set; } = new List<UsersAndCommentsDto>();
        public List<DbInfoDto> DbInfo { get; set; } = new List<DbInfoDto>();
    }
}
