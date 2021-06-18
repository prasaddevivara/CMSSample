using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CMSWebAPI.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskRepository _repository;
        private readonly ITaskTypeRepository _itasktyperepository;
        private readonly IUserRepository _iuserrepository;
        //private readonly IDZRepository _dzrepository;

        public TaskController(ITaskRepository repository, ITaskTypeRepository tasktyperepository, IUserRepository iuserrepository)
        {
            _repository = repository;
            _itasktyperepository = tasktyperepository;
            _iuserrepository = iuserrepository;            
        }

        [CustomAuthenticationFilter(Roles = "Admin, User")]
        public IEnumerable<TaskDisplayViewModel> Get()
        {
            try
            {
                return _repository.GetTasks().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [CustomAuthenticationFilter(Roles = "Admin, User")]
        [Route("api/Task/Edit")]
        public TaskEditViewModel GetTaskByEdit(int odzCaseId, string username)
        {
            UserDisplayViewModel usr = _iuserrepository.GetUserByUserName(username);

            var tsks = new TaskEditViewModel()
            {
                TaskTypes = _itasktyperepository.GetTaskTypes(),
                ODZCaseID = odzCaseId,
                UserId = usr.UserId
            };

            return tsks;
        }

        [CustomAuthenticationFilter(Roles = "Admin, User")]
        [HttpPost]
        public void PostTassk(TaskEditViewModel task)
        {
            var tsk = new Task()
            {
                TaskTypeID = task.TaskTypeId,
                TaskDescription = task.TaskDescription,
                ODZCaseID = task.ODZCaseID,
                CreatedDate = DateTime.Now,
                CompletedDate = task.CompletedDate,  
                UserId = task.UserId
            };

            _repository.InsertTask(tsk);
        }

        [CustomAuthenticationFilter(Roles = "Admin, User")]
        [Route("api/Task/{id}")]       
        public TaskEditViewModel GetTaskByID(int id)
        {
            return _repository.GetTaskByID(id);
        }

        [CustomAuthenticationFilter(Roles = "Admin, User")]
        [HttpPut]        
        public void UpdateTask(TaskEditViewModel task)
        {
            var tsk = new Task()
            {
                TaskId = task.TaskId,
                TaskTypeID = task.TaskTypeId,
                TaskDescription = task.TaskDescription,
                ODZCaseID = task.ODZCaseID,
                CreatedDate = task.CreatedDate,
                CompletedDate = task.CompletedDate,
                UserId = task.UserId
            };
            _repository.UpdateTask(tsk);
        }


        //[CustomAuthenticationFilter(Roles = "Admin, User")]
        //[HttpPost]
        //[Route("api/Task/{id}/UpdateStatus")]
        //public void UpdateTaskStatus(int id)
        //{
        //   // _repository.UpdateTaskStatus(id);
        //}


        [CustomAuthenticationFilter(Roles = "Admin, User")]
        [HttpDelete, Route("api/Task/{id}/TaskRemove")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

    }
}