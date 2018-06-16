﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sombra.Web.Infrastructure.Messaging;

namespace Sombra.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICachingBus _bus;
        private readonly IMapper _mapper;

        public DashboardController(ICachingBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("goed-doel-aanmelden")]
        public IActionResult SignUpCharity()
        {
            return View();
        }

        [HttpGet("deel-jouw-verhaal")]
        public IActionResult CreateStory()
        {
            return View();
        }
    }
}