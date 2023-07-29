using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SnapShop.Infrastructure.Interface;

namespace SnapShop.Controllers
{
    [ApiController]
    [Route("/")]
    public class IndexController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IStringLocalizer<IndexController> _localizer;

        public IndexController(IAssignmentRepository assignmentRepository, IStringLocalizer<IndexController> localizer)
        {
            _assignmentRepository = assignmentRepository;
            _localizer = localizer;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok(_localizer["Greeting"]);
        }
    }
}
