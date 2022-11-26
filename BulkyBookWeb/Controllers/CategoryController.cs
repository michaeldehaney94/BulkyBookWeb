using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        //object used to retrieve data from database table
        private readonly ApplicationDbContext _db;

        //constructor used to retrieve the connection string to connect to database to read data from tables
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //var objCategoryList = _db.Categories.ToList();
            //IEnumerable allows 'readonly' access to a collection then a collection that implements IEnumerable can be used with a for-each statement.
            IEnumerable<Category> objCategoryList = _db.Categories;

            return View(objCategoryList);
        }

        //GET
        //Route to view create page
        public IActionResult Create()
        {
            return View();
        }

        //POST to create data
        [HttpPost]
        [ValidateAntiForgeryToken] //prevents CSRF
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index"); //redirect to create page
            }
            return View(obj);
        }

        //GET record to update
		public IActionResult Edit(int? id)
		{
            if (id==null || id==0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
			return View(categoryFromDb);
		}

        //Update record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj) 
        {
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["success"] = "Category updated successfully!";
				return RedirectToAction("Index"); //redirect to create page
			}
			return View(obj);
		}

        //DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //DELETE record
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

         
        }

    }
}
