using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class MenuItemsViewComponent : ViewComponent
{
    private static List<MenuItem> _menuItems = new List<MenuItem>
    {
        new MenuItem { Name = "Pasta", Image = "/image/pasta.jpg", Price = 850 },
        new MenuItem { Name = "Fried Rice", Image = "/image/friedrice.jpg", Price = 900 },
        new MenuItem { Name = "Steak", Image = "/image/steak.jpg", Price = 2500 },
        new MenuItem { Name = "Sushi", Image = "/image/sushi.jpg", Price = 1800 },
      
        new MenuItem { Name = "BBQ Ribs", Image = "/image/bbq.jpg", Price = 1500 }
    };

    public IViewComponentResult Invoke()
    {
        return View(_menuItems);  // Passing static list to the view
    }
}
