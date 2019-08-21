﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DG_BugTracker.Models;
using DG_BugTracker.Helpers;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace DG_BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private ProjectHelper projectHelper = new ProjectHelper();
        private AccessHelper accessHelper = new AccessHelper();

        

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.ToList();
            return View(tickets.ToList());
        }

        //[Authorize (Roles = "Project Manager, Developer, Submitter")]
        public ActionResult MyIndex()
        {
            var myTickets = accessHelper.GetMyTickets();


            return View("Index", myTickets);
        }

        public ActionResult ArchiveIndex()
        {

            var archivedTickets = db.Tickets.Where(tkt => tkt.TicketStatus.Name == "Archived").ToList();

            return View("Index", archivedTickets);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {

            Ticket ticket = db.Tickets.Find(id);
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ticket == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if(accessHelper.CanSeeDetails(ticket))
            {
                return View(ticket);
            }
            else
            {
                return RedirectToAction("NotAllowedTicket", "Home");
            }
        }

        // GET: Tickets/Create
        //[Authorize (Roles = "Submitter")]
        public ActionResult Create()
        {
            var myProjects = projectHelper.ListUserProjects(User.Identity.GetUserId());

            ViewBag.ProjectId = new SelectList(myProjects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses.Where(status => status.Name != "Archived"), "Id", "Name");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize (Roles = "Submitter")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Updated,TicketTypeId,ProjectId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);

            if (ModelState.IsValid)
            {
                //automatic new status
                ticket.TicketStatusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "New").Id;
                //automate created field
                ticket.Created = DateTimeOffset.Now;

                //automate submitter field
                ticket.OwnerUserId = User.Identity.GetUserId();
                

                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            //variable to only show Devs in assign field
            var allDevs = roleHelper.UsersInRole("Developer");

            ViewBag.AssignedToUserId = new SelectList(allDevs, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,TicketTypeId,ProjectId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            

            if (ModelState.IsValid)
            {
                //store old tickets value for comparison with logic of helper methods
                var origin = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);

                ticket.Updated = DateTimeOffset.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                //calls notificationhelper to see which notification needs to be sent for the assignment.
                NotificationHelper.ManageNotifications(origin, ticket);
                HistoryHelper.CreateHistoryEntries(origin, ticket);

                return RedirectToAction("Index");
            }

            //variable to only show Devs in assign field
            var allDevs = roleHelper.UsersInRole("Developer");

            ViewBag.AssignedToUserId = new SelectList(allDevs, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        //
        // GET: Tickets/AssignTicket

        public ActionResult AssignTicket(int? id)
        {
            var ticket = db.Tickets.Find(id);
            var origin = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
            //get list of all devs
            var devList = roleHelper.UsersInRole("Developer").ToList();
            //if a dev is not on this project, they are removed from the list
            foreach(var dev in devList)
            {
                if(projectHelper.IsUserOnProject(dev.Id, ticket.ProjectId) == false)
                {
                    devList.Remove(dev);
                }
            }


            ViewBag.AssignedToUserId = new SelectList(devList, "Id", "FullName", ticket.AssignedToUserId);

           

            return View(ticket);
        }

        //
        //POST: Tickets/AssignTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTicket(Ticket model)
        {
            var ticket = db.Tickets.Find(model.Id);
            var origin = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
            ticket.AssignedToUserId = model.AssignedToUserId;
            

            db.SaveChanges();

            //calls notificationhelper to see which notification needs to be sent for the assignment.
            NotificationHelper.ManageNotifications(origin, ticket);
            HistoryHelper.CreateHistoryEntries(origin, ticket);

            var callbackUrl = Url.Action("Details", "Tickets", new { id = ticket.Id }, protocol: Request.Url.Scheme);

            try
            {
                EmailService serve = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                ApplicationUser user = db.Users.Find(model.AssignedToUserId);

                msg.Body = "You have a new ticket <br /> <hr /> Please click the <a href=\"" + callbackUrl + "\">this link</a> to view the details.";
                msg.Destination = user.Email;
                msg.Subject = "New Ticket Assignment";

                await serve.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }
            
            return RedirectToAction("Index", "Tickets");
        }



        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
