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
    public DayOverview DayOverview { get; set; }


    public void OnGet()
    {
        DayOverviews = new List<DayOverview>();

        // Create a list of DayOverview objects, starting with the monday of this (last) week
        var monday = DateOnly.FromDateTime(DateTime.Now).AddDays(-(int)DateTime.Now.DayOfWeek + 1);
        for (int i = 0; i < 7; i++)
        {
            var dayOverview = new DayOverview
            {
                Date = monday.AddDays(i),
                DayOfWeek = monday.AddDays(i).DayOfWeek.ToString(),
                DayItems = _db.Items.Where(item => item.LinkedDate == monday.AddDays(i)).ToList()
            };
            DayOverviews.Add(dayOverview);
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

    // Add page handler that returns a single item as html when requesting an item by id
    public IActionResult OnGetItem(int itemId)
    {
        var item = _db.Items.Find(itemId);
        return new ContentResult
        {
            ContentType = "text/html",
            Content = $"<h2>{item.Title}</h2>"
        };
    }
}
