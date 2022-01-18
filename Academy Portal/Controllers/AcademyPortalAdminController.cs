using Academy_Portal.Models;
using ClosedXML.Excel;
using Academy_Portal.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rotativa;

namespace Academy_Portal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AcademyPortalAdminController : Controller
    {
        //<-------------- Database Context Section ------------------->
        private ApplicationDbContext _context;
        public AcademyPortalAdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //<-------------- Admin Dashboard Section ------------------->
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string fromAction)
        {
            if (fromAction == "SkillRegistration")
                ViewBag.Message = "Skill Registered Successfully";
            else if (fromAction == "ModuleRegistration")
                ViewBag.Message = "Module Registered Successfully";
            else if (fromAction == "Map")
                ViewBag.Message = "Skill/Module Mapped Successfully";
            else if (fromAction == "BatchRegistration")
                ViewBag.Message = "Batch registered successfully";
            else
                ViewBag.Message = "Good job";
            return View();
        }
        //<-------------- New Registration Section ------------------->
        public ActionResult NewRegistrations()
        {
            //Will Load Only Pending Faculties
            var newRegistrations = _context.Users.Where(u => u.UserCategory == "Faculty" && u.LoginStatus == 0).ToList();
            var viewModel = new FacultyDetailsViewModel
            {
                Faculty = newRegistrations
            };
            return View(viewModel);
        }
        public ActionResult FacultyDetails(int id)
        {
            var particularFaculty = _context.Users.Where(u => u.UserId == id).SingleOrDefault();
            return View(particularFaculty);
        }
        public ActionResult AcceptFaculty(int id)
        {
            var particularFaculty = _context.Users.Where(u => u.UserId == id).SingleOrDefault();
            particularFaculty.LoginStatus = 1;
            _context.SaveChanges();
            return RedirectToAction("NewRegistrations");
        }
        public ActionResult DeleteFaculty(int id)
        {
            var particularFaculty = _context.Users.Where(u => u.UserId == id).SingleOrDefault();
            if (particularFaculty != null)
            {
                particularFaculty.LoginStatus = -1;
                //_context.Users.Remove(particularFaculty);
                _context.SaveChanges();
                return RedirectToAction("NewRegistrations");
            }
            return View("Error");
        }
        //<-------------- Skill Registration Section ------------------->
        [HttpGet]
        public ActionResult SkillRegistration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SkillRegistration(Skill skillModel)
        {
            if (!ModelState.IsValid)
                return View(skillModel);
            else
            {
                _context.Skills.Add(skillModel);
                _context.SaveChanges();
                return RedirectToAction("Details", new { fromAction="SkillRegistration"});
            }
        }
        //<-------------- Module Registration Section ------------------->

        [HttpGet]
        public ActionResult ModuleRegistration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModuleRegistration(Module moduleModel)
        {
            if (!ModelState.IsValid)
                return View(moduleModel);
            else
            {
                _context.Modules.Add(moduleModel);
                _context.SaveChanges();
                return RedirectToAction("Details", new { fromAction = "ModuleRegistration" });
            }
        }
        //<-------------- Skill-Module Mapping Section------------------->
        [HttpGet]
        public ActionResult Map()
        {
            var skills = _context.Skills.ToList();
            var modules = _context.Modules.ToList();
            var viewModel = new SkillModuleViewModel
            {
                SkillModule = new SkillModule(),
                Skills = skills,
                Modules = modules
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Map(SkillModuleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                _context.SkillModules.Add(model.SkillModule);
                _context.SaveChanges();
                return RedirectToAction("Details", new { fromAction = "Map" });
            }

        }
        //<--------- Batch Registration Section ----------->

        [HttpGet]
        public ActionResult BatchRegistration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BatchRegistration(Batch batchModel)
        {
            if (!ModelState.IsValid)
                return View(batchModel);
            else
            {
                batchModel.BatchApproval = 0;//0 is for pending state
                batchModel.RemainingCapacity = batchModel.BatchCapacity;
                batchModel.Module = _context.Modules.Where(m => m.ModuleID == batchModel.ModuleID).SingleOrDefault();//This will fill the batch for that module
                batchModel.Skill = _context.Skills.Where(m => m.SkillID == batchModel.SkillID).SingleOrDefault();//This will populate the Skill in Batch at time of nomination
                _context.Batches.Add(batchModel);
                _context.SaveChanges();
                return RedirectToAction("Details", new { fromAction = "BatchRegistration" });
            }
        }

        //<--------- Batch Nomination Section-------------->
        public ActionResult BatchNominations()
        {
            //Show the new registration and then by clicking show new page
            var employeeWithNomination = _context.EmployeeUsers.Where(e => e.UserCategory == "Employee" && e.BatchNominationStatus == 0).ToList();
            return View(employeeWithNomination);
        }
        public ActionResult BatchDetails(int id)
        {
            var batch = _context.Batches.Where(b => b.BatchID == id).SingleOrDefault();
            var faculty = _context.ApplicationUsers.Where(f => f.UserId == batch.FacultyID).SingleOrDefault();
            var viewModel = new BatchNominationModel
            {
                Faculty = faculty,
                Batch = batch
            };
            return View(viewModel);
        }
        public ActionResult ApproveBatchNomination(int batchId, int employeeId)
        {
            var batch = _context.Batches.Include(b => b.EmployeeUsers).Where(b => b.BatchID == batchId).SingleOrDefault();
            if (batch.RemainingCapacity > 0)
            {
                var particularEmployee = _context.EmployeeUsers.Include(e => e.Batches).Where(e => e.UserId == employeeId).SingleOrDefault();
                particularEmployee.BatchNominationStatus = 1;//Approved
                particularEmployee.Batches.Add(batch);//Added a batch for that particular employee user
                batch.EmployeeUsers.Add(particularEmployee);//Adding that employee for the batch
                batch.RemainingCapacity--;
            }
            _context.SaveChanges();
            return RedirectToAction("BatchNominations");
        }
        public ActionResult RejectBatchNomination(int employeeId)
        {
            var particularEmployee = _context.EmployeeUsers.Where(e => e.UserId == employeeId).SingleOrDefault();
            particularEmployee.BatchNominationStatus = -1;//Rejected
            _context.SaveChanges();
            return RedirectToAction("BatchNominations");
        }
        //<--------- Report Management Section ------------>
        public ActionResult ReportManagement()
        {
            return View();
        }
        public ActionResult ReportSubmission(string searchBy, int? search)
        {
            var batches = _context.Batches.AsQueryable();
            ViewBag.SearchBy = searchBy;
            ViewBag.Search = search;
            if (searchBy == "SkillID")
                batches = batches.Where(b => b.SkillID == search);//Include(b=>b.Skill)
            else if (searchBy == "ModuleID")
                batches = batches.Where(b => b.ModuleID == search);//Include(b=>b.Module)
            else
                batches = batches.Where(b => b.BatchID == search);

            return View(batches.ToList());
        }
        //<-------- Excel Download Section ----------->
        [HttpPost]
        public FileResult ExportToExcel(string searchBy, int? search)
        {
            DataTable dt = new DataTable("Batches");
            var batches = _context.Batches.AsQueryable();
            if (searchBy == "SkillID")
                batches = batches.Where(b => b.SkillID == search);//Include(b=>b.Skill)
            else if (searchBy == "ModuleID")
                batches = batches.Where(b => b.ModuleID == search);//Include(b=>b.Module)
            else
                batches = batches.Where(b => b.BatchID == search);
            var batchModel = batches.ToList();

            dt.Columns.AddRange(new DataColumn[8]
            {
                new DataColumn("BatchId"),
                new DataColumn("ClassroomName"),
                new DataColumn("Technology"),
                new DataColumn("ModuleId"),
                new DataColumn("SkillId"),
                new DataColumn("FacultyId"),
                new DataColumn("BatchStartDate"),
                new DataColumn("BatchEndDate")
            });
            foreach (var batch in batchModel)
            {
                dt.Rows.Add(batch.BatchID, batch.ClassroomName, batch.Technology, batch.ModuleID,
                    batch.SkillID, batch.FacultyID, batch.BatchStartDate, batch.BatchEndDate);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Batches.xls");
                }
            }
        }
        //<-------- Pdf Download Section----------->
        public ActionResult ExportToPdf(string queryName, int? query)
        {
            var fileName = "Batches.pdf";
            var cookies = Request.Cookies.AllKeys.ToDictionary(k => k, k => Request.Cookies[k]?.Value);
            return new ActionAsPdf("ReportSubmission", new
            {
                searchBy=queryName,
                search=query
            }) 
            { 
                FileName=fileName,
                Cookies = cookies 
            };
        }
        //<--------- Help Section------------------->
        public ActionResult GiveHelp()
        {
            //Add ResolutionProvided here
            var pendingProblems = _context.Help.Where(h => h.ResolutionStatus == 0).ToList();
            return View(pendingProblems);
        }
        public ActionResult ProvideResolution(int id)
        {
            var particularIssue = _context.Help.SingleOrDefault(h => h.RequestId == id);
            if (particularIssue == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("ProvideResolution", particularIssue);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProvideResolution(Help helpModel)
        {
            //Render a form where admin can provide a resolution
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            if (helpModel.RequestId == 0)
            {
                _context.Help.Add(helpModel);
            }
            else
            {
                var issueInDb = _context.Help.Single(h => h.RequestId == helpModel.RequestId);
                issueInDb.Issue = helpModel.Issue;
                issueInDb.Description = helpModel.Description;
                issueInDb.DateOfTicket = helpModel.DateOfTicket;
                issueInDb.ResolutionProvided = helpModel.ResolutionProvided;
                issueInDb.ResolutionStatus = 1;//Indicates that resolution have been provided by admin
            }
            _context.SaveChanges();
            return RedirectToAction("GiveHelp");
        }

    }
}