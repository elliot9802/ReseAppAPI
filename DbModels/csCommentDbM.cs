using Models.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace DbModels
{


    [Table("Comment")]
    public class csCommentDbM : csComment, ISeed<csCommentDbM>, IEquatable<csCommentDbM>
    {
        [Key]
        public override Guid CommentId { get; set; }
        //public bool Seeded { get; set; } = true;
        //public string Comment { get; set; }

        #region Attraction mapping
        [NotMapped]
        public override IAttraction Attraction
        {
            get => AttractionDbM;
            set => new NotImplementedException();
        }

        [JsonIgnore]
        public virtual csAttractionDbM AttractionDbM { get; set; } = null;
        #endregion

        #region Person mapping
        [NotMapped]
        public override IPerson Person
        {
            get => PersonDbM;
            set => new NotImplementedException();
        }

        [JsonIgnore]
        public virtual csPersonDbM PersonDbM { get; set; } = null;
        #endregion

        #region constructors
        public csCommentDbM() : base() { }
        public csCommentDbM(UserComment userComment) : base(userComment) { }
        public csCommentDbM(csCommentCUdto org)
        {
            CommentId = Guid.NewGuid();
            UpdateFromDTO(org);
        }
        #endregion

        #region implementing IEquatable
        public bool Equals(csCommentDbM other) => (other != null) ? ((Comment, Person, Attraction) ==
            (other.Comment, other.Person, other.Attraction)) : false;

        public override bool Equals(object obj) => Equals(obj as csCommentDbM);
        public override int GetHashCode() => (Comment, Person, Attraction).GetHashCode();

        #endregion

        public override csCommentDbM Seed(csSeedGenerator sgen)
        {
            base.Seed(sgen);
            return this;
        }

        public csComment UpdateFromDTO(csCommentCUdto org)
        {
            if (org == null) return null;

            Comment = org.Comment;

            return this;
        }
    }
}