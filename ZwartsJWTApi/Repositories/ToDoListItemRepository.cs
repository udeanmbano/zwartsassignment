using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwartsJWTApi.Authentication;
using ZwartsJWTApi.Interface;
using ZwartsJWTApi.Models;

namespace ZwartsJWTApi.Repositories
{
    public class ToDoListItemRepository : IToDoListItemRepository
    {
        private ApplicationDbContext _context;
        //initialise database context
        public ToDoListItemRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }

       public List<ToDoListItems> GetToDoItemLists(int toDoListId)
        {
            return _context.toDoListItems.Where(a => a.ToDoListId==toDoListId).ToList();
        }
       public ToDoListItems GetToDoListItemByID(int toDoListId)
        {
            return _context.toDoListItems.Find(toDoListId);
        }
        public async Task InsertToDoListItem(ToDoListItems toDoListItems)
        {
            _context.toDoListItems.Add(toDoListItems);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteToDoListItem(int toDoListId)
        {
           ToDoListItems toDoListItem = await _context.toDoListItems.FindAsync(toDoListId);
            _context.toDoListItems.Remove(toDoListItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateToDoListItem(ToDoListItems toDoListItems)
        {
            _context.Entry(toDoListItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
       public bool ToDoListExists(int toDoListItemId)
        {
            return _context.toDoLists.Count(e => e.Id == toDoListItemId) > 0;
        }
        public async Task MarkToDone(ToDoListItems toDoListItems)
        {
         
            _context.Entry(toDoListItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }
}
