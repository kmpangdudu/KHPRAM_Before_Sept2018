﻿using BIZ.EmailMe;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Filters;
namespace WebApi.Controllers
{
    /// <summary>
    /// Sending user selected favourite’s resources list to an email account.
    /// </summary>
    [AuthorizeIPAddress]
    [FilterIP]
    public class EmailController : ApiController
    {
        emailSenderServices es = new emailSenderServices();



        /// <summary>
        /// Sending user selected favourite's  resources from KHP's public SMTP. the email receiver should do email account format validation before sending.
        /// </summary>
        /// <param name="_email">user's email account and his favourites resources list. Using "，" (comma) to separate each receivers if you have multiple receivers.
        /// JSON format. 
        /// {"lang":"en","receiver":"user1@example.com; user2@example.com;", "body":"my favourites resources list:<br/> 1. Kids help phone; 2. Ontario Mental Health services."}</param>
        /// <returns>successful sent return "OK", otherwise will say "Failed" </returns>
        [ResponseType(typeof(email))]
        [ActionName("Send")]
        [Route("api/v2/email/send")]
        [HttpPost]
        public HttpResponseMessage Send([FromBody]email _email)
        {
             
            string host = Properties.Settings.Default.host;
            int port = Properties.Settings.Default.port;
            bool ssl = Properties.Settings.Default.ssl;
            string loginName = Properties.Settings.Default.loginname;
            string pwd = Properties.Settings.Default.pwd;
            string sendfrom = Properties.Settings.Default.sendfrom;
            string displayName = Properties.Settings.Default.displayname;
            string subject = _email.lang == "fr" ? Properties.Settings.Default.subject_fr : Properties.Settings.Default.subject;
            //string subject =   Properties.Settings.Default.subject;
            bool result = false;





            try
            {
                result = es.sendfromInternal(host, port, ssl, loginName, pwd, sendfrom, 
                    displayName, _email.receiver, subject, _email.body);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            if (result) 
            {
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent("\"status\":\"OK\"", Encoding.UTF8, "application/json");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            else
            {
                var response = this.Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent("\"status\":\"Failed\"", Encoding.UTF8, "application/json");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response; 
            }

        }



        /// <summary>
        /// Sending email by using Gmail public SMTP for testing. the email receiver should do email account format validation before sending.
        /// </summary>
        /// <param name="_email">user's email account and his favourites resources list. Using ", " (comma) to separate each receivers if you have multiple receivers.
        /// JSON format. 
        /// {"receiver":"user1@example.com; user2@example.com;", "body":"my favourites resources list:<br/> 1. Kids help phone; 2. Ontario Mental Health services."}</param>
        /// <returns>successful sent return "OK", otherwise will say "Failed" </returns>
        [ResponseType(typeof(email))]
        [ActionName("Send")]
        [HttpPost]
        [Route("api/v2/email/gmail")]
        public HttpResponseMessage Gsender([FromBody] email _email)

        {
            string subject = Properties.Settings.Default.subject;
            bool result = false;



            string _account = _email.receiver;
            string _body = _email.body;


            try
            {
                result = es.sendfromGmail(_account, subject, _body);
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

                

            if (result)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent("\"status\":\"OK\"", Encoding.UTF8, "application/json");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            else
            {
                var response = this.Request.CreateResponse(HttpStatusCode.NotFound);
                response.Content = new StringContent("\"status\":\"Failed\"", Encoding.UTF8, "application/json");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
        }
    }
}