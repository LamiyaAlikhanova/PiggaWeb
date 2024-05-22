using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pigga_WebApplication.DAL;
using Pigga_WebApplication.Models;

namespace Pigga_WebApplication.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class ChefController : Controller
    {
       
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        

        public ChefController(AppDbContext context,IWebHostEnvironment environment )
        {
            _context = context;
            _environment = environment;
           
        }
        public IActionResult Index()
        {
           
           
            return View(_context.Chefss.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Chefs chefs) 
        {
            if (!ModelState.IsValid) return View();
            if (!chefs.PhotoFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("PhotoFile" ,"File tipini duzgun daxil edin");
                    return View();
            }

            string path = _environment.WebRootPath + @"\Uploadd\";
            string filname= Guid.NewGuid()+chefs.PhotoFile.FileName;

            using (FileStream stream = new FileStream(path + filname, FileMode.Create))
            {
                chefs.PhotoFile.CopyTo(stream);
            }
            chefs.ImgUrl=filname;
            _context.Chefss.Add(chefs);
            _context.SaveChanges();
           

               return RedirectToAction("Index");
        }

        public IActionResult Update (int id)
        {
            var chef= _context.Chefss.FirstOrDefault(x => x.Id == id);
            if (chef == null)
            {
                return NotFound();
            }
            return View(chef);
        }
        [HttpPost]
        public IActionResult Update (Chefs chefs)
        {
            if(!ModelState.IsValid && chefs.PhotoFile!=null) 
            {
                return View();
            }

            var oldChef=_context.Chefss.FirstOrDefault(x=>x.Id == chefs.Id);    
            if(chefs.PhotoFile!=null)
            {
                string path = _environment.WebRootPath + @"\Uploadd\";
                FileInfo fileInfo = new FileInfo(path+oldChef.ImgUrl);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                string filname = Guid.NewGuid() + chefs.PhotoFile.FileName;

                using (FileStream stream = new FileStream(path + filname, FileMode.Create))
                {
                    chefs.PhotoFile.CopyTo(stream);
                }
                oldChef.ImgUrl = filname;
            }
            oldChef.FullName= chefs.FullName;
            oldChef.Description= chefs.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete (int id)
        {
            var Chef = _context.Chefss.FirstOrDefault(x => x.Id == id);
            _context.Chefss.Remove(Chef);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
