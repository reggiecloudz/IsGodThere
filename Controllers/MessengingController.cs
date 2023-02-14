using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IsGodThere.Data;
using IsGodThere.Domain;
using IsGodThere.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IsGodThere.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class MessengingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessengingController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessengingController(ILogger<MessengingController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [Route("/[controller]")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            var chats = await _context.Chats
                .Include(x => x.Users)
                // .Where(u => !u.Users.Any(x => x.UserId == currentUser.Id))
                .ToListAsync();

            return View(chats);
        }

        public async Task<IActionResult> Find()
        {
            var currentUser = await GetCurrentUserAsync();
            
            var users = _context.Users
                .Where(x => x.Id != currentUser.Id)
                .ToList();

            return View(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Chat(long id)
        {
            var chat = await _context.Chats
                .Include(m => m.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);
           
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(long chatId, string content)
        {
            var message = new ChatMessage
            {
                ChatId = chatId,
                Content = content,
                Nick = User.FindFirst("ShortName")!.Value
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            var currentUser = await GetCurrentUserAsync();

            chat.Users.Add(new ChatUser
            {
                UserId = currentUser.Id,
                Role = ChatRole.Admin
            });

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Private()
        {
            var currentUser = await GetCurrentUserAsync();

            var chats = _context.Chats
                .Include(u => u.Users)
                    .ThenInclude(x => x.User)
                .Where(t => t.Type == ChatType.Private && t.Users
                    .Any(y => y.UserId == currentUser.Id))
                .ToList();

            var rooms = ChatMapper(chats);

            return View(rooms);
        }

        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            if (userId == null || _context.Users.Any(u => u.Id != userId))
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUserAsync();

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            
            var chat = new Chat
            {
                Name = $"{currentUser.FullName}|{user!.FullName}",
                Type = ChatType.Private
            };

            chat.Users.Add(new ChatUser {
                UserId = currentUser.Id
            });

            chat.Users.Add(new ChatUser
            {
                UserId = user.Id
            });

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Private));
        }

        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        private string PrivateRoomDisplay(string roomName)
        {
            string[] names = roomName.Split("|");
            var currentUser = User.FindFirst("FullName")!.Value;
            var result = Array.Find(names, element => element != currentUser);
            return result!;
        }

        private List<PrivateChatViewModel> ChatMapper(List<Chat> chats)
        {
            var rooms = new List<PrivateChatViewModel>();

            foreach (var item in chats)
            {
                rooms.Add(new PrivateChatViewModel
                {
                    Id = item.Id,
                    Name = PrivateRoomDisplay(item.Name)
                });
            }

            return rooms;
        }
    }
}