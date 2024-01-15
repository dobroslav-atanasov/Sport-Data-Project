namespace SportData.WebAPI;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using SportData.Common.Constants;
using SportData.Data.Contexts;
using SportData.Data.Converters.Countries;
using SportData.Data.Factories;
using SportData.Data.Factories.Interfaces;
using SportData.Data.Repositories;
using SportData.Services;
using SportData.Services.Data.CrawlerStorageDb;
using SportData.Services.Data.CrawlerStorageDb.Interfaces;
using SportData.Services.Data.SportDataDb;
using SportData.Services.Data.SportDataDb.Interfaces;
using SportData.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        var app = builder.Build();
        Configure(app);
        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddLogging(config =>
        {
            config.AddConfiguration(builder.Configuration.GetSection(AppGlobalConstants.LOGGING));
            config.AddConsole();
            config.AddLog4Net(builder.Configuration.GetSection(AppGlobalConstants.LOG4NET_CORE).Get<Log4NetProviderOptions>());
        });

        var crawlerStorageDbOptions = new DbContextOptionsBuilder<CrawlerStorageDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.CRAWLER_STORAGE_CONNECTION_STRING))
            .Options;

        var sportDataDbOptions = new DbContextOptionsBuilder<CrawlerStorageDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.SPORT_DATA_CONNECTION_STRING))
            .Options;

        var olympicGamesDbOptions = new DbContextOptionsBuilder<OlympicGamesDbContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.OLYMPIC_GAMES_CONNECTION_STRING))
            .Options;

        var dbContextFactory = new DbContextFactory(crawlerStorageDbOptions, olympicGamesDbOptions);
        builder.Services.AddSingleton<IDbContextFactory>(dbContextFactory);

        builder.Services.AddDbContext<SportDataDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.SPORT_DATA_CONNECTION_STRING));
        });

        builder.Services.AddDbContext<CrawlerStorageDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(AppGlobalConstants.CRAWLER_STORAGE_CONNECTION_STRING));
        });

        builder.Services.AddScoped(typeof(SportDataRepository<>));

        builder.Services.AddScoped<IZipService, ZipService>();
        builder.Services.AddScoped<IRegExpService, RegExpService>();
        builder.Services.AddScoped<IHttpService, HttpService>();
        builder.Services.AddScoped<IMD5Hash, MD5Hash>();
        builder.Services.AddScoped<INormalizeService, NormalizeService>();
        builder.Services.AddScoped<IOlympediaService, OlympediaService>();
        builder.Services.AddScoped<IDateService, DateService>();

        builder.Services.AddScoped<ICrawlersService, CrawlersService>();
        builder.Services.AddScoped<IGroupsService, GroupsService>();
        builder.Services.AddScoped<ILogsService, LogsService>();
        builder.Services.AddScoped<ICountriesService, CountriesService>();
        builder.Services.AddScoped<ICountriesService, CountriesService>();

        builder.Services.AddScoped<CountryDataConverter>();
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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}