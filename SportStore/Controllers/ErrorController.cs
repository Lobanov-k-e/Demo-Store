﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportStore.WebUi.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error() => View();
    }
}