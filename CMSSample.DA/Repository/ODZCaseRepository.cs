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

        public ODZCaseEditViewModel CreateODZCase()
        {
            var incRepo = new IncidentTypeRepository(_context);
            var dzRepo = new DZRepository(_context);
            var odzcase = new ODZCaseEditViewModel()
            {
                IncidentTypes = incRepo.GetIncidentTypes(),
                DZS = dzRepo.GetDZs()
            };

            return odzcase;
        }

        //public ODZCase GetODZCaseByID(int ODZCaseId)
        //{
        //    return _context.ODZCase.Find(ODZCaseId);
        //}

        public ODZCaseEditViewModel GetODZCaseByID(int odzcID)
        {
            var incRepo = new IncidentTypeRepository(_context);
            var dzRepo = new DZRepository(_context);
            var odzc = _context.ODZCase.Where(x => x.ODZCaseID == odzcID).FirstOrDefault();
            var odzcase = new ODZCaseEditViewModel()
            {
                ODZCaseID = odzc.ODZCaseID,
                ODZCaseReference = odzc.ODZCaseReference,
                IncidentTypeID = odzc.IncidentTypeID.ToString(),
                IncidentTypes = incRepo.GetIncidentTypes(),
                SelectedCountryofIncidentID = odzc.CountryofIncidentID,
                DZS = dzRepo.GetDZs(),
                CaseCoverageAmount = odzc.CaseCoverageAmount,
                AssistedPerson = odzc.AssistedPerson,
                CaseDescription = odzc.CaseDescription
            };
            //return _context.ODZCase.Where(x => x.ODZCaseID == odzcID).FirstOrDefault();
            return odzcase;
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
