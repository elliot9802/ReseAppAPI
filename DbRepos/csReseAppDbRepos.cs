using DbContext;
using DbModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
using System.Data;

namespace DbRepos
{

    public class csReseAppDbRepos
    {
        private ILogger<csReseAppDbRepos> _logger = null;
        private string _dblogin = "sysadmin";

        #region only for layer verification
        private Guid _guid = Guid.NewGuid();
        private string _instanceHello = null;

        static public string Hello { get; } = $"Hello from namespace {nameof(DbRepos)}, class {nameof(csReseAppDbRepos)}";
        public string InstanceHello => _instanceHello;
        #endregion

        #region exploring the ChangeTracker
        private static void ExploreChangeTracker(csMainDbContext db)
        {
            foreach (var e in db.ChangeTracker.Entries())
            {
                if (e.Entity is csCommentDbM c)
                {
                    Console.WriteLine(e.State);
                    Console.WriteLine(c.CommentId);
                }
            }
        }
        #endregion

        #region contructors
        public csReseAppDbRepos()
        {
            _instanceHello = $"Hello from class {this.GetType()} with instance Guid {_guid}.";
        }
        public csReseAppDbRepos(ILogger<csReseAppDbRepos> logger) : this()
        {
            _logger = logger;
            _logger.LogInformation(_instanceHello);
        }
        #endregion

