using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        public bool EditMode { get; set; }
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> CuisineSelectItems { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? id)
        {
            EditMode = id.HasValue;
            Restaurant = EditMode ? _restaurantData.GetById(id) : new Restaurant();

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            CuisineSelectItems = _htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();

        }

        public IActionResult OnPost(int? id)
        {
            EditMode = id.HasValue;

            if (!ModelState.IsValid)
            {
                CuisineSelectItems = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (EditMode)
            {
                _restaurantData.Update(Restaurant);
            }
            else
            {
                _restaurantData.Add(Restaurant);
            }
            _restaurantData.Commit();

            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Details", new { id = Restaurant.Id });
        }
    }
}