using Microsoft.AspNetCore.Mvc;

public class TopPicksViewComponent : ViewComponent
{

    private static List<FoodItem> _foodItems = new List<FoodItem>
{
    new FoodItem { Name = "Grilled Chicken", Image = "/image/Chicken.jpg", Price = 1150 },
    new FoodItem { Name = "Pizza", Image = "/image/pizza.jpg", Price = 1000 },
    new FoodItem { Name = "Burger", Image = "/image/burger.jpg", Price = 550 }
};


    public IViewComponentResult Invoke()
    {
        return View(_foodItems);  //Passing static list to the view
    }
}
