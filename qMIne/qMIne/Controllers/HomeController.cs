using MinecraftServerRCON;
using qMIne.Context;
using qMIne.Models;
using qMineStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qMIne.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var serverCredentials = GetServerCredentials(User.Identity.Name);

                    return View(new StatusViewModel( new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port)));
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

        public JsonResult GetStatus()
        {
            var serverCredentials = GetServerCredentials(User.Identity.Name);
            return Json(new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CallRcon(string commandRcon)
        {
            var answer = "";

            try
            {
                using (var rcon = RCONClient.INSTANCE)
                {
                        var serverCredentials = GetServerCredentials(User.Identity.Name);

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

        public ServerCredentials GetServerCredentials(string UserName)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.ServerCredentials.FirstOrDefault(x => x.Name == UserName);
            }
        }
    }
}