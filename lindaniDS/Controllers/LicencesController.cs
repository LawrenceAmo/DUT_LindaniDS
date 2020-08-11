﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lindaniDS.Models;

namespace lindaniDS.Controllers
{
    public class LicencesController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: Licences
        public async Task<ActionResult> Index()
        {
           // ViewBag.userId = User.Identity.GetUserId();
            var licences = db.Licences;
            return View(await licences.ToListAsync());
        }
        public async Task<ActionResult> AdminView()
        {
            // ViewBag.userId = User.Identity.GetUserId();
            var licences = db.Licences;
            return View(await licences.ToListAsync());
        }

        // GET: Licences/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licence licence = await db.Licences.FindAsync(id);
            if (licence == null)
            {
                return HttpNotFound();
            }
            return View(licence);
        }

        // GET: Licences/Create
        public ActionResult Create()
        {
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name");
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Licences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LearnerID,UserID,Province,City,Surbub,Street,ZipCode,Phone,IDNum,Location,BookingDate,Photo,Picture,Code")] Licence licence, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(upload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    upload.SaveAs(_path);

                    licence.Picture = _FileName;
                }
               /// licence.LearnerID = Session[""]
                db.Licences.Add(licence);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", licence.PackageID);
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", licence.UserID);
            return View(licence);
        }

        // GET: Licences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licence licence = await db.Licences.FindAsync(id);
            if (licence == null)
            {
                return HttpNotFound();
            }
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", licence.PackageID);
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", licence.UserID);
            return View(licence);
        }

        // POST: Licences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LearnerID,Province,City,Surbub,Street,ZipCode,Phone,IDNum,Location,BookingDate,Photo,Picture,Code")] Licence licence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", licence.PackageID);
          //  ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", licence.UserID);
            return View(licence);
        }

        // GET: Licences/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licence licence = await db.Licences.FindAsync(id);
            if (licence == null)
            {
                return HttpNotFound();
            }
            return View(licence);
        }

        // POST: Licences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Licence licence = await db.Licences.FindAsync(id);
            db.Licences.Remove(licence);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
