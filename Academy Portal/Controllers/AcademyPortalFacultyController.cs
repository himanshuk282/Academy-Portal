using Academy_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Academy_Portal.Controllers
{
    [Authorize(Roles ="Faculty")]
    public class AcademyPortalFacultyController : Controller
    {
        //<------------------Database Context Section -------------------->
        private ApplicationDbContext _context;
        //<------------------ Current User Declaration -------------------->
        private string currentUser;
        private int facultyId;
        public AcademyPortalFacultyController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //<------------------Dashboard -------------------->
        public ActionResult Index()
        {
            currentUser = User.Identity.GetUserId();
            facultyId = _context.ApplicationUsers.Find(currentUser).UserId;
            ViewBag.FacultyId=facultyId;
            return View();
        }
        //Design a func to accept or reject batches assigned by admin
        //id will have the custom userId of faculty
        //<------------------Batch Approval Section -------------------->
        public ActionResult BatchApproval(int id)
        {
            var batches = _context.Batches.Where(b=>b.FacultyID == id && b.BatchApproval == 0).ToList();
            return View(batches);
        }
        public ActionResult AcceptBatch(int id)
        {
            var particularBatch = _context.Batches.Where(b => b.BatchID == id).SingleOrDefault();
            particularBatch.BatchApproval = 1;//1 means approved
            _context.SaveChanges();
            return RedirectToAction("BatchApproval", new { id=particularBatch.FacultyID});
        }
        public ActionResult DeleteBatch(int id)
        {
            var particularBatch = _context.Batches.Where(b => b.BatchID == id).SingleOrDefault();
            particularBatch.BatchApproval = -1;//-1 means rejected
            _context.SaveChanges();
            return RedirectToAction("BatchApproval", new { id = particularBatch.FacultyID });
        }
        //<------------------Help Section -------------------->
        public ActionResult HelpSection()
        {
            var helpIssues = _context.Help.Where(h => h.UserId == facultyId).ToList();
            return View(helpIssues);
        }
        public ActionResult GetHelp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetHelp(Help helpModel)
        {
            if (!ModelState.IsValid)
                return View(helpModel);
            else
            {
                helpModel.ResolutionStatus = 0;//0 is for pending state
                helpModel.UserId = facultyId;//Map the ticket to the currently logged in faculty
                _context.Help.Add(helpModel);
                _context.SaveChanges();
                return RedirectToAction("HelpSection");
            }
        }
    }
}