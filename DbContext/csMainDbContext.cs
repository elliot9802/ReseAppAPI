using Microsoft.EntityFrameworkCore;

using System.Data;
using Configuration;
using DbModels;
using System;
using Microsoft.Identity.Client;
using Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Models.DTO;
using Microsoft.Extensions.Options;

namespace DbContext;

//DbContext namespace is a fundamental EFC layer of the database context and is
//used for all Database connection as well as for EFC CodeFirst migration and database updates 

public class csMainDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    static public string Hello { get; } = $"Hello from namespace {nameof(DbContext)}, class {nameof(csMainDbContext)}";

    #region Table mapping / C# model of database tables or views
    //Tables
    public DbSet<csPersonDbM> Person { get; set; }
    public DbSet<csCommentDbM> Comments { get; set; }
    public DbSet<csAttractionDbM> Attractions { get; set; }
    public DbSet<csLocationDbM> Location { get; set; }

    //Views
    public DbSet<AttractionDetailsDto> vw_AttractionDetails { get; set; }
    public DbSet<AttractionsWithoutCommentsDto> vw_AttractionsWithoutComments { get; set; }
    public DbSet<UsersAndCommentsDto> vw_UsersAndComments { get; set; }
    public DbSet<DbInfoDto> vw_DbInfoDto { get; set; }
    #endregion

    #region get right context from DbSet configuration in json file and UserLogin
    public static DbContextOptionsBuilder<csMainDbContext> DbContextOptions(DbLoginDetail loginDetail)
    {
        var _optionsBuilder = new DbContextOptionsBuilder<csMainDbContext>();

        if (loginDetail.DbServer == "SQLServer")
        {
            _optionsBuilder.UseSqlServer(loginDetail.DbConnectionString,
                    options => options.EnableRetryOnFailure());
            return _optionsBuilder;
        }
        else if (loginDetail.DbServer == "MariaDb")
        {
            _optionsBuilder.UseMySql(loginDetail.DbConnectionString, ServerVersion.AutoDetect(loginDetail.DbConnectionString));
            return _optionsBuilder;
        }
        else if (loginDetail.DbServer == "Postgres")
        {
            _optionsBuilder.UseNpgsql(loginDetail.DbConnectionString);
            return _optionsBuilder;
        }
        else if (loginDetail.DbServer == "SQLite")
        {
            _optionsBuilder.UseSqlite(loginDetail.DbConnectionString);
            return _optionsBuilder;
        }

        //unknown database type
        throw new InvalidDataException($"Database type {loginDetail.DbServer} does not exist");
    }

    //Given a userlogin, this method finds the LoginDetails in the Active DbSet and return a DbContext
    public static csMainDbContext DbContext(string DbUserLogin) =>
        new csMainDbContext(csMainDbContext.DbContextOptions(csAppConfig.DbLoginDetails(DbUserLogin)).Options);

    #endregion

    #region constructors
    public csMainDbContext() { }
    public csMainDbContext(DbContextOptions options) : base(options)
    { }
    #endregion

    //Here we can modify the migration building
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region model the Views
        modelBuilder.Entity<AttractionDetailsDto>().ToView("vw_AttractionDetails", "dbo").HasNoKey();
        modelBuilder.Entity<AttractionsWithoutCommentsDto>().ToView("vw_AttractionsWithoutComments", "dbo").HasNoKey();
        modelBuilder.Entity<UsersAndCommentsDto>().ToView("vw_UsersWithComments", "dbo").HasNoKey();
        modelBuilder.Entity<DbInfoDto>().ToView("vw_DbInfoDto", "dbo").HasNoKey();
        #endregion

        #region Attractions mapping
        //Remove all attractions connected to a location if location is removed, a location is required for an attraction to exist
        modelBuilder.Entity("DbModels.csAttractionDbM", b =>
        {
            b.HasOne("DbModels.csLocationDbM", "LocationDbM")
            .WithMany("AttractionsDbM")
            .HasForeignKey("LocationId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            b.Navigation("LocationDbM");
        });
        #endregion

        #region Comments mapping
        //Remove all Comments related to an Attraction if the Attraction is removed, an attraction is required for comment to exist
        modelBuilder.Entity("DbModels.csCommentDbM", b =>
        {
            b.HasOne("DbModels.csAttractionDbM", "AttractionDbM")
            .WithMany("CommentsDbM")
            .HasForeignKey("AttractionId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            b.Navigation("AttractionDbM");
        });

        //Remove all Comments related to a Person if the Person is removed, a person is required for comment to exist
        modelBuilder.Entity("DbModels.csCommentDbM", b =>
        {
            b.HasOne("DbModels.csPersonDbM", "PersonDbM")
            .WithMany("CommentsDbM")
            .HasForeignKey("PersonId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            b.Navigation("PersonDbM");
        });
        #endregion


        base.OnModelCreating(modelBuilder);
    }



    #region DbContext for some popular databases
    public class SqlServerDbContext : csMainDbContext
    {
        public SqlServerDbContext() { }
        public SqlServerDbContext(DbContextOptions options) : base(options)
        { }


        //Used only for CodeFirst Database Migration and database update commands
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = csAppConfig.DbSetActive.DbLogins.Find(
                    i => i.DbServer == "SQLServer" && i.DbUserLogin == "sysadmin").DbConnectionString;
                optionsBuilder.UseSqlServer(connectionString,
                    options => options.EnableRetryOnFailure());

            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HaveColumnType("money");
            configurationBuilder.Properties<string>().HaveColumnType("nvarchar(200)");

            base.ConfigureConventions(configurationBuilder);
        }

        #region Add your own modelling based on done migrations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        #endregion

    }

    public class MySqlDbContext : csMainDbContext
    {
        public MySqlDbContext() { }
        public MySqlDbContext(DbContextOptions options) : base(options)
        { }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = csAppConfig.DbSetActive.DbLogins.Find(
                    i => i.DbServer == "MariaDb" && i.DbUserLogin == "sysadmin").DbConnectionString;
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");

            base.ConfigureConventions(configurationBuilder);

        }
    }

    public class PostgresDbContext : csMainDbContext
    {
        public PostgresDbContext() { }
        public PostgresDbContext(DbContextOptions options) : base(options)
        { }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = csAppConfig.DbSetActive.DbLogins.Find(
                    i => i.DbServer == "Postgres" && i.DbUserLogin == "sysadmin").DbConnectionString;
                optionsBuilder.UseNpgsql(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar(200)");
            base.ConfigureConventions(configurationBuilder);
        }
    }

    public class SqliteDbContext : csMainDbContext
    {
        public SqliteDbContext() { }
        public SqliteDbContext(DbContextOptions options) : base(options)
        { }


        //Used only for CodeFirst Database Migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = csAppConfig.DbSetActive.DbLogins.Find(
                    i => i.DbServer == "SQLite" && i.DbUserLogin == "sysadmin").DbConnectionString;
                optionsBuilder.UseSqlite(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
    #endregion
}

