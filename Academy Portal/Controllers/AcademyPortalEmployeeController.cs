using Academy_Portal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Academy_Portal.Controllers
{
    [Authorize(Roles ="Employee")]
    public class AcademyPortalEmployeeController : Controller
    {
        //<------------ CONTEXT --------------->
        private ApplicationDbContext _context;
        public AcademyPortalEmployeeController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //<------------- DASHBOARD ---------------->
        public ActionResult Index()
        {
            return View();
        }
        //<------------------------ BASIC SEARCHING SECTION ------------------------>
        //sorting - to be done after displaying the particular result
        public ActionResult BasicSearch(string searchBy, int? search, string sortBy)
        {
            ViewBag.SortSkillParam = sortBy == "SkillID" ? "SkillID_Desc" : "SkillID";
            ViewBag.SortModuleParam = sortBy== "ModuleID" ? "ModuleID_Desc" : "ModuleID";
            ViewBag.SortFacultyParam = sortBy == "FacultyID" ? "FacultyID_Desc" : "FacultyID";
            ViewBag.SortBatchParam = sortBy == "BatchID" ? "BatchID_Desc" : "BatchID";
            ViewBag.SortBatchStartParam = sortBy == "BatchStartDate" ? "BatchStartDate_Desc" : "BatchStartDate";
            ViewBag.SortBatchEndParam = sortBy == "BatchEndDate" ? "BatchEndDate_Desc" : "BatchEndDate";

            var batches = _context.Batches.Include(b=>b.Module).AsQueryable();
            if (searchBy == "SkillID")
            {
                batches = batches.Where(b => b.SkillID == search && b.BatchApproval == 1);
            }
            else if (searchBy == "ModuleID")
            {
                batches = batches.Where(b => b.ModuleID == search && b.BatchApproval == 1);
            }
            else if (searchBy == "BatchID")
            {
                batches = batches.Where(b => b.BatchID == search && b.BatchApproval == 1);
            }
            else if (searchBy == "FacultyID")
            {
                batches = batches.Where(b => b.FacultyID == search && b.BatchApproval == 1);
            }
            else
            {
                batches = batches.Where(b=>b.BatchApproval == 1).OrderBy(b=>b.BatchCapacity);
            }
            switch (sortBy)
            {
                case "SkillID":
                    batches = batches.OrderBy(b => b.SkillID);
                    break;
                case "SkillID_Desc":
                    batches= batches.OrderByDescending(b => b.SkillID);
                    break;
                case "ModuleID":
                    batches=batches.OrderBy(b => b.ModuleID);
                    break;
                case "ModuleID_Desc":
                    batches =batches.OrderByDescending(b => b.ModuleID);
                    break;
                case "FacultyID":
                    batches = batches.OrderBy(b => b.FacultyID);
                    break;
                case "FacultID_Desc":
                    batches=batches.OrderByDescending(b => b.FacultyID);
                    break;
                case "BatchID":
                    batches=batches.OrderBy(b => b.BatchID);
                    break;
                case "BatchID_Desc":
                    batches=batches.OrderByDescending(b => b.BatchID);
                    break;
                case "BatchStartDate":
                    batches=batches.OrderBy(b => b.BatchStartDate);
                    break;
                case "BatchStartDate_Desc":
                    batches = batches.OrderByDescending(b => b.BatchStartDate);
                    break;
                case "BatchEndDate":
                    batches = batches.OrderBy(b => b.BatchEndDate);
                    break;
                case "BatchEndDate_Desc":
                    batches = batches.OrderByDescending(b => b.BatchEndDate);
                    break;
                default:
                    batches = batches.OrderBy(b => b.BatchCapacity);
                    break;
            }

            return View(batches);
        }
        //<------------- ADVANCE SEARCHING ---------------------------------->
        //Sorting is to be done later on
        public ActionResult AdvanceSearch(string searchBy, string search,string sortBy)
        {
            ViewBag.SortSkillParam = sortBy == "SkillID" ? "SkillID_Desc" : "SkillID";
            ViewBag.SortModuleParam = sortBy == "ModuleID" ? "ModuleID_Desc" : "ModuleID";
            ViewBag.SortFacultyParam = sortBy == "FacultyID" ? "FacultyID_Desc" : "FacultyID";
            ViewBag.SortBatchParam = sortBy == "BatchID" ? "BatchID_Desc" : "BatchID";
            ViewBag.SortBatchStartParam = sortBy == "BatchStartDate" ? "BatchStartDate_Desc" : "BatchStartDate";
            ViewBag.SortBatchEndParam = sortBy == "BatchEndDate" ? "BatchEndDate_Desc" : "BatchEndDate";

            var batches = _context.Batches.Include(b => b.Module).AsQueryable();
            //var dataReturned = new List<Batch>();
            if (searchBy == "BatchStartDate")
            {
                var convertedSearchDate = Convert.ToDateTime(search);
                batches = batches.Where(b => b.BatchStartDate == convertedSearchDate && b.BatchApproval == 1);
            }
            else if (searchBy == "BatchEndDate")
            {
                var convertedSearchDate = Convert.ToDateTime(search);
                batches = batches.Where(b => b.BatchEndDate == convertedSearchDate && b.BatchApproval == 1);
            }
            //Works fine
            else if (searchBy == "ProficiencyLevel")
            {
                var dataReturned = new List<Batch>();
                var facultyWithProf = new List<FacultyUser>();
                facultyWithProf = _context.ApplicationUsers.Where(u => u.Proficiencylevel == search).ToList();

                foreach (var faculty in facultyWithProf)
                {
                    //Batch for that faculty - Can also be list but with some other modifications
                    var batchForFaculty = _context.Batches.Include(b=>b.Module).Where(b => b.FacultyID == faculty.UserId && faculty.LoginStatus == 1 && b.BatchApproval == 1).ToList();
                    if (batchForFaculty != null)
                        dataReturned.AddRange(batchForFaculty);
                    else
                        continue;
                }
                return View(dataReturned);
            }
            else
            {
                batches = batches.Where(b => b.BatchApproval == 1).OrderBy(b=>b.BatchCapacity);
            }
            switch (sortBy)
            {
                case "SkillID":
                    batches = batches.OrderBy(b => b.SkillID);
                    break;
                case "SkillID_Desc":
                    batches = batches.OrderByDescending(b => b.SkillID);
                    break;
                case "ModuleID":
                    batches = batches.OrderBy(b => b.ModuleID);
                    break;
                case "ModuleID_Desc":
                    batches = batches.OrderByDescending(b => b.ModuleID);
                    break;
                case "FacultyID":
                    batches = batches.OrderBy(b => b.FacultyID);
                    break;
                case "FacultID_Desc":
                    batches = batches.OrderByDescending(b => b.FacultyID);
                    break;
                case "BatchID":
                    batches = batches.OrderBy(b => b.BatchID);
                    break;
                case "BatchID_Desc":
                    batches = batches.OrderByDescending(b => b.BatchID);
                    break;
                case "BatchStartDate":
                    batches = batches.OrderBy(b => b.BatchStartDate);
                    break;
                case "BatchStartDate_Desc":
                    batches = batches.OrderByDescending(b => b.BatchStartDate);
                    break;
                case "BatchEndDate":
                    batches = batches.OrderBy(b => b.BatchEndDate);
                    break;
                case "BatchEndDate_Desc":
                    batches = batches.OrderByDescending(b => b.BatchEndDate);
                    break;
                default:
                    batches = batches.OrderBy(b => b.BatchCapacity);
                    break;
            }
            return View(batches);
        }
        //<------------ BatchNomination ------------------------------------->
        public ActionResult BatchNominationRegistration(int id)
        {
            var particularBatch= _context.Batches.Where(b => b.BatchID == id).SingleOrDefault();
            return View(particularBatch);
        }
        public ActionResult BatchNominations(int id)
        {
            //getting the currently logged in user
            var currentUser = User.Identity.GetUserId();
            var employeeId = _context.EmployeeUsers.Find(currentUser).UserId;
            var particularUser = _context.EmployeeUsers.Where(e => e.UserId == employeeId).SingleOrDefault();
            var particularBatch = _context.Batches.Where(b => b.BatchID == id).SingleOrDefault();
            if(particularBatch.RemainingCapacity > 0)
            {
                particularUser.BatchNominationStatus = 0;// 0 - Pending Nomination
                particularUser.BatchID = particularBatch.BatchID;//Initializing the Batch Id so that it is not null
                ViewBag.Message = "Nomination sent successfully";
            }
            else
            {
                ViewBag.Message = "Batch Full, please try next time";
            }
            _context.SaveChanges();
            return View();
        }
        //<------------- E-Nomination --------------------------------------->
        public ActionResult ModuleNominationRegistration(int id)
        {
            //We want to display e-learning batches for the employee to register a nomination
            var particularBatch = _context.Batches.Include(b=>b.Module).Where(b => b.ModuleID == id && b.Module.ExecutionType=="E-Learning" && b.BatchApproval == 1).SingleOrDefault();
            if (particularBatch != null)
                return View(particularBatch);
            else
                return View("Error");
        }
        //<------------- E-Nomination Execution ------------------------------>
        //In my learnings those batches which are E-Learning should have a new Method for execution
        public ActionResult ELearningExecution(int id)
        {
            var module = _context.Batches.Include(b => b.Module).Where(b => b.BatchID == id).SingleOrDefault();
            ViewBag.LearningContentId = id;
            return View(module);
        }

        //<------------- My Learning Section -------------------------------->
        public ActionResult MyLearnings()
        {
            //display only what is registed for the current user
            var currentUser = User.Identity.GetUserId();
            var employeeId = _context.EmployeeUsers.Find(currentUser).UserId;
            var approvedListForUser = _context.EmployeeUsers.Include(e => e.Batches).Where(e => e.UserId == employeeId).Select(e => e.Batches).SingleOrDefault();
            var ListToDisplay = new List<Batch>();
            foreach (var batch in approvedListForUser)
            {
                var particularModule = _context.Modules.Where(m => m.ModuleID == batch.ModuleID).SingleOrDefault();
                batch.Module = particularModule;
                ListToDisplay.Add(batch);
            }
            return View(ListToDisplay);
        }
        //<------------- Help Section -------------------------------->
        public ActionResult HelpSection()
        {
            var currentUser = User.Identity.GetUserId();
            var employeeId = _context.EmployeeUsers.Find(currentUser).UserId;
            //One-Many RelationShip btw EmployeeUser and Help Table
            var helpIssues = _context.Help.Where(h => h.UserId == employeeId).ToList();
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
            var currentUser = User.Identity.GetUserId();
            var employeeId = _context.EmployeeUsers.Find(currentUser).UserId;
            if (!ModelState.IsValid)
                return View(helpModel);
            else
            {
                helpModel.ResolutionStatus = 0;//0 is for pending state
                helpModel.DateOfTicket = DateTime.Today.Date;
                helpModel.UserId = employeeId;//Map the ticket to the currently logged in employee
                _context.Help.Add(helpModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}