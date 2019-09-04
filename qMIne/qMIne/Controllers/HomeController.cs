﻿using MinecraftServerRCON;
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
            return View();
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

                    if(rcon.ErrorMsg.Length>0)
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}