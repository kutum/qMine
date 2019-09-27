using qMine.Models;
using qMineStat;
using System.Web.Mvc;
using System.Threading.Tasks;
using System;

namespace qMIne.Controllers
{
    public class MapController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var serverCredentials = await new ServerCredentials().GetServerCredentialsAsync(User.Identity.Name);

                    ViewBag.ServerUp = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port).ServerUp;
                    ViewBag.MapUrl = serverCredentials.MapUrl;

                    return View();
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