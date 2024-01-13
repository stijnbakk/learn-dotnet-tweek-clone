using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using tweekClone.Data;
using tweekClone.Models;

namespace tweekClone.Pages
{
    [BindProperties]
    public class ItemDetails : PageModel
    {
        private readonly ILogger<ItemDetails> _logger;
        private readonly ApplicationDbContext _db;


        public ItemDetails(ILogger<ItemDetails> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        public Item Item { get; set; }
        public void OnGet(int id)
        {
            Item = _db.Items.Find(id);
        }

        // Delete item
        public IActionResult OnPostDelete(int id)
        {
            var item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            return RedirectToPage("/Index");
        }

        // Update title
        public IActionResult OnPostUpdateTitle(int id)
        {
            var item = _db.Items.Find(id);
            item.Title = Item.Title;
            _db.SaveChanges();
            return Page();
        }

    }
}