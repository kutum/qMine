﻿using MinecraftServerRCON;
using qMine.Models;
using qMineStat;
using Renci.SshNet;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace qMine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (Request.IsAuthenticated)
            {
                try
                {
                    var credentials = new ServerCredentials();

                    var serverCredentials = credentials.GetServerCredentials(User.Identity.Name);
                    if (serverCredentials == null)
                        serverCredentials = credentials.CreateStock(User.Identity.Name);

                    var mineStat = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port);
                    var statusViewModel = new StatusViewModel(serverCredentials, mineStat);

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

        public JsonResult GetStatus(ServerCredentials serverCredentials)
        {
            var statusViewModel =
                                    new StatusViewModel
                                    (
                                        serverCredentials,
                                        new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port),
                                        StatusText(serverCredentials)
                                    );
            return Json(statusViewModel, JsonRequestBehavior.AllowGet);
        }

        public string CallRcon(string commandRcon, ServerCredentials serverCredentials)
        {
            var answer = "";

            try
            {
                using (var rcon = RCONClient.INSTANCE)
                {
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

        public string Start(ServerCredentials serverCredentials)
        {
            return SSHSend("service " + serverCredentials.SSHMinecraftServiceName + " start",serverCredentials);
        }

        public string Stop(ServerCredentials serverCredentials)
        {
            return SSHSend("service " + serverCredentials.SSHMinecraftServiceName + " stop",serverCredentials);
        }

        public string StatusText(ServerCredentials serverCredentials)
        {
            var response = SSHSend("service " + serverCredentials.SSHMinecraftServiceName + " status", serverCredentials);
            return "<li class='list-group-item list-group-item-primary'>" + Regex.Replace(response.Substring(0, response.Length - 1), @"\t|\n|\r", "</li><li class='list-group-item list-group-item-secondary'>") + "</li>";
        }

        public string SSHSend(string Command, ServerCredentials serverCredentials)
        {
            if(Command == null)
            {
                return "SSH Error: Empty command";
            }

            if (serverCredentials.SSHPassword == null)
                return "SSH Error: Password is empty";

            ConnectionInfo ConnNfo = 
                new ConnectionInfo(serverCredentials.IP, serverCredentials.SSHPort, serverCredentials.SSHLogin,
                    new AuthenticationMethod[]{
                        new PasswordAuthenticationMethod(serverCredentials.SSHLogin,serverCredentials.SSHPassword)
                    }
             );

            try
            {
                using (var sshclient = new SshClient(ConnNfo))
                {
                    var response = "";

                    sshclient.Connect();
                    using (var cmd = sshclient.CreateCommand(Command))
                    {
                        cmd.Execute();
                        response = cmd.Result;
                    }
                    sshclient.Disconnect();

                    return response;
                }
            }
            catch (Exception e)
            {
                return "SSH Error: " + e.Message;
            }
        }
    }
}