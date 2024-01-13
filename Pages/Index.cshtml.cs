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
    public List<Item> ItemList { get; set; }

    public void OnGet()
    {
        // Add Items property
        ItemList = _db.Items.ToList();
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
