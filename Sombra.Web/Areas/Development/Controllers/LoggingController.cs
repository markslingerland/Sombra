using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Infrastructure;
using Sombra.Messaging.Requests;
using Sombra.Web.Areas.Development.Models;

namespace Sombra.Web.Areas.Development.Controllers
{
    [Area("Development")]
    public class LoggingController : Controller
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public LoggingController(IBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LogsQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LogsQuery query)
        {
            var request = _mapper.Map<LogRequest>(query);
            var logs = await _bus.RequestAsync(request);

            return View();
        }
    }
}