using CSIX.Data;
using CSIX.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CSIX.Services
{
    public interface IDBService
    {
        List<UserModel> GetAllUsers();
        List<ActivityModel> GetAllActivities();
        CreationResponse SaveActivity(ActivityModel _model);
        CreationResponse DeleteActivity(string Id);

        List<TaskModel> GetAllTasks(string UserId = "", string TaskId = "");
        CreationResponse SaveTask(TaskModel _model);
        CreationResponse DeleteTask(string Id);

    }
    public class DBService : IDBService
    {
        private readonly CSIXContext _context;
        public DBService(CSIXContext context)
        {
            _context = context;
        }
        public void LogError(Exception ex)
        {

        }
        public void UpdateActivitiesHub()
        {

        }
        public List<ActivityModel> GetAllActivities()
        {
            try
            {
                var _all = _context.Activities.Where(x => x.IsDeleted == false).Select(x => new ActivityModel()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return _all;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return new List<ActivityModel>();
        }
        public CreationResponse SaveActivity(ActivityModel _model)
        {
            try
            {
                if (string.IsNullOrEmpty(_model.Id))
                {
                    _model.Id = Guid.NewGuid().ToString("N");

                    Activities _a = new Activities()
                    {
                        Description = _model.Description,
                        Id = _model.Id,
                        Name = _model.Name
                    };
                    _context.Activities.Add(_a);
                    _context.SaveChanges();
                    UpdateActivitiesHub();
                    return new CreationResponse() { IsSaved = true, Message = "Saved Successfully" };
                }
                else
                {
                    var _a = _context.Activities.Where(x => x.Id == _model.Id && x.IsDeleted == false).FirstOrDefault();
                    _a.Description = _model.Description;
                    _context.SaveChanges();
                    UpdateActivitiesHub();
                    return new CreationResponse() { IsSaved = true, Message = "Saved Successfully" };
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
        }

        public CreationResponse DeleteActivity(string Id)
        {
            try
            {
                var _a = _context.Activities.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
                _a.IsDeleted = true;
                _a.DeleteDate = DateTime.Now;
                _context.SaveChanges();
                UpdateActivitiesHub();
                return new CreationResponse() { IsSaved = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
            }
        }
        public List<TaskModel> GetAllTasks(string UserId = "", string TaskId = "")
        {
            try
            {
                var _all = _context.Tasks
                    .Where(x => x.IsDeleted == false && (string.IsNullOrEmpty(UserId) || x.UserId == UserId) && (string.IsNullOrEmpty(TaskId) || x.Id == TaskId))
                    .OrderByDescending(x=>x.Created_Date)
                    .Include(y => y.User)
                    .Include(y => y.Activity)

                    .Select(x =>
                new TaskModel()
                {
                    ActivityId = x.ActivityId,
                    Status = x.Status,
                    UserId = x.UserId,
                    ActivityName = x.Activity == null ? "" : x.Activity.Name,
                    UserName = x.User == null ? "" : x.User.UserName,
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return _all;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return new List<TaskModel>();
        }
        public CreationResponse SaveTask(TaskModel _model)
        {
            try
            {
                if (string.IsNullOrEmpty(_model.Id))
                {
                    _model.Id = Guid.NewGuid().ToString("N");

                    Tasks _a = new Tasks()
                    {
                        Description = _model.Description,
                        ActivityId = _model.ActivityId,
                        Status = _model.Status,
                        Created_Date = DateTime.Now,
                        IsDeleted = false,
                        UserId = _model.UserId,
                        Id = _model.Id,
                        Name = _model.Name
                    };
                    _context.Tasks.Add(_a);
                    _context.SaveChanges();
                    return new CreationResponse() { IsSaved = true, TaskId = _model.Id, OldUserId = "", Message = "Saved Successfully" };
                }
                else
                {
                    var _a = _context.Tasks.Where(x => x.Id == _model.Id && x.IsDeleted == false).FirstOrDefault();
                    var _old = _a.UserId;
                    _a.Name = _model.Name;
                    _a.Description = _model.Description;
                    _a.ActivityId = _model.ActivityId;
                    _a.Status = _model.Status;
                    _a.UserId = _model.UserId;

                    _context.SaveChanges();

                    return new CreationResponse() { IsSaved = true, TaskId = _model.Id, OldUserId = _old ?? "", Message = "Saved Successfully" };
                }
            }
            catch(Exception ex)
            {
                LogError(ex);
            }
            return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
        }

        public CreationResponse DeleteTask(string Id)
        {
            try
            {
                var _a = _context.Tasks.Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefault();
                var _userId = _a.UserId;
                _a.IsDeleted = true;
                _a.DateDeleted = DateTime.Now;
                _context.SaveChanges();

                return new CreationResponse() { IsSaved = true, OldUserId = _userId ?? "", Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
            }
        }

        public List<UserModel> GetAllUsers()
        {
            try
            {
                var _all = _context.Users
                    .Select(x =>
                new UserModel()
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Email = x.Email
                }).ToList();
                return _all;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return new List<UserModel>();
        }
    }
}
