﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Mahc_Final.DBContext;
using Mahc_Final.ViewModels;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Mahc_Final.Controllers
{
    public class CareersController : Controller
    {

        static HttpClient client = new HttpClient();

        
        private HospitalContext db = new HospitalContext();

        // GET: Careers
        public async Task<ActionResult> Index()
        {
            Careers OppApp = new Careers() ;

            OppApp.opportunities = (from o in db.Jobs
                                    orderby o.Date_last_modified descending
                                    select o).Take(5).ToList();
            OppApp.applications = db.Job_applications.OrderByDescending(a => a.Date).Take(5).ToList();

            
            var responseString = await client.GetStringAsync("http://mmgnr1keg7.execute-api.us-east-1.amazonaws.com/prod/api/feature/careers");
            //developer = await GetDeveloperAsync("careers");
            dynamic developer = JObject.Parse(responseString);
            ViewBag.Developer=(string)developer["name"] ;
            return View("Admin/Index",OppApp);
        }
    }
}