using DG_BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DG_BugTracker.Helpers
{
    public abstract class InstanceHelper
    {

        //store instances of each helper inside this inherited class

        //ApplicationDbContext Instance
        protected static ApplicationDbContext db = new ApplicationDbContext();
        //UserRoleHelper Instance
        protected static UserRoleHelper roleHelper = new UserRoleHelper();
        //UserProjectsHelper Instance
        protected static UserProjectsHelper projectHelper = new UserProjectsHelper();
        //UserManager Instance
        protected static UserManager<ApplicationUser> userManager = new
       UserManager<ApplicationUser>(new UserStore<ApplicationUser>
       (new ApplicationDbContext()));

        protected static HistoryHelper historyHelper = new HistoryHelper();
    }
}