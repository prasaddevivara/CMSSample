using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSSample.DA.Repository
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private CMSSampleDAContext _context;

        public TaskTypeRepository(CMSSampleDAContext cmssampledacontext)
        {
            this._context = cmssampledacontext;
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
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

        public IEnumerable<SelectListItem> GetTaskTypes()
        {
            List<SelectListItem> TaskTypes = _context.TaskType.AsNoTracking()
                .OrderBy(n => n.TaskTypeName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.TaskTypeID.ToString(),
                        Text = n.TaskTypeName
                    }).ToList();
            var TaskTypesdisp = new SelectListItem()
            {
                Value = null,
                Text = "--- Select TaskType ---"
            };

            TaskTypes.Insert(0, TaskTypesdisp);
            return new SelectList(TaskTypes, "Value", "Text");
        }
    }
}
