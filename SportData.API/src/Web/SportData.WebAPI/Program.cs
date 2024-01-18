namespace SportData.WebAPI;

using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
using SportData.Data.Models.Entities.SportData;
using SportData.Data.Options;
using SportData.Data.Repositories;
using SportData.Data.Seeders;
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
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();
        Configure(app);
        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // JWT token
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
            };
        });

        services.AddAuthorization();

        // Log to file
        services.AddLogging(config =>
        {
            config.AddConfiguration(configuration.GetSection(AppGlobalConstants.LOGGING));
            config.AddConsole();
            config.AddLog4Net(configuration.GetSection(AppGlobalConstants.LOG4NET_CORE).Get<Log4NetProviderOptions>());
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(IdentityOptionsProvider.SetIdentityOptions)
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<SportDataDbContext>()
            .AddDefaultTokenProviders();

        // Automapper
        services.AddAutoMapper(typeof(OlympicGamesProfile));

        // Databases options
        var crawlerStorageDbOptions = new DbContextOptionsBuilder<CrawlerStorageDbContext>()
            .UseSqlServer(configuration.GetConnectionString(AppGlobalConstants.CRAWLER_STORAGE_CONNECTION_STRING))
            .Options;

        var olympicGamesDbOptions = new DbContextOptionsBuilder<OlympicGamesDbContext>()
            .UseSqlServer(configuration.GetConnectionString(AppGlobalConstants.OLYMPIC_GAMES_CONNECTION_STRING))
            .Options;

        // Database factory
        var dbContextFactory = new DbContextFactory(crawlerStorageDbOptions, olympicGamesDbOptions);
        services.AddSingleton<IDbContextFactory>(dbContextFactory);

        services.AddDbContext<SportDataDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(AppGlobalConstants.SPORT_DATA_CONNECTION_STRING));
        });

        // Repositories
        services.AddScoped(typeof(SportDataRepository<>));

        // Services
        services.AddScoped<IZipService, ZipService>();
        services.AddScoped<IRegExpService, RegExpService>();
        services.AddScoped<IHttpService, HttpService>();
        services.AddScoped<IMD5Hash, MD5Hash>();
        services.AddScoped<INormalizeService, NormalizeService>();
        services.AddScoped<IOlympediaService, OlympediaService>();
        services.AddScoped<IDateService, DateService>();
        services.AddTransient<IJwtService, JwtService>();
        //builder.Services.AddTransient<IUserService, UserService>();

        // Data services
        services.AddScoped<IOperationsService, OperationsService>();
        services.AddScoped<ICrawlersService, CrawlersService>();
        services.AddScoped<IGroupsService, GroupsService>();
        services.AddScoped<ILogsService, LogsService>();
        services.AddScoped<IDataCacheService, DataCacheService>();
        services.AddScoped<ICountriesService, CountriesService>();
        services.AddScoped<INOCsService, NOCsService>();
        services.AddScoped<ICitiesService, CitiesService>();
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IHostsService, HostsService>();
        services.AddScoped<ISportsService, SportsService>();
        services.AddScoped<IDisciplinesService, DisciplinesService>();
        services.AddScoped<IVenuesService, VenuesService>();
        services.AddScoped<IEventsService, EventsService>();
        services.AddScoped<IEventVenueService, EventVenueService>();
        services.AddScoped<IAthletesService, AthletesService>();
        services.AddScoped<INationalitiesService, NationalitiesService>();
        services.AddScoped<IParticipantsService, ParticipantsService>();
        services.AddScoped<ITeamsService, TeamsService>();
        services.AddScoped<ISquadsService, SquadsService>();
        services.AddScoped<IResultsService, ResultsService>();
        services.AddScoped<Services.Data.SportDataDb.Interfaces.ICountriesService, Services.Data.SportDataDb.CountriesService>();

        // Crawlers
        services.AddTransient<CountryDataCrawler>();
        services.AddTransient<NOCCrawler>();
        services.AddTransient<GameCrawler>();
        services.AddTransient<SportDisciplineCrawler>();
        services.AddTransient<ResultCrawler>();
        services.AddTransient<AthleteCrawler>();
        services.AddTransient<VenueCrawler>();

        // Converters
        services.AddScoped<CountryDataConverter>();
        services.AddScoped<CountryConverter>();
        services.AddScoped<NOCConverter>();
        services.AddScoped<GameConverter>();
        services.AddScoped<SportDisciplineConverter>();
        services.AddScoped<VenueConverter>();
        services.AddScoped<EventConverter>();
        services.AddScoped<AthleteConverter>();
        services.AddScoped<ParticipantConverter>();
        services.AddScoped<ResultConverter>();
    }

    private static void Configure(WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<SportDataDbContext>();
            dbContext.Database.Migrate();

            new SportDataDbSeeder().SeedAsync(serviceScope.ServiceProvider).GetAwaiter().GetResult();
        }

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