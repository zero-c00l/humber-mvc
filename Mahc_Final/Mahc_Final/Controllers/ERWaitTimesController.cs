﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mahc_Final.DBContext;

namespace Mahc_Final.Controllers
{
    public class ERWaitTimesController: Controller
    {
        private readonly HospitalContext _db = new HospitalContext();        
       
        // GET: ERWaitTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ERWaitTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArrivalTime, TreatmentTime")] ERParam newEntry)
        {
            if (ModelState.IsValid)
            {
                _db.ERParams.Add(newEntry);
                _db.SaveChanges();
                CalculateNewERTime();
                return RedirectToAction("Create");
            }

            return View(newEntry);
        }

        public PartialViewResult GetCurrentTime()
        {
            CalculateNewERTime();
            var erWaitTime = _db.ERWaitTimes.First(x => x.Lock == "X");
            if (erWaitTime.UpdatedAt.AddHours(1) < DateTime.Now)
            {
                erWaitTime.UpdatedAt = DateTime.Now;
                erWaitTime.CurrentWaitTime = 0;
                // TODO(batuhan): Save to archive.
                _db.Entry(erWaitTime).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return PartialView("_ERWaitTimes", erWaitTime);
        }

        private void CalculateNewERTime()
        {
            var lastOneHour = DateTime.Now.AddHours(-1);
            var records = (from p in _db.ERParams where (p.ArrivalTime >= lastOneHour) select p);
            int recordCount = records.Count();
            if (recordCount == 0)
                return;

            int minutes = 0;
            foreach (var erParam in records)
            {
                minutes += (int)((erParam.TreatmentTime - erParam.ArrivalTime).TotalMinutes);
            }
            minutes = minutes / recordCount;

            var erEntity = _db.ERWaitTimes.Single(x => x.Lock == "X");
            erEntity.UpdatedAt = DateTime.Now;
            erEntity.CurrentWaitTime = minutes;
            erEntity.WaitingPatients = records.Count();

            _db.Entry(erEntity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
