using ClassifiedApp.BusinessServices.Users.Interfaces;
using ClassifiedApp.BusinessServices.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ClassifiedApp.DataAccess.UnitOfWork;

namespace ClassifiedApp.WebApi.Areas.Admin.Controllers
{
    
    public class UserController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private readonly IUserService _userService;
        //private readonly IClassifiedService _classifiedService;
        //private readonly IReportService _reportService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/User
        public ActionResult Index(string sortOrder, string username, int? page,string status)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            ViewBag.UsernameFilter = String.IsNullOrEmpty(username) ? "" : username; 
            ViewBag.StatusFilter = String.IsNullOrEmpty(status) ? "" : status; 
            var users = _userService.All();
            if (!String.IsNullOrEmpty(username))
            {
                users = users.Where(s => s.Username.Contains(username)
                                       );
            }
            if (!String.IsNullOrEmpty(status))
            {
               
                users = users.Where(s => s.IsBlocked == (status=="0")
                                       );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.Username);
                    break;
                case "Date":
                    users = users.OrderBy(s => s.CreationDate);
                    break;
                case "date_desc":
                    users = users.OrderByDescending(s => s.CreationDate);
                    break;
                default:
                    users = users.OrderBy(s => s.Username);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.prevUrl = Request.UrlReferrer.AbsoluteUri;
            var model = unitOfWork.UserRepository.FindSingleBy(u => u.Id == id);
            return View(model);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
