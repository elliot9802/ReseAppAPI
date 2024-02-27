using DbContext;
using DbModels;
using Models;
using Models.DTO;

namespace Services
{
    public interface IReseAppService
    {
        //To test the overall layered structure
        public string InstanceHello { get; }

        #region Info and reading, unrelated to project demands
        public Task<gstusrInfoAllDto> InfoAsync { get; }

        public Task<List<ILocation>> ReadLocationsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);

        public Task<List<IComment>> ReadCommentsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);

        public Task<List<IPerson>> ReadPersonsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);

        public Task<IPerson> ReadPersonAsync(Guid id, bool flat);

        public Task<IAttraction> ReadAttractionAsync(Guid id, bool flat);
        #endregion

        #region ReseApp G del

        #region Seeding and removeseeding
        public Task<adminInfoDbDto> SeedAsync(int nrOfItems);
        public Task<adminInfoDbDto> RemoveSeedAsync(bool seeded);
        #endregion

        #region views
        public Task<List<IAttraction>> FilterAttractions(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
        public Task<sqlAllInfoDto> AttractionsWithoutComments { get; }
        public Task<IAttraction> AttractionDetails(Guid id);
        public Task<List<IPerson>> UsersAndComments { get; }
        #endregion

        #endregion

        #region ReseApp VG del

        #region Person
        public Task<IPerson> CreatePersonAsync(csPersonCUdto item);
        public Task<IPerson> DeletePersonAsync(Guid id);
        #endregion

        #region Attractions
        public Task<IAttraction> CreateAttractionAsync(csAttractionCUdto item);
        public Task<IAttraction> UpdateAttractionAsync(csAttractionCUdto item);
        public Task<IAttraction> DeleteAttractionAsync(Guid id);
        #endregion

        #region Comments
        public Task<bool> AddCommentToAttraction(Guid attractionId, Guid personId, string comment);
        public Task<IComment> DeleteCommentAsync(Guid id);
        #endregion

        #endregion
    }
}
