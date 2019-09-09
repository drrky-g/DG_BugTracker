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
using System.Threading.Tasks;

namespace DG_BugTracker.Controllers
{
    [Authorize(Roles = "Submitter, Developer, Admin, Project Manager")]
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

        [Authorize (Roles = "Project Manager, Developer, Submitter")]
        public ActionResult MyIndex()
        {
            var myTickets = accessHelper.GetMyTickets();

            return View("Index", myTickets);
        }

        public ActionResult ArchiveIndex()
        {
            ViewBag.Header = "Ticket Archive";
            ViewBag.Subheader = "These tickets are no longer active.";
            var archivedTickets = db.Tickets.Where(tkt => tkt.TicketStatus.Name == "Archived").ToList();

            return View("Index", archivedTickets);
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize (Roles = "Submitter")]
        public ActionResult Create()
        {
            var myProjects = projectHelper.ListUserProjects();

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
        [Authorize (Roles = "Submitter")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Updated,TicketTypeId,ProjectId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses.Where(status => status.Name != "Archived"), "Id", "Name", ticket.TicketStatusId);


            //automatic new status
            ticket.TicketStatusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "New").Id;
            //automate created field
            ticket.Created = DateTime.Now;
            //automate submitter field
            ticket.OwnerUserId = User.Identity.GetUserId();
            db.Tickets.Add(ticket);
            db.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);

            if (accessHelper.CanSeeDetails(ticket))
                RedirectToAction("NotAllowedTicket", "Home");

            if (ticket == null)
            {
                return HttpNotFound();
            }
            var projectDevs = new List<ApplicationUser>();
            //get list of all devs
            var devList = roleHelper.UsersInRole("Developer").ToList();
            foreach (var dev in devList)
            {
                if (projectHelper.IsUserOnProject(dev.Id, ticket.ProjectId) == true)
                {
                    projectDevs.Add(dev);
                }
            }

            ViewBag.AssignedToUserId = new SelectList(projectDevs, "Id", "FirstName", ticket.AssignedToUserId);
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
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,TicketTypeId,ProjectId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (!accessHelper.CanSeeDetails(ticket))
                RedirectToAction("NotAllowedTicket", "Home");
            var projectDevs = new List<ApplicationUser>();
            //get list of all devs
            var devList = roleHelper.UsersInRole("Developer").ToList();
            foreach (var dev in devList)
            {
                if (projectHelper.IsUserOnProject(dev.Id, ticket.ProjectId) == true)
                {
                    projectDevs.Add(dev);
                }
            }
            ViewBag.AssignedToUserId = new SelectList(projectDevs, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);

            if (ModelState.IsValid)
            {
                // store old tickets value for comparison with logic of helper methods
                var foreignTicketContext = db.Tickets.Include(tkt => tkt.TicketPriority).Include(tkt => tkt.TicketStatus).Include(tkt => tkt.TicketType).Include(tkt => tkt.AssignedToUser).Include(tkt => tkt.TicketStatus).AsQueryable();
                var origin = foreignTicketContext.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                ticket.Updated = DateTime.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                var edit = foreignTicketContext.FirstOrDefault(tkt => tkt.Id == ticket.Id);
                //calls notificationhelper to see which notification needs to be sent for the assignment.
                NotificationHelper.ManageNotifications(origin, edit);

                return RedirectToAction("Dashboard", "Home");
            }
            


            return View(ticket);
        }

        //
        // GET: Tickets/AssignTicket

        public ActionResult AssignTicket(int? id)
        {
            var ticket = db.Tickets.Find(id);
            var origin = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
            var projectDevs = new List<ApplicationUser>();
            //get list of all devs
            var devList = roleHelper.UsersInRole("Developer").ToList();
            foreach(var dev in devList)
            {
                if(projectHelper.IsUserOnProject(dev.Id, ticket.ProjectId) == true)
                {
                    projectDevs.Add(dev);
                }
            }
            ViewBag.AssignedToUserId = new SelectList(projectDevs, "Id", "FullName", ticket.AssignedToUserId);
            return View(ticket);
        }

        //
        //POST: Tickets/AssignTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTicket(Ticket model)
        {
            if (!accessHelper.CanSeeDetails(model))
                RedirectToAction("NotAllowedTicket", "Home");
            var ticket = db.Tickets.Find(model.Id);
            var origin = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
            ticket.AssignedToUserId = model.AssignedToUserId;
            db.SaveChanges();
            //calls notificationhelper to see which notification needs to be sent for the assignment
            NotificationHelper.ManageNotifications(origin, ticket);

            var callbackUrl = Url.Action("Dashboard", "Home", null, protocol: Request.Url.Scheme);

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
            
            return RedirectToAction("Dashboard", "Home");
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
        //
        //POST: Ticket/InProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetTicketInProgress(int id)
        {
            var me = User.Identity.GetUserId();
            var ticket = db.Tickets.Find(id);

            if (accessHelper.CanSeeDetails(ticket))
                RedirectToAction("NotAllowedTicket", "Home");

            var origin = db.Tickets.AsNoTracking().FirstOrDefault(tkt => tkt.Id == ticket.Id);

            if(ticket.AssignedToUserId == me)
            {
                ticket.TicketStatusId = db.TicketStatuses.Where(status => status.Name == "In Progress").FirstOrDefault().Id;
                ticket.Updated = DateTime.Now;
                db.SaveChanges();
                NotificationHelper.CreateEditNotification(origin, ticket);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                return RedirectToAction("DeveloperOnly", "Home");
            }
            
        }

        //POST: Ticket/OnHold
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetTicketOnHold(int id)
        {
            var me = User.Identity.GetUserId();
            var ticket = db.Tickets.Find(id);
            if (accessHelper.CanSeeDetails(ticket))
                RedirectToAction("NotAllowedTicket", "Home");
            var origin = db.Tickets.AsNoTracking().FirstOrDefault(tkt => tkt.Id == ticket.Id);
            if(ticket.AssignedToUserId == me)
            {
                ticket.TicketStatusId = db.TicketStatuses.Where(status => status.Name == "On Hold").FirstOrDefault().Id;
                ticket.Updated = DateTime.Now;
                db.SaveChanges();
                NotificationHelper.CreateEditNotification(origin, ticket);
                return RedirectToAction("Dashboard", "Home");
            }
            else
                return RedirectToAction("DeveloperOnly", "Home");
        }
    }
}
