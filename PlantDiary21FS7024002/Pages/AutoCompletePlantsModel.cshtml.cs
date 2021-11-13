using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlantDiary21FS7024002.Pages
{
    /// <summary>
    /// Create a dynamic list of plants.
    /// </summary>
    public class AutoCompletePlantsModelModel : PageModel
    {

        // declare an empty collection.
        private IList<string> plantNames = new List<string>();

        public JsonResult OnGet(String term)
        {
            // create a list of all plants.  This simulates a database of plants.
            plantNames.Add("Redbud");
            plantNames.Add("Red Maple");
            plantNames.Add("Red Oak");
            plantNames.Add("Red Lily");
            plantNames.Add("Red Rose");

            // make a list of filtered plants.
            IList<string> filteredPlants = new List<string>();

            // filter by the user's term.
            foreach(string plantName in plantNames)
            {
                if (plantName.Contains(term))
                {
                    // add it to our list of filtered plants.
                    filteredPlants.Add(plantName);
                }
            }

            return new JsonResult(filteredPlants);
        }
    }
}
