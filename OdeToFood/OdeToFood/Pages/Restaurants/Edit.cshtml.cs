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

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> CuisineSelectItems { get; set; } 

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int id)
        {
            Restaurant = _restaurantData.GetById(id);

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            CuisineSelectItems = _htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();

        }

        public IActionResult OnPost()
        {
            Restaurant = _restaurantData.Update(Restaurant);
            _restaurantData.Commit();

            CuisineSelectItems = _htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();
        }
    }
}