using CSIX.Hubs;
using CSIX.Model;
using CSIX.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CSIX.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IDBService _dbService;
        private readonly IHubContext<TasksHub> _hubContext;
        public TasksController(IDBService dbService, IHubContext<TasksHub> hubContext)
        {
            _dbService = dbService;
            _hubContext = hubContext;
        }


        [HttpPost]
        [ActionName("Create")]
        public async Task<CreationResponse> CreatePost([FromBody] TaskModel Task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CreationResponse _resp = _dbService.SaveTask(Task);
                    var _n = _dbService.GetAllTasks("", _resp.TaskId);
                    await _hubContext.Clients.All.SendAsync("TaskUpdate", "All", _n);

                    if (!string.IsNullOrEmpty(_n[0].UserId))
                    {
                        await _hubContext.Clients.User(_n[0].UserId ?? "").SendAsync("MyTaskUpdate", "Specific", _n);
                    }
                    if (!string.IsNullOrEmpty(_resp.OldUserId) && _resp.OldUserId != _n[0].UserId)
                    {
                        await _hubContext.Clients.User(_resp.OldUserId).SendAsync("MyTaskDeleted", "Specific", _n[0].Id);
                    }

                    return _resp;
                }
                return new CreationResponse() { IsSaved = false, Message = "Validation Failed" };
            }
            catch (Exception ex)
            {
                return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
            }

        }


        [HttpPost, ActionName("Delete")]
        public async Task<CreationResponse> DeleteConfirmed(string id)
        {
            try
            {
                CreationResponse _resp = _dbService.DeleteTask(id);
                if (!string.IsNullOrEmpty(_resp.OldUserId))
                {
                    await _hubContext.Clients.User(_resp.OldUserId).SendAsync("MyTaskDeleted", "Specific", id);
                }
                await _hubContext.Clients.All.SendAsync("AllTasksDeleted", "All", id);
                return _resp;
            }
            catch (Exception ex)
            {
                return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
            }
        }

    }
}
