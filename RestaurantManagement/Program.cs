var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        // Register the custom view location expander to look in the BFC folder
        options.ViewLocationExpanders.Add(new CustomViewLocationExpander());
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
app.MapControllerRoute(
    name: "bfc",
    pattern: "BFC",// URL for the BFC page
    defaults: new { controller = "Home", action = "BFC" });

app.MapControllerRoute(
    name: "kfc",
    pattern: "KFC", // URL for the KFC page
    defaults: new { controller = "Home", action = "KFC" });