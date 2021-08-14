using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwartsJWTApi.Models;

namespace ZwartsJWTApi.Interface
{
    interface IToDoListRepository
    {
        List<ToDoList> GetToDoLists(String UserId);
        ToDoList GetToDoListByID(int toDoListId);
        Task InsertToDoList(ToDoList toDoList);
        Task DeleteToDoList(int toDoListId);
        Task UpdateToDoList(ToDoList toDoList);
        bool ToDoListExists(int toDoListId);
        Task Save();
    }
}
