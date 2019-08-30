using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace DG_BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Project Manager, Developer, Submitter")]
    public class TicketNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //let one notification show all changes that are applied in an instance of an edit?
        //reflection: when looking at an object, find what "kind of object" i am, spin through the properties, and compare the changes to the properties


        // GET: TicketNotifications
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var ticketNotifications = db.TicketNotifications.Include(t => t.Reciever).Include(t => t.Sender).Include(t => t.Ticket);

            return View(ticketNotifications.ToList());
        }
        
        //GET : DeleteAll

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAll()
        {
            foreach (var notification in db.TicketNotifications)
            {
                db.TicketNotifications.Remove(notification);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        //GET: MyNotifications
        public ActionResult MyNotifications()
        {
            var me = User.Identity.GetUserId();
            var myNotifications = db.TicketNotifications.Where(notification => notification.RecieverId == me).ToList();

            return View("Index", myNotifications);
        }

        

        //POST: MarkAsRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsRead(int id)
        {
            //grab the notification passed in the form
            var notification = db.TicketNotifications.Find(id);
            //change the read status to true when the "form" is submitted
            notification.ReadStatus = true;
            //save changes in db so it doesnt show in that list again
            db.SaveChanges();

            return RedirectToAction("Dashboard", "Home");
        }


        // GET: TicketNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Create
        public ActionResult Create()
        {
            ViewBag.RecieverId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.SenderId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            return View();
        }

        // POST: TicketNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,RecieverId,SenderId,Created,NotificationBody,ReadStatus")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {
                db.TicketNotifications.Add(ticketNotification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RecieverId = new SelectList(db.Users, "Id", "FirstName", ticketNotification.RecieverId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "FirstName", ticketNotification.SenderId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotification.TicketId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecieverId = new SelectList(db.Users, "Id", "FirstName", ticketNotification.RecieverId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "FirstName", ticketNotification.SenderId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotification.TicketId);
            return View(ticketNotification);
        }

        // POST: TicketNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,RecieverId,SenderId,Created,NotificationBody,ReadStatus")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {


                db.Entry(ticketNotification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecieverId = new SelectList(db.Users, "Id", "FirstName", ticketNotification.RecieverId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "FirstName", ticketNotification.SenderId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotification.TicketId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            if (ticketNotification == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotification);
        }

        // POST: TicketNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketNotification ticketNotification = db.TicketNotifications.Find(id);
            db.TicketNotifications.Remove(ticketNotification);
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
