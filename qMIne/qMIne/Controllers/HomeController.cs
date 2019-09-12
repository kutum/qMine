﻿using MinecraftServerRCON;
using qMine.Context;
using qMine.Models;
using qMineStat;
using Renci.SshNet;
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
        public  ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var serverCredentials = GetServerCredentials(User.Identity.Name);
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

        public  JsonResult GetStatus()
        {
            var serverCredentials = GetServerCredentials(User.Identity.Name);
            var mineStat = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port);

            return Json(mineStat, JsonRequestBehavior.AllowGet);
        }

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

        public string Start()
        {
            var serverCredentials = GetServerCredentials(User.Identity.Name);

            return SSHSend("service " + serverCredentials.SSHMinecraftServiceName + " start");
        }

        public string Stop()
        {
            var serverCredentials = GetServerCredentials(User.Identity.Name);

            return SSHSend("service " + serverCredentials.SSHMinecraftServiceName + " stop");
        }

        public string SSHSend(string Command)
        {
            if(Command == null)
            {
                return "SSH Error: Empty command";
            }

            var serverCredentials = GetServerCredentials(User.Identity.Name);

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

        #region Async
        public async Task<JsonResult> GetStatusAsync()
        {
            var serverCredentials = await GetServerCredentialsAsync(User.Identity.Name);
            var mineStat = new MineStat(serverCredentials.IP, (ushort)serverCredentials.Port);

            return Json(mineStat, JsonRequestBehavior.AllowGet);
        }

        public async Task<ServerCredentials> GetServerCredentialsAsync(string UserName)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.ServerCredentials.Where(x => x.Name == UserName).FirstOrDefaultAsync();
            }
        }

        [HttpPost]
        public async Task<string> CallRconAsync(string commandRcon)
        {
            var answer = "";

            try
            {
                using (var rcon = RCONClient.INSTANCE)
                {
                    var serverCredentials = await GetServerCredentialsAsync(User.Identity.Name);

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
        #endregion
    }
}