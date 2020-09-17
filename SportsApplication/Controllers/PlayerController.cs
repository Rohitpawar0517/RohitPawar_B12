using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsApplication;

namespace SportsApplication.Controllers
{
    [Authorize(Roles = "Player")]
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult PlayerMessage()
        {
            ViewBag.list = TempData["mydata"];
            return View();
        }
    }
}