using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSSample.DA.Repository
{
    public class IncidentTypeRepository : IIncidentTypeRepository
    {

        private CMSSampleDAContext _context;

        public IncidentTypeRepository(CMSSampleDAContext cmssampledacontext)
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

        //public IEnumerable<IncidentType> GetIncidentTypes()
        //{
        //    return _context.IncidentType.ToList();            
        //}


        public IEnumerable<SelectListItem> GetIncidentTypes()
        {            
            List<SelectListItem> IncidentTypes = _context.IncidentType.AsNoTracking()
                .OrderBy(n => n.IncidentTypeName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.IncidentTypeID.ToString(),
                        Text = n.IncidentTypeName
                    }).ToList();
            var IncidentTypesdisp = new SelectListItem()
            {
                Value = null,
                Text = "--- select IncidentType ---"
            };

            IncidentTypes.Insert(0, IncidentTypesdisp);
            return new SelectList(IncidentTypes, "Value", "Text");            
        }
    }
}
