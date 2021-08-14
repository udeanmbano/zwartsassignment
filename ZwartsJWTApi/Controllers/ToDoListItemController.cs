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
    public class ToDoListItemController : ControllerBase
    {
        private IToDoListItemRepository _toDoListItemRepository;
        private readonly ApplicationDbContext _db;

        public ToDoListItemController(ApplicationDbContext db)
        {
            _toDoListItemRepository = (IToDoListItemRepository)new ToDoListItemRepository(db);
        }
        [HttpGet]
        [Route("api/ToDoListItems/{id}")]
        public JsonResult ToDoListItems(int id)
        {
            try
            {
                List<ZwartsJWTApi.Models.ToDoListItems> toDoItemLists = this._toDoListItemRepository.GetToDoItemLists(id);
                return new JsonResult((object)new MessageObjectItem()
                {
                    toDoListItems = toDoItemLists,
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

        [Route("api/GetToDoListItem/{id}")]
        public object GetToDoListItem(int id)
        {
            try
            {
                ZwartsJWTApi.Models.ToDoListItems toDoListItemById = this._toDoListItemRepository.GetToDoListItemByID(id);
                return (object)new JsonResult((object)new MessageObjectItem()
                {
                    Message = (toDoListItemById != null ? "Success" : "Not found"),
                    toDoListItems = new List<ZwartsJWTApi.Models.ToDoListItems>()
          {
            toDoListItemById
          },
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

        [Route("api/PutToDoListItem/{id}")]
        public async Task<object> PutToDoListItem(int id, ZwartsJWTApi.Models.ToDoListItems toDoListItem)
        {
            ToDoListItemController listItemController = this;
            if (!listItemController.ModelState.IsValid)
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 400,
                    Message = "Bad request",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            if (id != toDoListItem.ToDoListItemId)
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
                await listItemController._toDoListItemRepository.UpdateToDoListItem(toDoListItem);
                return (object)new JsonResult((object)new MessageObjectItem()
                {
                    toDoListItems = new List<ZwartsJWTApi.Models.ToDoListItems>()
          {
            toDoListItem
          },
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
                    Message = (listItemController.ToDoListExists(id) ? ex.Message : "Not found"),
                    ResponseCode = 200,
                    StatusCode = -1
                })
                {
                    StatusCode = new int?(201)
                };
            }
        }

        [Route("api/MarkToDoListItem/{id}")]
        public async Task<object> MarkToDoListItem(int id, ZwartsJWTApi.Models.ToDoListItems toDoListItem)
        {
            ToDoListItemController listItemController = this;
            if (!listItemController.ModelState.IsValid)
                return (object)new JsonResult((object)new MessageResponse()
                {
                    ResponseCode = 400,
                    Message = "Bad request",
                    StatusCode = 0
                })
                {
                    StatusCode = new int?(201)
                };
            if (id != toDoListItem.ToDoListItemId)
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
                toDoListItem.ItemDoneStatus = true;
                await listItemController._toDoListItemRepository.UpdateToDoListItem(toDoListItem);
                return (object)new JsonResult((object)new MessageObjectItem()
                {
                    toDoListItems = new List<ZwartsJWTApi.Models.ToDoListItems>()
          {
            toDoListItem
          },
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
                    Message = (listItemController.ToDoListExists(id) ? ex.Message : "Not found"),
                    ResponseCode = 200,
                    StatusCode = -1
                })
                {
                    StatusCode = new int?(201)
                };
            }
        }

        [Route("api/PostToDoListItem")]
        public async Task<object> PostToDoListItem(ZwartsJWTApi.Models.ToDoListItems toDoListItem)
        {
            ToDoListItemController listItemController = this;
            try
            {
                if (!listItemController.ModelState.IsValid)
                    return (object)listItemController.BadRequest(listItemController.ModelState);
                await listItemController._toDoListItemRepository.InsertToDoListItem(toDoListItem);
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

        [Route("api/DeleteToDoListItem/{id}")]
        public async Task<object> DeleteToDoList(int id)
        {
            MessageObject jsonObject = new MessageObject();
            try
            {
                if (this._toDoListItemRepository.GetToDoListItemByID(id) == null)
                {
                    jsonObject.ResponseCode = 200;
                    jsonObject.Message = "Todo list does not exsist.";
                    jsonObject.StatusCode = -1;
                    return (object)new JsonResult((object)jsonObject)
                    {
                        StatusCode = new int?(201)
                    };
                }
                await this._toDoListItemRepository.DeleteToDoListItem(id);
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

        private bool ToDoListExists(int id) => this._toDoListItemRepository.ToDoListItemExists(id);
    }
}
