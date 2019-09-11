using MinecraftServerRCON;
using qMine.Context;
using qMine.Models;
using qMineStat;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace qMine.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var serverCredentials = await GetServerCredentials(User.Identity.Name);
                    var mineStat = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port);
                    var statusViewModel = new StatusViewModel(mineStat);
                    return View(statusViewModel);
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = "Error: " + ex.Message;

                    return Redirect("~/Account/Login");
                }

            }
            else
            {
                return Redirect("~/Account/Login");
            }
        }

        public async Task<JsonResult> GetStatus()
        {
            var serverCredentials = await GetServerCredentials(User.Identity.Name);
            var mineStat = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port);

            return Json(mineStat, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<string> CallRcon(string commandRcon)
        {
            var answer = "";

            try
            {
                using (var rcon = RCONClient.INSTANCE)
                {
                        var serverCredentials =  await GetServerCredentials(User.Identity.Name);

                        if (serverCredentials != null)
                        {
                            rcon.setupStream(serverCredentials.IP, serverCredentials.RconPort, password: serverCredentials.Password);
                            answer = rcon.sendMessage(RCONMessageType.Command, commandRcon).RemoveColorCodes();

                            if (rcon.isInit == false)
                            {
                                answer = "Error: Server is offline!";
                            }
                            else if (rcon.ErrorMsg.Length > 0)
                            {
                                answer = rcon.ErrorMsg;
                            }
                        }
                        else
                        {
                            answer = "Error: Configure connection!";
                        }
                    
                }
            }
            catch (Exception ex)
            {
                answer = ex.Message;
            }

            return answer;
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public async Task<ServerCredentials> GetServerCredentials(string UserName)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.ServerCredentials.Where(x => x.Name == UserName).FirstOrDefaultAsync();
            }
        }
    }
}