using MinecraftServerRCON;
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
                return View();
            }
            else
            {
              return  Redirect("Account/Login");
            }
        }

        [HttpPost]
        public string CallRcon(string commandRcon)
        {
            var answer = "";

            try
            {
                using (var rcon = RCONClient.INSTANCE)
                {
                    rcon.setupStream("192.168.1.15", password: "myofrene");
                    answer = rcon.sendMessage(RCONMessageType.Command, commandRcon);

                    if(rcon.isInit == false)
                    {
                        answer = "Server is offline";
                    }
                    else if(rcon.ErrorMsg.Length>0)
                    {
                        answer = rcon.ErrorMsg;
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
    }
}