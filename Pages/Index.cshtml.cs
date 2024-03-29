using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tweekClone.Data;
using tweekClone.Models;

namespace tweekClone.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    // Add ApplicationDBContext
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _db;

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    // Add Items property

    public Item NewItem { get; set; }
    public List<DayOverview> DayOverviews { get; set; }
    public WeekOverview WeekOverview { get; set; }


    public void OnGet(string startDate)
    {
        // Initiate a WeekOverview object
        WeekOverview = new WeekOverview
        {
            StartDate = GetStartDate(startDate),
            WeekDays = new List<DayOverview>()
        };

        // Populate weekOverview with a series of DayOverview objects
        for (int i = 0; i < 7; i++)
        {
            var dayOverview = new DayOverview
            {
                Date = WeekOverview.StartDate.AddDays(i),
                DayItems = _db.Items
                    .Where(item => item.LinkedDate == WeekOverview.StartDate.AddDays(i))
                    .OrderBy(item => item.DaySortOrder) // Sort by DaySortOrder
                    .ToList()
            };
            WeekOverview.WeekDays.Add(dayOverview);
        }
    }

    private DateOnly GetStartDate(string startDate)
    {
        if (!string.IsNullOrEmpty(startDate) && DateOnly.TryParse(startDate, out DateOnly parsedDate))
        {
            return parsedDate;
        }
        else
        {
            return DateOnly.FromDateTime(DateTime.Now).AddDays(-(int)DateTime.Now.DayOfWeek + 1);
        }
    }

    public IActionResult OnPost()
    {
        // TODO: make robust with a check on validity of NewItem
        // TODO: make HTMX compliant by adding new item to end of the list
        NewItem.DaySortOrder = _db.Items.Where(item => item.LinkedDate == NewItem.LinkedDate).Count();
        _db.Items.Add(NewItem);
        _db.SaveChanges();
        return RedirectToPage();
    }

    // Add page handler "UpdateCompleted" to handle completion of items
    public IActionResult OnPostUpdateCompleted(int itemId)
    {
        var item = _db.Items.Find(itemId);
        item.Completed = !item.Completed;
        _db.SaveChanges();
        // return RedirectToPage();

        // return partial _ItemListView with the updated item
        return Partial("/Pages/Components/WeekView/ListItem.cshtml", item);

    }
    public async Task<IActionResult> OnPostSortItems()
    {
        try
        {
            var itemIds = Request.Form["itemId"].ToList();
            for (int i = 0; i < itemIds.Count; i++)
            {
                var item = _db.Items.Find(int.Parse(itemIds[i]));
                item.DaySortOrder = i;
            }
            await _db.SaveChangesAsync();

            // find the date of the first item
            var firstItem = _db.Items.Find(int.Parse(itemIds[0]));
            var date = firstItem.LinkedDate;
            // query all items for that date
            var items = _db.Items.Where(item => item.LinkedDate == date).OrderBy(item => item.DaySortOrder).ToList();
            // return partialview components/weekview/list with the items
            return Partial("/Pages/Components/WeekView/List.cshtml", items);
            // return RedirectToPage();
        }
        catch (System.Exception)
        {
            return Page();
        }
    }
}
