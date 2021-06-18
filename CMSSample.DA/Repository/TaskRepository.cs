using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace CMSSample.DA.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private CMSSampleDAContext _context;

        public TaskRepository(CMSSampleDAContext cmssampledacontext)
        {
            this._context = cmssampledacontext;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TaskDisplayViewModel> GetTasks()
        {
            using (_context)
            {
                List<Task> tasks = new List<Task>();
                tasks = _context.Task.AsNoTracking()
                    .Include(x => x.ODZCase)
                    .Include(x => x.User)
                    .Include(x => x.TaskType)
                    .ToList();

                if (tasks != null)
                {
                    List<TaskDisplayViewModel> tasksDisplay = new List<TaskDisplayViewModel>();
                    foreach (var x in tasks)
                    {
                        var taskDisplay = new TaskDisplayViewModel()
                        {
                            TaskId = x.TaskId,
                            TaskTypeId = x.TaskTypeID,
                            TaskTypeName = x.TaskType.TaskTypeName,
                            TaskDescription = x.TaskDescription,
                            ODZCaseReference = x.ODZCase.ODZCaseReference,
                            CompletedDate = Convert.ToString(x.CompletedDate),
                            CreatedDate = Convert.ToString(x.CreatedDate),
                            UserName = x.User.UserName,
                            UserId = x.User.UserId,
                            ODZCaseID = x.ODZCase.ODZCaseID
                        };
                        tasksDisplay.Add(taskDisplay);
                    }
                    return tasksDisplay;
                }
                return null;
            }
        }
      
        public TaskEditViewModel GetTaskByID(int taskid)
        {
            var tasktypeRepo = new TaskTypeRepository(_context);
            
            var tsk = _context.Task.Where(x => x.TaskId == taskid).FirstOrDefault();
            var tskedt = new TaskEditViewModel()
            {
                TaskId = tsk.TaskId,
                TaskTypeId = tsk.TaskTypeID,
                TaskDescription = tsk.TaskDescription,
                ODZCaseID = tsk.ODZCaseID,
                CreatedDate = tsk.CreatedDate,
                CompletedDate = tsk.CompletedDate,
                UserId = tsk.UserId,
                TaskTypes = tasktypeRepo.GetTaskTypes()
            };
            return tskedt;
        }


        public void Delete(object id)
        {
            Task tsk = new Task();
            tsk = _context.Task.Find(id);
            _context.Task.Remove(tsk);
            Save();
        }

        public void InsertTask(Task task)
        {
            _context.Task.Add(task);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            Save();
        }
    }
}
