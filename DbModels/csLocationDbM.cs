using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace DbModels
{

    [Table("Location")] 
    [Index(nameof(StreetAddress), nameof(City), nameof(Country))] 
    public class csLocationDbM : csLocation, ISeed<csLocationDbM>, IEquatable<csLocationDbM>
    {
        [Key]
        public override Guid LocationId { get; set; }

        [Required]
        public override string StreetAddress { get; set; }
        [Required]
        public override string City { get; set; }
        [Required]
        public override string Country { get; set; }

        [NotMapped]
        public override List<IAttraction> Attractions
        {
            get => AttractionsDbM?.ToList<IAttraction>();
            set => new NotImplementedException();
        }

        [JsonIgnore]
        public virtual List<csAttractionDbM> AttractionsDbM { get; set; } = new List<csAttractionDbM>();

        #region constructors
        public csLocationDbM UpdateFromDTO(csLocationCUdto org)
        {
            if (org == null) return null;

            StreetAddress = org.StreetAddress;
            City = org.City;
            Country = org.Country;

            return this;
        }
        public csLocationDbM() { }
        public csLocationDbM(csLocationCUdto org)
        {
            LocationId = Guid.NewGuid();
            UpdateFromDTO(org);
        }
        #endregion

        #region implementing IEquatable

        public bool Equals(csLocationDbM other) => (other != null) ? ((StreetAddress, City, Country) ==
            (other.StreetAddress, other.City, other.Country)) : false;

        public override bool Equals(object obj) => Equals(obj as csLocationDbM);
        public override int GetHashCode() => (StreetAddress, City, Country).GetHashCode();

        #endregion

        #region Seeding
        public override csLocationDbM Seed(csSeedGenerator sgen)
        {
            base.Seed(sgen);
            return this;
        }
        #endregion

    }
}