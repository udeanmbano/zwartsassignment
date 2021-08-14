using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwartsJWTApi.Models;

namespace ZwartsJWTApi.Model
{
    public class MessageObjectItem
    {
        public List<ToDoListItems> toDoListItems { get; set; }

        public int ResponseCode { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
