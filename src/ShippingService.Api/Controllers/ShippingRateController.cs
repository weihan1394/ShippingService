using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingService.Core.Services;

namespace ShippingService.Api.Controllers
{

    [Route("api/shipping")]
    public class ShippingRateController : ApiControllerBase
    {
        private readonly IShippingRateService _shippingRateService;

        public ShippingRateController(IShippingRateService shippingRateService)
        {
            _shippingRateService = shippingRateService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getShippingRate()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("https://api.ipify.org/");

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // upload file(s) to server that palce under path: rootDirectory/subDirectory
        [HttpPost("upload")]
        public IActionResult UploadShippingRate([FromForm(Name = "files")] List<IFormFile> files, string subDirectory)
        {
            try
            {
                _shippingRateService.SaveFile(files, subDirectory);

                return Ok(new { files.Count, Size = _shippingRateService.SizeConverter(files.Sum(f => f.Length)) });
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}
