using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZwartsJWTApi.Authentication;

namespace ZwartsJWTApi.Models
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<ToDoListItems> toDoListItems { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
    }
}
