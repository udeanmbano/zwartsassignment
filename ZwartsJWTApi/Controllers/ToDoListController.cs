using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZwartsJWTApi.Authentication;
using ZwartsJWTApi.Interface;
using ZwartsJWTApi.Model;
using ZwartsJWTApi.Models;
using ZwartsJWTApi.Repositories;

namespace ZwartsJWTApi.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private IToDoListRepository _toDoListRepository;
        private readonly ApplicationDbContext _db;

        public ToDoListController(ApplicationDbContext db)
        {
            _toDoListRepository = (IToDoListRepository)new ToDoListRepository(db);
        }

        [HttpGet]
        [Route("api/ToDoLists/{id}")]
        public JsonResult ToDoLists(string id)
        {
            try
            {
                List<ToDoList> toDoLists = this._toDoListRepository.GetToDoLists(id);
                return new JsonResult((object)new MessageObject()
                {
                    ToDoLists = toDoLists,
                    ResponseCode = 200,
                    Message = "Success",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            }
            catch (Exception ex)
            {
                return new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 200,
                    Message = ex.Message,
                    StatusCode = -1
                })
                {
                    StatusCode = new int?(201)
                };
            }
        }

        [Route("api/GetToDoList/{id}")]
        public object GetToDoList(int id)
        {
            try
            {
                ToDoList toDoListById = this._toDoListRepository.GetToDoListByID(id);
                return (object)new JsonResult((object)new MessageObject()
                {
                    Message = (toDoListById != null ? "Success" : "Not found"),
                    ToDoLists = new List<ToDoList>() { toDoListById },
                    ResponseCode = 200,
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            }
            catch (Exception ex)
            {
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 200,
                    Message = ex.Message,
                    StatusCode = -1
                })
                {
                    StatusCode = new int?(201)
                };
            }
        }

        [Route("api/PutToDoList/{id}")]
        public async Task<object> PutToDoList(int id, ToDoList toDoList)
        {
            ToDoListController doListController = this;
            if (!doListController.ModelState.IsValid)
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 400,
                    Message = "Bad request",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            if (id != toDoList.Id)
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 400,
                    Message = "Bad request",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            try
            {
                await doListController._toDoListRepository.UpdateToDoList(toDoList);
                return (object)new JsonResult((object)new MessageObject()
                {
                    ToDoLists = new List<ToDoList>() { toDoList },
                    ResponseCode = 200,
                    Message = "Updated successfully",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return (object)new JsonResult((object)new MessageResponse()
                {
                    Message = (doListController.ToDoListExists(id) ? ex.Message : "Not found"),
                    ResponseCode = 200,
                    StatusCode = -1
                })
                {
                    StatusCode = new int?(201)
                };
            }
        }

        [Route("api/PostToDoList")]
        public async Task<object> PostToDoList(ToDoList toDoList)
        {
            ToDoListController doListController = this;
            try
            {
                if (!doListController.ModelState.IsValid)
                    return (object)doListController.BadRequest(doListController.ModelState);
                await doListController._toDoListRepository.InsertToDoList(toDoList);
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 200,
                    Message = "Inserted successfully",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            }
            catch (Exception ex)
            {
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 200,
                    Message = ex.Message,
                    StatusCode = -1
                })
                {
                    StatusCode = new int?(201)
                };
            }
        }

        [Route("api/DeleteToDoList/{id}")]
        public async Task<object> DeleteToDoList(int id)
        {
            MessageObject jsonObject = new MessageObject();
            try
            {
                if (this._toDoListRepository.GetToDoListByID(id) == null)
                {
                    jsonObject.ResponseCode = 200;
                    jsonObject.Message = "Todo list does not exsist.";
                    jsonObject.StatusCode = -1;
                    return (object)new JsonResult((object)jsonObject)
                    {
                        StatusCode = new int?(201)
                    };
                }
                await this._toDoListRepository.DeleteToDoList(id);
                jsonObject.ResponseCode = 200;
                jsonObject.Message = "Deleted successfully";
                jsonObject.StatusCode = 0;
                return (object)new JsonResult((object)jsonObject)
                {
                    StatusCode = new int?(201)
                };
            }
            catch (Exception ex)
            {
                jsonObject.ResponseCode = 200;
                jsonObject.Message = ex.Message;
                jsonObject.StatusCode = -1;
                return (object)new JsonResult((object)jsonObject)
                {
                    StatusCode = new int?(201)
                };
            }
        }

        private bool ToDoListExists(int id) => this._toDoListRepository.ToDoListExists(id);
    }
}
