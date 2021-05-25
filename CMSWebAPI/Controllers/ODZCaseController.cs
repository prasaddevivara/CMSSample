using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMSWebAPI.Controllers
{
    public class ODZCaseController : ApiController
    {

        private readonly IODZCaseRepository _repository;
        private readonly IIncidentTypeRepository _repository1;

        public ODZCaseController(IODZCaseRepository repository, IIncidentTypeRepository repository1)
        {
            _repository = repository;
            _repository1 = repository1;
        }

        [HttpGet]        
        public IEnumerable<ODZCaseDisplayViewModel> GetODZCases()
        {
            return _repository.GetODZCases().ToList();
        }

        [HttpGet]
        
        public IEnumerable<ODZCase> GetODZCaseReference(int ODZCaseReference)
        {
            return _repository.GetODZCaseReference(ODZCaseReference);
        }

        [HttpPut]
        
        public void UpdateUser(ODZCase odzcase)
        {
            _repository.UpdateODZCase(odzcase);
        }
                
        public void Delete(int odzcaseid)
        {
            _repository.Delete(odzcaseid);
        }

        [HttpPost]
        public void PostUsers(ODZCase odzcase)
        {
            _repository.InsertODZCase(odzcase);
        }
    }
}
