using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DG_BugTracker.Models;
using DG_BugTracker.Helpers;
using Microsoft.AspNet.Identity;

namespace DG_BugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private ProjectHelper projectHelper = new ProjectHelper();
        
        



        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.ToList();
            return View(tickets.ToList());
        }

        // [Authorize (Roles = "Project Manager, Developer, Submitter")]
        public ActionResult MyIndex()
        {
            //View where the authenticated user only sees tickets theyre associated with
            //Submitter - any ticket theyve created will show here
            //Dev - any ticket theyve been assigned will show here

            //store userId
            var userId = User.Identity.GetUserId();

            //store user role
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            //ticket sets
            var myTickets = new List<Ticket>();

            switch (myRole)
            {
                case "Developer":
                    //Dev index
                    //shows tickets you have be assigned to by your PM
                    myTickets = db.Tickets.Where(ticket => ticket.AssignedToUserId == userId).ToList();
                    break;

                case "Submitter":
                    //Submitter index
                    //show tickets you have submitted
                    myTickets = db.Tickets.Where(ticket => ticket.OwnerUserId == userId).ToList();
                    break;

                case "Project Manager":
                    //PM index
                    //show tickets for any project you are managing
                    myTickets = db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).ToList();
                    break;
            }


            return View("Index", myTickets);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {

            Ticket ticket = db.Tickets.Find(id);
             
            //compare logged in user to users assigned the ticket, if they match, allow access
            var loggedInUser = User.Identity.GetUserId();

            //get project id
            var projectId = ticket.ProjectId;

            //check to see if PM is the PM of the projectId
            var isOnProject = projectHelper.IsUserOnProject(loggedInUser, projectId);

            //store assigned dev
            var assignedDev = ticket.AssignedToUserId;

            //store submitter
            var assignedSubmitter = ticket.OwnerUserId;

            

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
            else
            {
                //get user role
                var myRole = roleHelper.ListUserRoles(loggedInUser).FirstOrDefault();

                switch (myRole)
                {
                    case "Project Manager":
                        //Only allow PM of associated project
                        if (!isOnProject)
                        {
                            return RedirectToAction("NotAllowedTicket", "Home");
                        }
                        break;
                    case "Developer":
                        //Compare AssignedToUser property to logged in user
                        if (loggedInUser != assignedDev)
                        {
                            return RedirectToAction("NotAllowedTicket", "Home");
                        }
                        break;
                    case "Submitter":
                        //Compare OwnerUser property to logged in user
                        if (loggedInUser != assignedSubmitter)
                        {
                            return RedirectToAction("NotAllowedTicket", "Home");
                        }
                        break;
                    case "Admin":
                        //Automatic Access
                        return View(ticket);
                }
            }

            
            return View(ticket);
        }

        // GET: Tickets/Create
        //[Authorize (Roles = "Submitter")]
        public ActionResult Create()
        {
            //variable to only show Devs in assign field
            var allDevs = roleHelper.UsersInRole("Developer");

            ViewBag.AssignedToUserId = new SelectList(allDevs, "Id", "FirstName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            
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

            //variable to only show Devs in assign field
            var allDevs = roleHelper.UsersInRole("Developer");

            ViewBag.AssignedToUserId = new SelectList(allDevs, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);

            if (ModelState.IsValid)
            {
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
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
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
