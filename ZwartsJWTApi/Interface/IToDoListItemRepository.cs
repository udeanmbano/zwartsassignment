using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwartsJWTApi.Models;

namespace ZwartsJWTApi.Interface
{
    interface IToDoListItemRepository
    {
        List<ToDoListItems> GetToDoItemLists(int toDoListId);
        ToDoListItems GetToDoListItemByID(int toDoListId);
        Task InsertToDoListItem(ToDoListItems toDoListItems);
        Task DeleteToDoListItem(int toDoListId);
        Task UpdateToDoListItem(ToDoListItems toDoListItems);
        bool ToDoListItemExists(int toDoListId);
        Task MarkToDone(ToDoListItems toDoListItems);
        Task Save();
    }
}
