using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                WeekdayName = WeekOverview.StartDate.AddDays(i).DayOfWeek.ToString(),
                DayItems = _db.Items.Where(item => item.LinkedDate == WeekOverview.StartDate.AddDays(i)).ToList()
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
        // Add Items property
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
        return RedirectToPage();
    }
}
