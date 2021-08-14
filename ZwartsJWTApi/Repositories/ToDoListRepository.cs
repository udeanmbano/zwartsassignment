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
    public class ToDoListRepository : IToDoListRepository
    {
        private ApplicationDbContext _context;
        //initialise database context
        public ToDoListRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }
        public List<ToDoList> GetToDoLists(String UserId)
        {
            return _context.toDoLists.Where(a=>a.UserId.Equals(UserId)).ToList();
        }
       public ToDoList GetToDoListByID(int toDoListId)
        {
            return _context.toDoLists.Find(toDoListId);
        }
        public async Task InsertToDoList(ToDoList toDoList)
        {
            _context.toDoLists.Add(toDoList);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteToDoList(int toDoListId)
        {
            ToDoList toDoList = await _context.toDoLists.FindAsync(toDoListId);
            _context.toDoLists.Remove(toDoList);
            await _context.SaveChangesAsync();
        }
       public async Task UpdateToDoList(ToDoList toDoList)
        {
            _context.Entry(toDoList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public bool ToDoListExists(int toDoListId)
        {
            return _context.toDoLists.Count(e => e.Id == toDoListId) > 0;
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
