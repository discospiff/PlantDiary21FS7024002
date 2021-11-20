using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
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
            ViewData["Result"] = "Not processed";
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

            ValidateXml(file);

        }

        private void ValidateXml(string file)
        {
            // declare our validation preference
            XmlReaderSettings settings = new XmlReaderSettings();
            string xsdPath = Path.Combine(Environment.ContentRootPath, "uploads", "plants.xsd");
            settings.Schemas.Add(null, xsdPath);

            // we want to validate with XSD
            settings.ValidationType = ValidationType.Schema;

            // a couple more flags
            settings.ValidationFlags |= System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings;

            settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(this.ValidationEventHandler);

            XmlReader xmlReader = XmlReader.Create(file, settings);
            try
            {
                while (xmlReader.Read())
                {

                }
                ViewData["Result"] = "Validation Passed.";
            } 
            catch (Exception e)
            {
                ViewData["Result"] = "Validation Failed.  " + e.Message;
            }
        }

        /// <summary>
        /// Only if a validation occurs, will this method be invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            throw new Exception("validation failed.  Message: " + args.Message);
        }
    }
}
