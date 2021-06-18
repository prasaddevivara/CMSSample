using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CMSSample.DA.Repository
{
    public interface ITaskRepository : IDisposable
    {
        IEnumerable<TaskDisplayViewModel> GetTasks();

        TaskEditViewModel GetTaskByID(int taskid);
        //TaskDisplayViewModel GetTaskByName(string TaskName);
        void InsertTask(Task userTask);
        void Delete(Object taskID);
        void UpdateTask(Task task);
        void Save();
    }
}
