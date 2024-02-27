using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModels
{
    [Table("Attraction")]
    [Index(nameof(AttractionTitle), nameof(AttractionDescription), nameof(strCategory))]
    public class csAttractionDbM : csAttraction, ISeed<csAttractionDbM>, IEquatable<csAttractionDbM>
    {
        [Key]
        public override Guid AttractionId { get; set; }

        public override string AttractionTitle { get; set; }

        public override string AttractionDescription { get; set; }

        [NotMapped]
        public override enAttractionCategory Category { get; set; }

        public virtual string strCategory
        {
            get => Category.ToString();
            set { }
        }

        #region location mapping
        [JsonIgnore]
        public virtual Guid LocationId { get; set; }

        [NotMapped]
        public override ILocation Location
        {
            get => LocationDbM;
            set => new NotImplementedException();
        }

        [JsonIgnore]
        [ForeignKey("LocationId")]
        public virtual csLocationDbM LocationDbM { get; set; } = null;
        #endregion

        #region comment mapping
        [NotMapped]
        public override List<IComment> Comments
        {
            get => CommentsDbM?.ToList<IComment>();
            set => new NotImplementedException();
        }

        [JsonIgnore]
        [ForeignKey("CommentId")]
        public virtual List<csCommentDbM> CommentsDbM { get; set; } = new List<csCommentDbM>();
        #endregion

        #region constructors
        public csAttractionDbM UpdateFromDTO(csAttractionCUdto org)
        {
            if (org == null) return null;
            AttractionTitle = org.AttractionTitle;
            AttractionDescription= org.AttractionDescription;
            Category = org.Category;
            
            return this;
        }

        public csAttractionDbM() { }
        public csAttractionDbM(csAttractionCUdto org)
        {
            AttractionId = Guid.NewGuid();
            UpdateFromDTO(org);
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(csAttractionDbM other) => (other != null) ? ((AttractionTitle, AttractionDescription, Category) ==
            (other.AttractionTitle, other.AttractionDescription, other.Category)) : false;

        public override bool Equals(object obj) => Equals(obj as csAttractionDbM);
        public override int GetHashCode() => (AttractionTitle, AttractionDescription, Category).GetHashCode();
        #endregion

        #region Seeding
        public override csAttractionDbM Seed(csSeedGenerator sgen)
        {
            base.Seed(sgen);
            return this;
        }
        #endregion
    }
}
