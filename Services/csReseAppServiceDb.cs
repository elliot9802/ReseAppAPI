using DbContext;
using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services
{
    public class csReseAppServiceDb : IReseAppService
    {
        private csReseAppDbRepos _repo = null;
        private ILogger<csReseAppServiceDb> _logger = null;

        #region only for layer verification
        private Guid _guid = Guid.NewGuid();
        private string _instanceHello;

        public string InstanceHello => _instanceHello;

        static public string Hello { get; } = $"Hello from namespace {nameof(Services)}, class {nameof(csReseAppServiceDb)}" +

            // added after project references is setup
            $"\n   - {csReseAppDbRepos.Hello}" +
            $"\n   - {csMainDbContext.Hello}";
        #endregion

        #region constructors
        public csReseAppServiceDb(ILogger<csReseAppServiceDb> logger)
        {
            //only for layer verification
            _instanceHello = $"Hello from class {this.GetType()} with instance Guid {_guid}. ";

            _logger = logger;
            _logger.LogInformation(_instanceHello);
        }

        public csReseAppServiceDb(csReseAppDbRepos repo, ILogger<csReseAppServiceDb> logger)
        {
            //only for layer verification
            _instanceHello = $"Hello from class {this.GetType()} with instance Guid {_guid}. " +
                $"Will use repo {repo.GetType()}.";

            _logger = logger;
            _logger.LogInformation(_instanceHello);

            _repo = repo;

        }
        #endregion

        // 1:1 Calls between csReseAppServiceDb & csReseAppDbRepos
        #region Info and reading, unrelated to project demands
        public Task<gstusrInfoAllDto> InfoAsync => _repo.InfoAsync();
        public Task<List<ILocation>> ReadLocationsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.ReadLocationsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<List<IComment>> ReadCommentsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.ReadCommentsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<List<IPerson>> ReadPersonsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.ReadPersonsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<IPerson> ReadPersonAsync(Guid id, bool flat) => _repo.ReadPersonAsync(id, flat);
        public Task<IAttraction> ReadAttractionAsync(Guid id, bool flat) => _repo.ReadAttractionAsync(id, flat);
        #endregion

        #region ReseApp G del

        #region Seeding and removeseeding
        public Task<adminInfoDbDto> SeedAsync(int nrOfItems) => _repo.SeedAsync(nrOfItems);
        public Task<adminInfoDbDto> RemoveSeedAsync(bool seeded) => _repo.RemoveSeedAsync(seeded);
        #endregion

        #region views
        public Task<List<IAttraction>> FilterAttractions(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.FilterAttractions(seeded, flat, filter, pageNumber, pageSize);
        public Task<sqlAllInfoDto> AttractionsWithoutComments => _repo.AttractionsWithoutComments();
        public Task<IAttraction> AttractionDetails(Guid id) => _repo.AttractionDetails(id);
        public Task<List<IPerson>> UsersAndComments => _repo.UsersAndComments();
        #endregion

        #endregion

        #region ReseApp VG del

        #region Person
        public Task<IPerson> CreatePersonAsync(csPersonCUdto item) => _repo.CreatePersonAsync(item);
        public Task<IPerson> DeletePersonAsync(Guid id) => _repo.DeletePersonAsync(id);
        #endregion

        #region Comments
        public Task<bool> AddCommentToAttraction(Guid attractionId, Guid personId, string comment) => _repo.AddCommentToAttraction(attractionId, personId, comment);
        public Task<IComment> DeleteCommentAsync(Guid id) => _repo.DeleteCommentAsync(id);
        #endregion

        #region Attractions
        public Task<IAttraction> DeleteAttractionAsync(Guid id) => _repo.DeleteAttractionAsync(id);
        public Task<IAttraction> UpdateAttractionAsync(csAttractionCUdto item) => _repo.UpdateAttractionAsync(item);
        public Task<IAttraction> CreateAttractionAsync(csAttractionCUdto item) => _repo.CreateAttractionAsync(item);
        #endregion

        #endregion
    }
}