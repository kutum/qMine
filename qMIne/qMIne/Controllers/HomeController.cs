using MinecraftServerRCON;
using qMIne.Context;
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

                    using (var context = new ApplicationDbContext())
                    {
                        var serverCredentials = context.ServerCredentials.FirstOrDefault(x => x.Name == User.Identity.Name);

                        if(serverCredentials != null)
                        {
                            rcon.setupStream(serverCredentials.IP, password: serverCredentials.Password);
                            answer = rcon.sendMessage(RCONMessageType.Command, commandRcon);

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