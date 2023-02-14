using IsGodThere.Data;
using IsGodThere.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IsGodThere.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RoomViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var user = _context.Users.SingleOrDefault(m => m.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var chats = _context.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == user!.Id && x.Chat!.Type == ChatType.Room)
                .Select(c => c.Chat)
                .ToList();

            return View(chats);
        }
    }
}
