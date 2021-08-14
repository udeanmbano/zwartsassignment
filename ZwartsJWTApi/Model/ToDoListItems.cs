using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZwartsJWTApi.Authentication
{
    public class ToDoListItems
    {

        [Key]
        public int ToDoListItemId { get; set; }
        [Required(ErrorMessage = "To Do Item is required")]
        public string ToDoItem { get; set; }
        public bool ItemDoneStatus { get; set; }
        public int ToDoListId { get; set; }
        public virtual ToDoList toDoList { get; set; }
    }
}
