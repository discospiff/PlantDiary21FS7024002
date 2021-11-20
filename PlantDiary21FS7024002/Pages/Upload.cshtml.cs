using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlantDiary21FS7024002.Pages
{
    public class UploadModel : PageModel
    {

        public UploadModel(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        public IHostingEnvironment Environment { get; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // find the path where we want to save the uploade file.
            string fileName = Upload.FileName;
            string file = Path.Combine(Environment.ContentRootPath, "uploads", fileName);
            // take the uploaded file, write it to disk.
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                Upload.CopyTo(fileStream);
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode node = doc.SelectSingleNode("/plant/genus");
            int foo = 1 + 1;

        }
    }
}
