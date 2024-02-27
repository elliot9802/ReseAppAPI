using System;
namespace Models.DTO
{
    public class adminInfoDbDto
    {
        public int nrSeededPersons { get; set; } = 0;
        public int nrUnseededPersons { get; set; } = 0;

        public int nrSeededLocations { get; set; } = 0;
        public int nrUnseededLocations { get; set; } = 0;

        public int nrSeededAttractions { get; set; } = 0;
        public int nrUnseededAttractions { get; set; } = 0;

        public int nrSeededComments { get; set; } = 0;
        public int nrUnseededComments { get; set; } = 0;
    }
}

