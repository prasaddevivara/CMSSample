using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DA.Repository
{
    public class DZRepository : IDZRepository
    {
        private CMSSampleDAContext _context;

        public DZRepository(CMSSampleDAContext cmssampledacontext)
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

        public void Delete(object dzID)
        {
            _context.Database.Log("Deleted DZID: " + dzID);
            DZ dz = new DZ();
            dz = _context.DZ.Find(dzID);
            _context.DZ.Remove(dz);
            Save();
        }

        public IEnumerable<DZ> GetDZByDZName(string DZName)
        {
            return _context.DZ.Where(x => x.DZName == DZName).ToList();
        }

        public DZ GetDZByID(int DZId)
        {
            return _context.DZ.Find(DZId);
        }

        public IEnumerable<DZ> GetDZs()
        {
            return _context.DZ.ToList();
        }

        public void InsertDZ(DZ dz)
        {
            _context.Database.Log("New DZ Inserted" + dz.DZName);
            _context.DZ.Add(dz);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateDZ(DZ dz)
        {
            _context.Database.Log("DZ updated for DZID" + dz.DZId);
            _context.Entry(dz).State = EntityState.Modified;
            Save();
        }
    }
}
