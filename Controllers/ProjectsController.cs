﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DG_BugTracker.Helpers;
using DG_BugTracker.Models;

namespace DG_BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Project Manager")]
    public class ProjectsController : Controller
    {
        private UserRoleHelper roleHelper = new UserRoleHelper();
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projectHelper = new ProjectHelper();

        // GET: Projects
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: MyProjects
        public ActionResult MyProjects()
        {

            //if user in project, assign to myProjects
            //return myProjects to view
            var myProjects = projectHelper.ListUserProjects();

            return View("Index", myProjects);
        }

        

        // GET: Projects/Details/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            };


            Project project = db.Projects.Find(id);


            if (project == null)
            {
                return HttpNotFound();
            };

            //give details view a multiselectlist of available people per role
            var allPMs = roleHelper.UsersInRole("Project Manager");
            var allDevs = roleHelper.UsersInRole("Developer");
            var allSubmitters = roleHelper.UsersInRole("Submitter");

            //get all current assigned team members
            var assignedPMs = projectHelper.UsersInRoleOnProject(project.Id, "Project Manager");
            var assignedDevs = projectHelper.UsersInRoleOnProject(project.Id, "Developer");
            var assignedSubmitters = projectHelper.UsersInRoleOnProject(project.Id, "Submitter");



            //setting view bag to contain multiselect lists of all members of each specific role
            //maybe make PM a drop down list since only 1 can be on a project at a time?
            ViewBag.ProjectManagers = new MultiSelectList(allPMs, "Id", "FullNamePlusEmail", assignedPMs);
            ViewBag.Developers = new MultiSelectList(allDevs, "Id", "FullNamePlusEmail", assignedDevs);
            ViewBag.Submitters = new MultiSelectList(allSubmitters, "Id", "FullNamePlusEmail", assignedSubmitters);

            return View(project);
        }



        // GET: Projects/Create
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Project project)
        {
            project.Created = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = project.Id});
            }
            return View(project);
                
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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