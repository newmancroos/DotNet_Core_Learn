using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asp_Net_MVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                    //ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found.";
                    //ViewBag.path = statusCodeResult.OriginalPath;
                    //ViewBag.QS = statusCodeResult.OriginalQueryString;
                    logger.LogWarning($"404 Error occured, path = {statusCodeResult.OriginalPath} and Query string {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exeptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exeptionDetails.Path;
            //ViewBag.ExceptionMessage = exeptionDetails.Error.Message;
            //ViewBag.Stacktrace = exeptionDetails.Error.StackTrace;
            logger.LogError($"The Path {exeptionDetails.Path} threw an exception {exeptionDetails.Error}");
            return View("Error");
        }
    }
}
