using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DA.Repository
{
    public class ODZCaseRepository : IODZCaseRepository
    {

        private CMSSampleDAContext _context;

        public ODZCaseRepository(CMSSampleDAContext cmssampledacontext)
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

        public IEnumerable<ODZCaseDisplayViewModel> GetODZCases()
        {
            using (_context)
            {
                List<ODZCase> odzcases = new List<ODZCase>();
                odzcases = _context.ODZCase.AsNoTracking()
                    .Include(x => x.IncidentType)
                    .Include(x => x.DZ)
                    .ToList();

                if (odzcases != null)
                {
                    List<ODZCaseDisplayViewModel> odzcasesDisplay = new List<ODZCaseDisplayViewModel>();
                    foreach (var x in odzcases)
                    {
                        var odzcaseDisplay = new ODZCaseDisplayViewModel()
                        {
                            ODZCaseID = x.ODZCaseID,
                            ODZCaseReference = x.ODZCaseReference,
                            IncidentTypeName = x.IncidentType.IncidentTypeName,
                            DZName = x.DZ.DZName,
                            CaseCoverageAmount=x.CaseCoverageAmount,
                            AssistedPerson = x.AssistedPerson,
                            CaseDescription = x.CaseDescription
                        };
                    odzcasesDisplay.Add(odzcaseDisplay);
                    }
                    return odzcasesDisplay;
                }
                return null;
            }
        }

        public ODZCase GetODZCaseByID(int ODZCaseId)
        {
            return _context.ODZCase.Find(ODZCaseId);
        }

        public IEnumerable<ODZCase> GetODZCaseReference(int ODZCaseReference)
        {
            return _context.ODZCase.Where(x => x.ODZCaseReference == ODZCaseReference).ToList();
        }

        public void InsertODZCase(ODZCase odzcase)
        {
            _context.ODZCase.Add(odzcase);
            Save();
        }

        public void UpdateODZCase(ODZCase odzcase)
        {
            _context.Entry(odzcase).State = EntityState.Modified;
            Save();
        }

        public void Delete(object odzcaseID)
        {
            ODZCase odzcase = new ODZCase();
            odzcase = _context.ODZCase.Find(odzcaseID);
            _context.ODZCase.Remove(odzcase);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
