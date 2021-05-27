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
        private readonly IIncidentTypeRepository _inctyprepository;
        private readonly IDZRepository _dzrepository;

        public ODZCaseController(IODZCaseRepository repository, IIncidentTypeRepository inctyprepository, IDZRepository dzrepository)
        {
            _repository = repository;
            _inctyprepository = inctyprepository;
            _dzrepository = dzrepository;
        }

           
        public IEnumerable<ODZCaseDisplayViewModel> Get()
        {
            return _repository.GetODZCases().ToList();
        }

        [Route("api/ODZCase/Edit")]
        public ODZCaseEditViewModel GetODZCaseByEdit()
        {
            var odzcase = new ODZCaseEditViewModel()
            {
                IncidentTypes = _inctyprepository.GetIncidentTypes(),
                DZS = _dzrepository.GetDZs()
            };

            return odzcase;
        }

        [Route("api/ODZCase/{id}")]
        
        public ODZCaseEditViewModel GetODZCaseByID(int id)
        {
            return _repository.GetODZCaseByID(id);
        }

        [HttpPut]
        
        public void UpdateODZCase(ODZCaseEditViewModel odzcase)
        {
            var odzcupd = new ODZCase()
            {
                ODZCaseID = odzcase.ODZCaseID,
                ODZCaseReference = odzcase.ODZCaseReference,
                IncidentTypeID = Convert.ToInt32(odzcase.IncidentTypeID),
                CountryofIncidentID = odzcase.SelectedCountryofIncidentID,
                // IncidentTypes = _inctyprepository.GetIncidentTypes(),
                CaseCoverageAmount = odzcase.CaseCoverageAmount,
                AssistedPerson = odzcase.AssistedPerson,
                CaseDescription = odzcase.CaseDescription
            };
            _repository.UpdateODZCase(odzcupd);
        }
               
        [HttpDelete, Route("api/ODZCase/{id}/CaseRemove")]
        public void DeleteODZCaseByID(int id)
        {
            _repository.Delete(id);
        }

        [HttpPost]
        public void PostODZCase(ODZCaseEditViewModel odzcase)
        {
            var odzcins = new ODZCase()
            {
                ODZCaseReference = odzcase.ODZCaseReference,
                IncidentTypeID = Convert.ToInt32(odzcase.IncidentTypeID),
                CountryofIncidentID = odzcase.SelectedCountryofIncidentID,
               // IncidentTypes = _inctyprepository.GetIncidentTypes(),
                CaseCoverageAmount = odzcase.CaseCoverageAmount,
                AssistedPerson = odzcase.AssistedPerson,
                CaseDescription = odzcase.CaseDescription                
            };

            _repository.InsertODZCase(odzcins);
        }
    }
}
