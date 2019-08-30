using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DG_BugTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Dashboard",
                url: "home",
                defaults: new
                {
                    controller = "Home",
                    action = "Dashboard"
                });

            routes.MapRoute(
                name: "Create Ticket",
                url: "ticket/new",
                defaults: new
                {
                    controller = "Tickets",
                    action = "Create"
                });

            routes.MapRoute(
                name: "Create Project",
                url: "project/new",
                defaults: new
                {
                    controller = "Projects",
                    action = "Create"
                });

            routes.MapRoute(
                name: "Manage Project Team",
                url: "project/team/{id}",
                defaults: new
                {
                    controller = "Projects",
                    action = "Details",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "User Index",
                url: "admin/allusers",
                defaults: new
                {
                    controller = "RoleManagement",
                    action = "UserIndex"
                });

            routes.MapRoute(
                name: "Manage User Projects",
                url: "admin/projects/{id}",
                defaults: new
                {
                    controller = "RoleManagement",
                    action = "ManageUsersMultipleProjects",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Manage Multiple User Roles",
                url: "admin/allroles",
                defaults: new
                {
                    controller = "RoleManagement",
                    action = "ManageMultipleRoles"
                });

            routes.MapRoute(
                name: "Archive",
                url: "archive",
                defaults: new
                {
                    controller = "Tickets",
                    action = "ArchiveIndex"
                });

            routes.MapRoute(
                name: "Edit Ticket",
                url: "ticket/modify/{id}",
                defaults: new
                {
                    controller = "Tickets",
                    action = "Edit",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Edit Project",
                url: "project/modify/{id}",
                defaults: new
                {
                    controller = "Projects",
                    action = "Edit",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Manage Single Role",
                url: "admin/role/{id}",
                defaults: new
                {
                    controller = "RoleManagement",
                    action = "ManageSingleRole",
                    id = UrlParameter.Optional
                });
            routes.MapRoute(
                name: "Assign Ticket",
                url: "ticket/assign/{id}",
                defaults: new
                {
                    controller = "Tickets",
                    action = "AssignTicket",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
