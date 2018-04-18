using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Sombra.Messaging.Requests;
using Sombra.Messaging.Responses;
using Sombra.Web.Areas.Development.Models;
using Sombra.Messaging.Infrastructure;

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
            var response = await _bus.RequestAsync(request);
            var logs = _mapper.Map<IEnumerable<LogViewModel>>(response.Logs);

            return View("View", logs);
        }
    }
}