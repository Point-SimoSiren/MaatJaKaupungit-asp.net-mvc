using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaatJaKaupungit.Models;
using Newtonsoft.Json;

namespace MaatJaKaupungit.Controllers
{
    public class KaupungitController : Controller
    {
        private MaatJaKaupungitEntities db = new MaatJaKaupungitEntities();

        // GET: Kaupungit
        public ActionResult Index()
        {
            var kaupungit = db.Kaupungit.Include(k => k.Maat);
            return View(kaupungit.ToList());
        }

        // POST: Metodi jolla luodaan uusi maa vaikka ollaankin kaupungit kontrollerissa
        // Tämä metodi vastaanottaa olion jossa on kaksi kenttää. Idtä ei kuulukaan yleensä antaa
        public string CreateMaaFromData(Maat maa)
        {
            try
            {
                // Jostain syystä ohjelma vaati että annetaan id mutta tietokannan kumminkin olisi tarkoitus luoda id itse,
                // eli ei kannata koodissa generoida jos ohjelma vaan taipuu siihen että annetaan tieto ilman id:tä.
                Random generoituId = new Random();
                int uusiId = generoituId.Next(1000, 500000);
                maa.maaId = uusiId;

                db.Maat.Add(maa);
                db.SaveChanges();
                return "Luotu uusi maa" + maa.maaNimi;
            }
            catch (Exception ex)
            {
                // Tässä vähän säädetty errorien esitystapaa
                return ex.GetType().Name + " Tarkempaa tietoa: " +  ex.Message;
            }
        }


        // GET: Kaupungit/Create
        public ActionResult Create()
        {
            ViewBag.maaId = new SelectList(db.Maat, "maaId", "maaNimi");
            return View();
        }

        // POST: Kaupungit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kaupunkiId,kaupunginNimi,asukasluku,maaId")] Kaupungit kaupungit)
        {
            if (ModelState.IsValid)
            {
                db.Kaupungit.Add(kaupungit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maaId = new SelectList(db.Maat, "maaId", "maaNimi", kaupungit.maaId);
            return View(kaupungit);
        }

        // GET: Kaupungit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kaupungit kaupungit = db.Kaupungit.Find(id);
            if (kaupungit == null)
            {
                return HttpNotFound();
            }
            ViewBag.maaId = new SelectList(db.Maat, "maaId", "maaNimi", kaupungit.maaId);
            return View(kaupungit);
        }

        // POST: Kaupungit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kaupunkiId,kaupunginNimi,asukasluku,maaId")] Kaupungit kaupungit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kaupungit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maaId = new SelectList(db.Maat, "maaId", "maaNimi", kaupungit.maaId);
            return View(kaupungit);
        }

        // GET: Kaupungit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kaupungit kaupungit = db.Kaupungit.Find(id);
            if (kaupungit == null)
            {
                return HttpNotFound();
            }
            return View(kaupungit);
        }

        // POST: Kaupungit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kaupungit kaupungit = db.Kaupungit.Find(id);
            db.Kaupungit.Remove(kaupungit);
            db.SaveChanges();
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