        #region Info and reading, unrelated to project demands
        public async Task<gstusrInfoAllDto> InfoAsync()
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var _info = new gstusrInfoAllDto
                {
                    Db = new gstusrInfoDbDto
                    {
                        #region Persons info
                        nrSeededPersons = await db.Person.Where(f => f.Seeded).CountAsync(),
                        nrUnseededPersons = await db.Person.Where(f => !f.Seeded).CountAsync(),

                        nrPersonsWithComments = await db.Person
                            .Where(p => p.CommentsDbM.Any())
                            .CountAsync(),
                        nrPersonsWithOutComments = await db.Person
                            .Where(p => !p.CommentsDbM.Any())
                            .CountAsync(),
                        #endregion

                        #region Locations info
                        nrSeededLocations = await db.Location.Where(f => f.Seeded).CountAsync(),
                        nrUnseededLocations = await db.Location.Where(f => !f.Seeded).CountAsync(),

                        nrLocationsWithAttractions = await db.Location
                            .Where(p => !p.AttractionsDbM.Any())
                            .CountAsync(),
                        nrLocationsWithOutAttractions = await db.Location
                            .Where(p => !p.AttractionsDbM.Any())
                            .CountAsync(),
                        #endregion

                        #region Attractions info
                        nrSeededAttractions = await db.Attractions.Where(f => f.Seeded).CountAsync(),
                        nrUnseededAttractions = await db.Attractions.Where(f => !f.Seeded).CountAsync(),

                        nrAttractionsWithComments = await db.Attractions
                            .Where(p => p.CommentsDbM.Any())
                            .CountAsync(),
                        nrAttractionsWithOutComments = await db.Attractions
                            .Where(p => !p.CommentsDbM.Any())
                            .CountAsync(),
                        #endregion

                        #region Comments info
                        nrSeededComments = await db.Comments.Where(f => f.Seeded).CountAsync(),
                        nrUnseededComments = await db.Comments.Where(f => !f.Seeded).CountAsync(),
                        #endregion
                    }
                };
                return _info;
            }
        }

        public async Task<List<IPerson>> ReadPersonsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                filter ??= "";
                if (!flat)
                {
                    var _query = db.Person.AsNoTracking().Include(i => i.CommentsDbM)
                        .ThenInclude(cm => cm.AttractionDbM);

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.FirstName.ToLower().Contains(filter) ||
                                i.LastName.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<IPerson>();
                }
                else
                {
                    var _query = db.Person.AsNoTracking();

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.FirstName.ToLower().Contains(filter) ||
                                i.LastName.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<IPerson>();
                }
            }
        }
        public async Task<List<IComment>> ReadCommentsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                filter ??= "";
                if (!flat)
                {
                    var _query = db.Comments.AsNoTracking().Include(i => i.PersonDbM)
                        .Include(cm => cm.AttractionDbM)
                        .ThenInclude(atn => atn.LocationDbM);

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.Comment.ToLower().Contains(filter) ||
                                i.PersonDbM.FirstName.ToLower().Contains(filter) ||
                                i.PersonDbM.LastName.ToLower().Contains(filter) ||
                                i.AttractionDbM.strCategory.ToLower().Contains(filter) ||
                                i.AttractionDbM.AttractionTitle.ToLower().Contains(filter) ||
                                i.AttractionDbM.AttractionDescription.ToLower().Contains(filter) ||
                                i.AttractionDbM.LocationDbM.Country.ToLower().Contains(filter) ||
                                i.AttractionDbM.LocationDbM.City.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<IComment>();
                }
                else
                {
                    var _query = db.Comments.AsNoTracking();

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.Comment.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<IComment>();
                }
            }
        }
        public async Task<List<ILocation>> ReadLocationsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                filter ??= "";
                if (!flat)
                {
                    var _query = db.Location.AsNoTracking().Include(i => i.AttractionsDbM);

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.StreetAddress.ToLower().Contains(filter) ||
                                i.City.ToLower().Contains(filter) ||
                                i.Country.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<ILocation>();
                }
                else
                {
                    var _query = db.Location.AsNoTracking();

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.StreetAddress.ToLower().Contains(filter) ||
                                i.City.ToLower().Contains(filter) ||
                                i.Country.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<ILocation>();
                }
            }
        }
        public async Task<IPerson> ReadPersonAsync(Guid id, bool flat)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                if (flat)
                {
                    var pn = await db.Person
                        .FirstOrDefaultAsync(pn => pn.PersonId == id);

                    return pn;
                }
                else
                {
                    var pn = await db.Person
                        .Include(pn => pn.CommentsDbM)
                        .ThenInclude(cm => cm.AttractionDbM)
                    .FirstOrDefaultAsync(pn => pn.PersonId == id);

                    return pn;
                }
            }
        }
        public async Task<IAttraction> ReadAttractionAsync(Guid id, bool flat)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                if (flat)
                {
                    var atn = await db.Attractions
                        .FirstOrDefaultAsync(mg => mg.AttractionId == id);

                    return atn;
                }
                else
                {
                    var atn = await db.Attractions
                        .Include(atn => atn.LocationDbM)
                        .Include(atn => atn.CommentsDbM)
                        .ThenInclude(cm => cm.PersonDbM)
                    .FirstOrDefaultAsync(atn => atn.AttractionId == id);

                    return atn;
                }
            }
        }
        #endregion

        #region ReseApp G delen
        #region punkt 5 & 7: seeding and removeseeding
        public async Task<adminInfoDbDto> SeedAsync(int nrOfItems)
        {
            var _seeder = new csSeedGenerator();
            var _locations = new List<csLocationDbM>();
            var _attractions = new List<csAttractionDbM>();
            var _persons = new List<csPersonDbM>();
            var _comments = new List<csCommentDbM>();

            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                // Seed locations first and add to the list
                for (int i = 0; i < nrOfItems; i++)
                {
                    var loc = new csLocationDbM().Seed(_seeder);
                    _locations.Add(loc);
                }

                // Seed persons and add to the list
                for (int i = 0; i < nrOfItems; i++)
                {
                    var person = new csPersonDbM().Seed(_seeder);
                    _persons.Add(person);
                }

                // Seed attractions, associate with locations, and add to the list
                for (int i = 0; i < nrOfItems; i++)
                {
                    var attraction = new csAttractionDbM().Seed(_seeder);
                    attraction.LocationDbM = _locations[_seeder.Next(0, _locations.Count)];
                    _attractions.Add(attraction);
                }

                // Seed comments, associate with attractions and persons, and add to the list
                for (int i = 0; i < nrOfItems; i++)
                {
                    var comment = new csCommentDbM().Seed(_seeder);
                    comment.AttractionDbM = _attractions[_seeder.Next(0, _attractions.Count)];
                    comment.PersonDbM = _persons[_seeder.Next(0, _persons.Count)];

                    // Generate a random number between 0 and 20 for the number of comments
                    int numComments = _seeder.Next(0, 21); // 0 to 20 comments
                    for (int j = 0; j < numComments; j++)
                    {
                        _comments.Add(comment);
                    }
                }

                // Add all entities to the DbContext ChangeTracker
                db.Location.AddRange(_locations);
                db.Person.AddRange(_persons);
                db.Attractions.AddRange(_attractions);
                db.Comments.AddRange(_comments);

                // Save all changes in a single call
                await db.SaveChangesAsync();

                // Create and return an instance of adminInfoDbDto with counts
                var _info = new adminInfoDbDto()
                {
                    nrSeededPersons = db.Person.AsEnumerable().Count(p => p.Seeded),
                    nrUnseededPersons = db.Person.AsEnumerable().Count(p => !p.Seeded),
                    nrSeededAttractions = db.Attractions.AsEnumerable().Count(a => a.Seeded),
                    nrUnseededAttractions = db.Attractions.AsEnumerable().Count(a => !a.Seeded),
                    nrSeededLocations = db.Location.AsEnumerable().Count(l => l.Seeded),
                    nrUnseededLocations = db.Location.AsEnumerable().Count(l => !l.Seeded),
                    nrSeededComments = db.Comments.AsEnumerable().Count(c => c.Seeded),
                    nrUnseededComments = db.Comments.AsEnumerable().Count(c => !c.Seeded),
                };
                return _info;
            }
        }


        public async Task<adminInfoDbDto> RemoveSeedAsync(bool seeded)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                if (seeded)
                {
                    // Remove seeded csCommentDbM entities
                    db.Comments.RemoveRange(db.Comments.Where(c => c.Seeded));

                    // Remove seeded csAttractionDbM entities
                    db.Attractions.RemoveRange(db.Attractions.Where(a => a.Seeded));

                    // Remove seeded csLocationDbM entities
                    db.Location.RemoveRange(db.Location.Where(l => l.Seeded));

                    // Remove seeded csPersonDbM entities
                    db.Person.RemoveRange(db.Person.Where(p => p.Seeded));
                }
                else
                {
                    // Remove unseeded csCommentDbM entities
                    db.Comments.RemoveRange(db.Comments.Where(c => !c.Seeded));

                    // Remove unseeded csAttractionDbM entities
                    db.Attractions.RemoveRange(db.Attractions.Where(a => !a.Seeded));

                    // Remove unseeded csLocationDbM entities
                    db.Location.RemoveRange(db.Location.Where(l => !l.Seeded));

                    // Remove unseeded csPersonDbM entities
                    db.Person.RemoveRange(db.Person.Where(p => !p.Seeded));
                }
                await db.SaveChangesAsync();

                ExploreChangeTracker(db);

                var _info = new adminInfoDbDto();

                _info.nrSeededPersons = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csPersonDbM) && entry.State == EntityState.Deleted);
                _info.nrSeededAttractions = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csAttractionDbM) && entry.State == EntityState.Deleted);
                _info.nrSeededLocations = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csLocationDbM) && entry.State == EntityState.Deleted);
                _info.nrSeededComments = db.ChangeTracker.Entries().Count(entry => (entry.Entity is csCommentDbM) && entry.State == EntityState.Deleted);
                _info.nrUnseededPersons = db.Person.AsEnumerable().Count(p => !p.Seeded);
                _info.nrUnseededAttractions = db.Attractions.AsEnumerable().Count(a => !a.Seeded);
                _info.nrUnseededLocations = db.Location.AsEnumerable().Count(l => !l.Seeded);
                _info.nrUnseededComments = db.Comments.AsEnumerable().Count(c => !c.Seeded);

                return _info;
            }
        }
        #endregion

        #region punkt 6: view
        public async Task<List<IAttraction>> FilterAttractions(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                filter ??= "";
                if (!flat)
                {
                    var _query = db.Attractions.AsNoTracking().Include(i => i.LocationDbM);

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.strCategory.ToLower().Contains(filter) ||
                                i.AttractionTitle.ToLower().Contains(filter) ||
                                i.AttractionDescription.ToLower().Contains(filter) ||
                                i.LocationDbM.Country.ToLower().Contains(filter) ||
                                i.LocationDbM.City.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<IAttraction>();
                }
                else
                {
                    var _query = db.Attractions.AsNoTracking();

                    return await _query
                        .Where(i => i.Seeded == seeded
                                && (i.strCategory.ToLower().Contains(filter) ||
                                i.AttractionTitle.ToLower().Contains(filter) ||
                                i.AttractionDescription.ToLower().Contains(filter)))
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync<IAttraction>();
                }
            }
        }

        public async Task<sqlAllInfoDto> AttractionsWithoutComments()
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var _info = new sqlAllInfoDto();

                _info.AttractionsWithoutComments = await db.vw_AttractionsWithoutComments.ToListAsync();

                return _info;
            }
        }

        public async Task<IAttraction> AttractionDetails(Guid id)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var atn = await db.Attractions.Include(atn => atn.CommentsDbM).ThenInclude(cm => cm.PersonDbM)
                    .FirstOrDefaultAsync(atn => atn.AttractionId == id);

                return atn;
            }
        }

        public async Task<List<IPerson>> UsersAndComments()
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var _list = await db.Person
                                 .Include(pn => pn.CommentsDbM).ToListAsync();
                List<IPerson> result = _list.Select(person => (IPerson)person).ToList();
                return result;
            }
        }
        #endregion

        #endregion

        #region ReseApp VG delen

        #region Person repo methods
        public async Task<IPerson> CreatePersonAsync(csPersonCUdto _src)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var _person = new csPersonDbM(_src);

                db.Person.Add(_person);

                await db.SaveChangesAsync();
                return _person;
            }
        }
        public async Task<IPerson> DeletePersonAsync(Guid id)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var atn = await db.Person
                    .Include(cm => cm.CommentsDbM)
                    .FirstOrDefaultAsync(atn => atn.PersonId == id);
                if (atn == null)
                {
                    throw new ArgumentException($"Item with id {id} does not exist");
                }
                else
                {
                    // Loop through and remove each related comment
                    foreach (var comment in atn.CommentsDbM)
                    {
                        db.Comments.Remove(comment);
                    }

                    db.Person.Remove(atn);

                    await db.SaveChangesAsync();

                    return atn;
                }
            }
        }
        #endregion

        #region comments repo methods
        public async Task<bool> AddCommentToAttraction(Guid attractionId, Guid personId, string comment)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                try
                {
                    // Create the parameters for the stored procedure
                    var attractionIdParam = new SqlParameter("@AttractionID", attractionId);
                    var personIdParam = new SqlParameter("@PersonId", personId);
                    var commentParam = new SqlParameter("@Comment", comment);

                    // Call the stored procedure to add the comment to the attraction
                    await db.Database.ExecuteSqlRawAsync("EXEC usp_AddCommentToAttraction @AttractionID, @PersonId, @Comment",
                        attractionIdParam, personIdParam, commentParam);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
        public async Task<IComment> DeleteCommentAsync(Guid id)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var cm = await db.Comments
                    .FirstOrDefaultAsync(cm => cm.CommentId == id);
                if (cm == null)
                {
                    throw new ArgumentException($"Item with id {id} does not exist");
                }
                else
                {
                    db.Comments.Remove(cm);

                    await db.SaveChangesAsync();

                    return cm;
                }
            }
        }
        #endregion

        #region Attractions repo methods
        private static async Task csAttractionCUdto_to_csAttractionDbM(csMainDbContext db, csAttractionCUdto _itemDtoSrc, csAttractionDbM _itemDst)
        {
            //update LocationDbM from itemDto.LocationId
            _itemDst.LocationDbM = await db.Location.FirstOrDefaultAsync(
                a => (a.LocationId == _itemDtoSrc.LocationId));

            var _comments = new List<csCommentDbM>();
            foreach (var id in _itemDtoSrc.CommentsId)
            {
                var c = await db.Comments.FirstOrDefaultAsync(i => i.CommentId == id);
                if (c == null)
                    throw new ArgumentException($"Item id {id} not found");

                _comments.Add(c);
            }

            _itemDst.CommentsDbM = _comments;
        }

        public async Task<IAttraction> CreateAttractionAsync(csAttractionCUdto _src)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                // Get a list of all available LocationIds
                var availableLocationIds = await db.Location
                    .Where(l => l.Seeded)
                    .Select(l => l.LocationId)
                    .ToListAsync();

                if (availableLocationIds.Count == 0)
                {
                    throw new Exception("No available LocationIds to assign.");
                }

                // Generate a random index to select a LocationId
                var random = new Random();
                var randomIndex = random.Next(availableLocationIds.Count);

                // Get the randomly selected LocationId
                var randomLocationId = availableLocationIds[randomIndex];

                // Transfer any changes from DTO to the database object
                var _item = new csAttractionDbM(_src);

                // Set the LocationId to the randomly selected value
                _item.LocationId = randomLocationId;

                // Write to the database model
                db.Attractions.Add(_item);

                // Write to the database in a UoW
                await db.SaveChangesAsync();
                return _item;
            }
        }
        public async Task<IAttraction> UpdateAttractionAsync(csAttractionCUdto _src)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var _query1 = db.Attractions
                    .Where(i => i.AttractionId == _src.AttractionId);
                var _item = await _query1.Include(i => i.LocationDbM).FirstOrDefaultAsync();

                if (_item == null) throw new ArgumentException($"Item {nameof(_src.AttractionId)} can't be found, Make sure to use DTO data for updating");

                _item.UpdateFromDTO(_src);

                await csAttractionCUdto_to_csAttractionDbM(db, _src, _item);

                db.Attractions.Update(_item);

                await db.SaveChangesAsync();
                return _item;
            }
        }
        public async Task<IAttraction> DeleteAttractionAsync(Guid id)
        {
            using (var db = csMainDbContext.DbContext(_dblogin))
            {
                var atn = await db.Attractions
                    .Include(a => a.CommentsDbM)
                    .FirstOrDefaultAsync(atn => atn.AttractionId == id);
                if (atn == null)
                {
                    throw new ArgumentException($"Item with id {id} does not exist");
                }
                else
                {
                    // Loop through and remove each related comment
                    foreach (var comment in atn.CommentsDbM)
                    {
                        db.Comments.Remove(comment);
                    }

                    db.Attractions.Remove(atn);

                    await db.SaveChangesAsync();

                    return atn;
                }
            }
        }
        #endregion

        #endregion
    }

}
