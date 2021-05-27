using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CMSSample.DomainModel;

namespace CMSSample.DA.Repository
{
    public interface IIncidentTypeRepository : IDisposable
    {
        IEnumerable<SelectListItem> GetIncidentTypes();
        //IncidentType GetIncidentTypeByID(int incidenttypeId);
        //IEnumerable<IncidentType> GetIncidentTypeByDZName(string IncidentTypeName);
        //void InsertIncidentType(IncidentType incidenttype);
        //void Delete(Object dzIncidentType);
        //void UpdateIncidentType(IncidentType incidenttype);
        //void Save();
    }
}
