namespace SportData.WebAPI;

using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using SportData.Common.Constants;
using SportData.Converters.OlympicGames;
using SportData.Data.Contexts;
using SportData.Data.Converters.Countries;
using SportData.Data.Converters.OlympicGames;
using SportData.Data.Crawlers.Countries;
using SportData.Data.Crawlers.Olympedia;
using SportData.Data.Factories;
using SportData.Data.Factories.Interfaces;
using SportData.Data.Repositories;
using SportData.Services;
using SportData.Services.Data.CrawlerStorageDb;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Data.OlympicGamesDb;
using SportData.Services.Data.OlympicGamesDb.Interfaces;
using SportData.Services.Interfaces;
using SportData.Services.Mapper.Profiles;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        var app = builder.Build();
        Configure(app);

        app.MapGet("/security/getMessage",
() => "Hello World!").RequireAuthorization();
        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // JWT token
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
            };
        });

        builder.Services.AddAuthorization();

        // Log to file
        builder.Services.AddLogging(config =>
        {
            config.AddConfiguration(builder.Configuration.GetSection(AppGlobalConstants.LOGGING));
            config.AddConsole();
            config.AddLog4Net(builder.Configuration.GetSection(AppGlobalConstants.LOG4NET_CORE).Get<Log4NetProviderOptions>());
        });

        // Automapper
        builder.Services.AddAutoMapper(typeof(OlympicGamesProfile));

        // Databases options
        var crawlerStorageDbOptions = new DbContextOptionsBuilder<CrawlerStorageDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.CRAWLER_STORAGE_CONNECTION_STRING))
            .Options;

        var sportDataDbOptions = new DbContextOptionsBuilder<CrawlerStorageDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.SPORT_DATA_CONNECTION_STRING))
            .Options;

        var olympicGamesDbOptions = new DbContextOptionsBuilder<OlympicGamesDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.OLYMPIC_GAMES_CONNECTION_STRING))
            .Options;

        // Database factory
        var dbContextFactory = new DbContextFactory(crawlerStorageDbOptions, olympicGamesDbOptions);
        builder.Services.AddSingleton<IDbContextFactory>(dbContextFactory);

        // Databases
        builder.Services.AddDbContext<SportDataDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.SPORT_DATA_CONNECTION_STRING));
        });

        builder.Services.AddDbContext<CrawlerStorageDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.CRAWLER_STORAGE_CONNECTION_STRING));
            //options.UseLazyLoadingProxies();
        });

        // Repositories
        builder.Services.AddScoped(typeof(SportDataRepository<>));

        // Services
        builder.Services.AddScoped<IZipService, ZipService>();
        builder.Services.AddScoped<IRegExpService, RegExpService>();
        builder.Services.AddScoped<IHttpService, HttpService>();
        builder.Services.AddScoped<IMD5Hash, MD5Hash>();
        builder.Services.AddScoped<INormalizeService, NormalizeService>();
        builder.Services.AddScoped<IOlympediaService, OlympediaService>();
        builder.Services.AddScoped<IDateService, DateService>();
        builder.Services.AddTransient<IJwtService, JwtService>();
        //builder.Services.AddTransient<IUserService, UserService>();

        // Data services
        builder.Services.AddScoped<IOperationsService, OperationsService>();
        builder.Services.AddScoped<ICrawlersService, CrawlersService>();
        builder.Services.AddScoped<IGroupsService, GroupsService>();
        builder.Services.AddScoped<ILogsService, LogsService>();
        builder.Services.AddScoped<IDataCacheService, DataCacheService>();
        builder.Services.AddScoped<ICountriesService, CountriesService>();
        builder.Services.AddScoped<INOCsService, NOCsService>();
        builder.Services.AddScoped<ICitiesService, CitiesService>();
        builder.Services.AddScoped<IGamesService, GamesService>();
        builder.Services.AddScoped<IHostsService, HostsService>();
        builder.Services.AddScoped<ISportsService, SportsService>();
        builder.Services.AddScoped<IDisciplinesService, DisciplinesService>();
        builder.Services.AddScoped<IVenuesService, VenuesService>();
        builder.Services.AddScoped<IEventsService, EventsService>();
        builder.Services.AddScoped<IEventVenueService, EventVenueService>();
        builder.Services.AddScoped<IAthletesService, AthletesService>();
        builder.Services.AddScoped<INationalitiesService, NationalitiesService>();
        builder.Services.AddScoped<IParticipantsService, ParticipantsService>();
        builder.Services.AddScoped<ITeamsService, TeamsService>();
        builder.Services.AddScoped<ISquadsService, SquadsService>();
        builder.Services.AddScoped<IResultsService, ResultsService>();
        builder.Services.AddScoped<Services.Data.SportDataDb.Interfaces.ICountriesService, Services.Data.SportDataDb.CountriesService>();

        // Crawlers
        builder.Services.AddTransient<CountryDataCrawler>();
        builder.Services.AddTransient<NOCCrawler>();
        builder.Services.AddTransient<GameCrawler>();
        builder.Services.AddTransient<SportDisciplineCrawler>();
        builder.Services.AddTransient<ResultCrawler>();
        builder.Services.AddTransient<AthleteCrawler>();
        builder.Services.AddTransient<VenueCrawler>();

        // Converters
        builder.Services.AddScoped<CountryDataConverter>();
        builder.Services.AddScoped<CountryConverter>();
        builder.Services.AddScoped<NOCConverter>();
        builder.Services.AddScoped<GameConverter>();
        builder.Services.AddScoped<SportDisciplineConverter>();
        builder.Services.AddScoped<VenueConverter>();
        builder.Services.AddScoped<EventConverter>();
        builder.Services.AddScoped<AthleteConverter>();
        builder.Services.AddScoped<ParticipantConverter>();
        builder.Services.AddScoped<ResultConverter>();
    }

    private static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}