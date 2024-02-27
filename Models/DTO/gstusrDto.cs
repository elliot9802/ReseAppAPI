namespace Models.DTO
{
    public class gstusrInfoDbDto
    {
        public int nrSeededPersons { get; set; } = 0;
        public int nrUnseededPersons { get; set; } = 0;
        public int nrPersonsWithComments { get; set; } = 0;
        public int nrPersonsWithOutComments { get; set; } = 0;

        public int nrSeededLocations { get; set; } = 0;
        public int nrUnseededLocations { get; set; } = 0;

        public int nrSeededAttractions { get; set; } = 0;
        public int nrUnseededAttractions { get; set; } = 0;
        public int nrAttractionsWithComments { get; set; } = 0;
        public int nrAttractionsWithOutComments { get; set; } = 0;
        public int nrLocationsWithAttractions { get; set; } = 0;
        public int nrLocationsWithOutAttractions { get; set; } = 0;


        public int nrSeededComments { get; set; } = 0;
        public int nrUnseededComments { get; set; } = 0;
    }

    public class gstusrInfoAllDto
    {
        public gstusrInfoDbDto Db { get; set; } = null;
    }
}
