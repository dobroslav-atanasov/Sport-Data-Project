namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using SportData.Common.Constants;
using SportData.Data.Converters.Countries;

public class ConverterController : BaseController
{
    private readonly CountryDataConverter countryDataConverter;

    public ConverterController(CountryDataConverter countryDataConverter)
    {
        this.countryDataConverter = countryDataConverter;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await this.countryDataConverter.ConvertAsync(ConverterConstants.COUNTRY_CONVERTER);
        return this.Ok("test");
    }
}