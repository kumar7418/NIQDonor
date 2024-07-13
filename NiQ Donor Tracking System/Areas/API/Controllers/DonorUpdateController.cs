using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using DonorTracking.Data;
using NiQ_Donor_Tracking_System.API.Models;
using Swashbuckle.Swagger.Annotations;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using NiQ_Donor_Tracking_System.API.Controllers;
using System.Timers;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Text;
using System.Security.Policy;

namespace NiQ_Donor_Tracking_System.Areas.API.Controllers
{
    [RoutePrefix("api/donorupdate")]
    public class DonorUpdateController: NiqController
    {
        //public IDonorUpdateRepository _donorRepository = new IDonorUpdateRepository();

        private readonly IDonorUpdateRepository _donorRepository;
        string strPattern = "[' \"]";
        string BaseUrl ="https://stage-leche.odoo.com/";
        public DonorUpdateController(IDonorUpdateRepository donorRepositoryS)
        {
            
            _donorRepository = donorRepositoryS;
        }

        [HttpPost]
        [Route("application")]
        [SwaggerResponse(HttpStatusCode.OK, "Donors Application", typeof(Application))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Donr Application found", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error occured", typeof(string))]
        public IHttpActionResult Application([FromBody] Application Application) 
        {
            Reponse _resp= new Reponse();
            try
            {
                if (Application != null && 
                    !string.IsNullOrEmpty(Application.FirstName) && !string.IsNullOrEmpty(Application.LastName) && !string.IsNullOrEmpty(Application.EmailAddress) && !string.IsNullOrEmpty(Application.PhoneNumber)
                    && Application.FirstName.Trim().Length> 0 && Application.LastName.Trim().Length > 0 && Application.EmailAddress.Trim().Length > 0 && Application.PhoneNumber.Trim().Length > 0
                    && !string.IsNullOrEmpty(Application.MailingAddress1) && !string.IsNullOrEmpty(Application.MailingCity) && !string.IsNullOrEmpty(Application.MailingState) && !string.IsNullOrEmpty(Application.MailingCountry) && !string.IsNullOrEmpty(Application.MailingZipCode)
                    )
                {
                    string DonorID = _donorRepository.CreateApplication(Application);
                    if (DonorID != null)
                    {

                        _resp.ResponseCode = "200";
                        _resp.Message = $" Application created Successfully";
                        _resp.Data = DonorID;
                    }
                    else
                    {
                        _resp.ResponseCode = "500";
                        _resp.Message = $"Application not created";
                    }
                }
                else{
                    _resp.ResponseCode = "500";
                    _resp.Message = $"Request is not in correct formate";
                }

            }
            catch (Exception ex )
            {
                _resp.ResponseCode= "500";
                _resp.Message = $"An error occured getting donor application:"+ex.Message;
            }

            return Ok(_resp);

        }

        [HttpPost]
        [Route("milkkits")]
        [SwaggerResponse(HttpStatusCode.OK, "MilkKits", typeof(MilkKits))]
        [SwaggerResponse(HttpStatusCode.NotFound, "MilkKits not found", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error occured", typeof(string))]
        public IHttpActionResult MilkKits([FromBody] MilkKits MilkKits)
        {
            Reponse _resp = new Reponse();
            try
            {

                if (MilkKits != null && !string.IsNullOrEmpty(MilkKits.DonorID) && MilkKits.DonorID.Trim().Length > 0 
                    && MilkKits.MilkKitStatus>=0  && MilkKits.MilkKitStatus <=6  && MilkKits.NumberOfKits>0 )
                {
                   List<string> noofkits = _donorRepository.CreateMilkKits(MilkKits);
                    if (noofkits.Count>0)
                    {

                        _resp.ResponseCode = "200";
                        _resp.Message = $" MilkKits created Successfully";
                        _resp.Data = noofkits;
                    }
                    else
                    {
                        _resp.ResponseCode = "500";
                        _resp.Message = $"MilkKits not created";
                    }
                }
                else
                {
                    _resp.ResponseCode = "500";
                    _resp.Message = $"Request is not in correct formate";
                }

            }
            catch (Exception ex)
            {
                _resp.ResponseCode = "500";
                _resp.Message = $"An error occured getting donor MilkKits:" + ex.Message;
            }

            return Ok(_resp);

        }

        [HttpPost]
        [Route("MilkKitsupdate")]
        [SwaggerResponse(HttpStatusCode.OK, "MilkKitsupdate address", typeof(MilkKitsupdate))]
        [SwaggerResponse(HttpStatusCode.NotFound, "MilkKitsupdate not found", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error occured", typeof(string))]
        public IHttpActionResult MilkKitsupdate([FromBody] MilkKitsupdate MilkKitsupdate)
        {
            Reponse _resp = new Reponse();
            try
            {
                if (MilkKitsupdate != null)
                {
                    // Serialize the data to JSON
                    var json = JsonConvert.SerializeObject(MilkKitsupdate);

                    _resp = callApi("donor/MilkKit", "POST", json); /// need to test 
                }
                else
                {
                    _resp.ResponseCode = "500";
                    _resp.Message = $"Request is not in correct formate";
                }
            }
            catch (Exception ex)
            {
                _resp.ResponseCode = "500";
                _resp.Message = $"An error occured getting MilkKitsupdate:" + ex.Message;
            }

            return Ok(_resp);

        }

        [HttpPost]
        [Route("BloodWork")]
        [SwaggerResponse(HttpStatusCode.OK, "BloodWork", typeof(Application))]
        [SwaggerResponse(HttpStatusCode.NotFound, "BloodWork not found", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error occured", typeof(string))]
        public IHttpActionResult BloodWork([FromBody] BloodWork BloodWork)
        {
            Reponse _resp = new Reponse();
            try
            {
                if (BloodWork != null)
                {
                    // Serialize the data to JSON
                    var json = JsonConvert.SerializeObject(BloodWork);
                    _resp = callApi("donor/bloodwork", "POST", json); /// need to test 
                }
                else
                {
                    _resp.ResponseCode = "500";
                    _resp.Message = $"Request is not in correct formate";
                }
            }
            catch (Exception ex)
            {
                _resp.ResponseCode = "500";
                _resp.Message = $"An error occured getting MilkKitsupdate:" + ex.Message;
            }

            return Ok(_resp);


        }
        [NonAction]
        public Reponse  callApi(string URL,string Method,string Req)
        {
            Reponse _resp = new Reponse();
            try
            {
                if (!string.IsNullOrEmpty(URL) && !string.IsNullOrEmpty(Method) && !string.IsNullOrEmpty(Req))
                {
                    using (var client = new HttpClient())
                    {
                        // Set the base address of the API
                        client.BaseAddress = new Uri(BaseUrl);

                        // Set any headers if required
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer "+"asdasfdas");

                        // Serialize the data to JSON
                        var content = new StringContent(Req, Encoding.UTF8, "application/json");

                        // Make a POST request
                        HttpResponseMessage response = client.PostAsync(URL, content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as a string
                            string data =  response.Content.ReadAsStringAsync().Result;
                            _resp.ResponseCode = "200";
                            _resp.Message = $" data updated successfully";
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode}");

                            _resp.ResponseCode = "500";
                            _resp.Message = $" "+ response.StatusCode;
                        }
                    }


                   
                }
                else
                {
                    _resp.ResponseCode = "500";
                    _resp.Message = $" request parameter not correct";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return _resp;
        }

    }

   
}