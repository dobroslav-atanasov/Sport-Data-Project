﻿namespace SportData.Services.Data.SportDataDb;

using System.Collections.Generic;

using SportData.Data.Models.Entities.SportData;
using SportData.Data.Repositories;
using SportData.Data.ViewModels.Countries;
using SportData.Services.Data.SportDataDb.Interfaces;
using SportData.Services.Mapper.Extensions;

public class CountriesService : ICountriesService
{
    private readonly SportDataRepository<Country> repository;

    public CountriesService(SportDataRepository<Country> repository)
    {
        this.repository = repository;
    }

    public async Task<Country> AddOrUpdateAsync(Country country)
    {
        var dbCountry = await repository.GetAsync(x => x.Code == country.Code);
        if (dbCountry == null)
        {
            await repository.AddAsync(country);
            await repository.SaveChangesAsync();
        }
        else
        {
            var isUpdated = dbCountry.IsUpdated(country);
            if (isUpdated)
            {
                repository.Update(dbCountry);
                await repository.SaveChangesAsync();
            }

            country = dbCountry;
        }

        return country;
    }

    public async Task<IEnumerable<CountryViewModel>> GetAllAsync()
    {
        var countries = this.repository
            .AllAsNoTracking()
            .To<CountryViewModel>();

        return countries;
    }

    public async Task<Country> GetAsync(string code)
    {
        var country = await repository.GetAsync(x => x.Code == code);
        return country;
    }
}