
using System.Net;
using System.Web.Mvc;
using FirstMVCFiveProject.Models;
using FirstMVCFiveProject.UnitOfWork;

namespace FirstMVCFiveProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public StudentsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Students
        public ActionResult Index()
        {
            var students = _uow.Students.GetAll();
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = _uow.Students.GetById(id.Value);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);

            _uow.Students.Add(student);
            _uow.Complete();
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = _uow.Students.GetById(id.Value);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (!ModelState.IsValid) return View(student);

            _uow.Students.Update(student);
            _uow.Complete();
            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var student = _uow.Students.GetById(id.Value);
            if (student == null) return HttpNotFound();
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = _uow.Students.GetById(id);
            if (student == null) return HttpNotFound();

            _uow.Students.Remove(student);
            _uow.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}