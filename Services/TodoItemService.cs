using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;
        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<TodoItem[]> GetIncompleteItemAsync() => await _context.Items
            .Where(x => x.IsDone == false)
            .ToArrayAsync();

        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            throw new System.NotImplementedException();
        }
        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.Id = Guid.NewGuid ();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays (3);

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}