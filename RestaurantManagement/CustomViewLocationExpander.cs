using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

public class CustomViewLocationExpander : IViewLocationExpander
{
    // This method can be used to add custom values, if needed.
    public void PopulateValues(ViewLocationExpanderContext context)
    {
        // We don't need to add values in this case, so it's left empty.
    }

    // This method defines additional view locations.
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        // Add the custom folder path for views (e.g., BFC/Index.cshtml)
        var newLocations = new List<string>
        {
            "/Views/BFC/{0}.cshtml" // Add custom path for views inside the BFC folder
        };

        // Yield custom locations first
        foreach (var location in newLocations)
        {
            yield return location;
        }

        // Yield the default locations as well
        foreach (var location in viewLocations)
        {
            yield return location;
        }
    }
}
