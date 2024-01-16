namespace SportData.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using SportData.Common.Constants;
using SportData.Data.Converters.Countries;
using SportData.Data.Models.Requests.Converters;

public class ConverterController : BaseController
{
    private readonly CountryDataConverter countryDataConverter;

    public ConverterController(CountryDataConverter countryDataConverter)
    {
        this.countryDataConverter = countryDataConverter;
    }

    [HttpPost]
    [Route(RouteConstants.CONVERTER_START)]
    public async Task<IActionResult> Start(InputConverterModel inputModel)
    {
        await this.countryDataConverter.ConvertAsync(inputModel.Name);
        return this.Ok();
    }
}