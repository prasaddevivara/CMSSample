using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;

namespace CMSSample.DA.Repository
{  
    public interface IODZCaseRepository : IDisposable
    {
        IEnumerable<ODZCaseDisplayViewModel> GetODZCases();
        ODZCase GetODZCaseByID(int ODZCaseId);
        IEnumerable<ODZCase> GetODZCaseReference(int ODZCaseReference);
        void InsertODZCase(ODZCase odzcase);
        void Delete(Object ODZCaseID);
        void UpdateODZCase(ODZCase odzcase);
        void Save();
    }
}
