using CSIX.Data;
using CSIX.Model;
using CSIX.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CSIX.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IDBService _dbService;

        public ActivitiesController(IDBService dbService)
        {
            _dbService = dbService;
        }


        [HttpPost]
        [ActionName("Create")]
        public CreationResponse CreatePost([FromBody] ActivityModel activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CreationResponse _resp = _dbService.SaveActivity(activity);
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
        public CreationResponse DeleteConfirmed(string id)
        {
            try
            {
                CreationResponse _resp = _dbService.DeleteActivity(id);
                return _resp;
            }
            catch (Exception ex)
            {
                return new CreationResponse() { IsSaved = false, Message = "Error Occured" };
            }
        }

    }
}
