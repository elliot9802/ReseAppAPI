using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModels;

[Table("Person")]
[Index(nameof(FirstName), nameof(LastName))]
[Index(nameof(LastName), nameof(FirstName))]
public class csPersonDbM : csPerson, ISeed<csPersonDbM>
{
    [Key]
    public override Guid PersonId { get; set; }

    [Required]
    public override string FirstName { get; set; }
    [Required]
    public override string LastName { get; set; }

    [NotMapped]
    public override List<IComment> Comments
    {
        get => CommentsDbM?.ToList<IComment>();
        set => new NotImplementedException();
    }

    [JsonIgnore]
    [ForeignKey("CommentId")]
    public List<csCommentDbM> CommentsDbM { get; set; } = new List<csCommentDbM>();

    #region constructors
    public csPersonDbM UpdateFromDTO(csPersonCUdto org)
    {
        FirstName = org.FirstName;
        LastName = org.LastName;

        return this;
    }

    public csPersonDbM() { }
    public csPersonDbM(csPersonCUdto org)
    {
        PersonId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion

    #region Seeding
    public override csPersonDbM Seed(csSeedGenerator sgen)
    {
        base.Seed(sgen);
        return this;
    }
    #endregion
}


