using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using ZwartsJWTApi.Models;

namespace ZwartsJWTApi.Authentication
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ICollection<ToDoList> toDoList { get; set; }
    }
}
