using Configuration;
using DbContext;
using DbRepos;
using Services;

var builder = WebApplication.CreateBuilder(args);

#region Insert standard WebApi services
// NOTE: global cors policy needed for JS and React frontends
builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
#endregion

#region Dependency Inject FriendsService

//DI injects the DbRepos into csReseAppService
//Services are typically added as Scoped as one scope is a Web client request
builder.Services.AddScoped<csReseAppDbRepos>();

//WebController have a matching constructor, so service must be created
//Services are typically added as Scoped as one scope is a Web client request
builder.Services.AddScoped<IReseAppService, csReseAppServiceDb>();
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline
//In this example always use Swagger 
//if (app.Environment.IsDevelopment())
//{

app.UseSwagger();
app.UseSwaggerUI();

//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion