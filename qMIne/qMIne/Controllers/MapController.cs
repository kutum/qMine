using qMine.Models;
using qMineStat;
using System;
using System.Web.Mvc;

namespace qMIne.Controllers
{
    public class MapController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var serverCredentials = new ServerCredentials().GetServerCredentials(User.Identity.Name);


                    return View(new MapView
                    {
                        ServerUp = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port).ServerUp,
                        MapUrl = serverCredentials.MapUrl
                    });
                }
                catch (Exception e)
                {
                    return ViewBag.Error = e.Message;
                }

            }
            else
            {
                return Redirect("~/Account/Login");
            }
        }
    }
}