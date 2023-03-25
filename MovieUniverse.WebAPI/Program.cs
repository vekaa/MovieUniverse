using MovieUniverse.Data;
using MovieUniverse.WebAPI.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MovieContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;
});
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IGetMovieMapper, GetMovieMapper>();
builder.Services.AddTransient<ICreateMovieMapper, CreateMovieMapper>();
builder.Services.AddTransient<IUpdateMovieMapper, UpdateMovieMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
