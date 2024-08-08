 //Interneuron synapse

//Copyright(C) 2024 Interneuron Limited

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

//See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.If not, see<http://www.gnu.org/licenses/>.
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace SynapsePlatformServices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IConfiguration config;
        private string host;
        private int port;
        private string username;
        private string password;
        private bool useCredentials;
        private bool useSSL;

        public EmailController(IConfiguration iConfig)
        {
            config = iConfig;

            host = config.GetValue<string>("Settings:EmailSetting:host");
            port = config.GetValue<int>("Settings:EmailSetting:port");
            username = config.GetValue<string>("Settings:EmailSetting:username");
            password = config.GetValue<string>("Settings:EmailSetting:password");
            useCredentials = config.GetValue<bool>("Settings:EmailSetting:useCredentials");
            useSSL = config.GetValue<bool>("Settings:EmailSetting:useSSL");

        }

        [HttpPost]
        [Route("SendEmail")]
        public void SendEmail([FromBody] string value)
        {
            try
            {
                Models.EmailModel eModel = new Models.EmailModel();
                eModel = JsonConvert.DeserializeObject<Models.EmailModel>(value);

                SendEmailSMTP(eModel);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        private void SendEmailSMTP(Models.EmailModel eModel)
        {
            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;

            //If username password is not sent, use default
            if (useCredentials)
            {
                if (String.IsNullOrEmpty(eModel.Username) || String.IsNullOrEmpty(eModel.password))
                {
                    client.Credentials = new NetworkCredential(username, password);
                }
                else
                {
                    client.Credentials = new NetworkCredential(eModel.Username, eModel.password);

                }
            }
           
            MailAddress from = new MailAddress(eModel.emailFrom, eModel.fromName, System.Text.Encoding.UTF8);
            if (useSSL)
            {
                client.EnableSsl = true;
            }
            MailMessage message = new MailMessage();
            message.From = from;
            for (int i = 0; i < eModel.emailTo.Length; i++)
            {
                message.To.Add(eModel.emailTo[i]);
            }
            message.Body = eModel.body;
            message.IsBodyHtml = true;
            message.Subject = eModel.subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            try
            {
                client.Send(message);
                
            }
            catch (Exception e)
            { 
                throw e;
            }
            finally
            {
                client.Dispose();
                message.Dispose();

            }

        }
    }
}
