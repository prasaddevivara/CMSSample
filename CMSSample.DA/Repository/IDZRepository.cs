using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSSample.DomainModel;

namespace CMSSample.DA.Repository
{
    public interface IDZRepository : IDisposable
    {
        IEnumerable<DZ> GetDZs();
        DZ GetDZByID(int DZId);
        IEnumerable<DZ> GetDZByDZName(string DZName);
        void InsertDZ(DZ dz);
        void Delete(Object dzID);
        void UpdateDZ(DZ dz);
        void Save();
    }
}
