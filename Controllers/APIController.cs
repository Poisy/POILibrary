using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using POILibrary.Models;
using Newtonsoft.Json;
using POILibrary.Services;


namespace ASPMVC.Controllers
{
    [Authorize]
    public class APIController
    {
        readonly LibraryService _service;

        public APIController(LibraryService service)
        {
            _service = service;
        }

        public IActionResult Books()
        {
            var books = _service.GetAllBooks().Select(b => b.ToObject().ToString());
            var result = new JsonResult(books);

            return result;
        }

        public IActionResult Exist(string title)
        {
            string state = (_service.GetAllBooks().Any(b => b.Title == title)).ToString();
            var result = new JsonResult(new { State = state });

            return result;
        }
    }
}