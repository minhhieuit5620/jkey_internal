using JKEY_INTERNAL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace JKEY_INTERNAL.Controllers
{
    [Route("api/lang")]
    [ApiController]
    public class MultLangsController : ControllerBase
    {
        private readonly IStringLocalizer<ShareResource> _shareResource;
        public MultLangsController(IStringLocalizer<ShareResource> shareResource)
        {
            _shareResource = shareResource;
        }
        [HttpGet]
        [HttpGet]
        public IActionResult Get()
        {
            var allStrings = _shareResource.GetAllStrings();
            var result = new Dictionary<string, string>();
            foreach (var str in allStrings)
            {
                result[str.Name] = str.Value;
            }
            return Ok(result);
        }

    }
}
