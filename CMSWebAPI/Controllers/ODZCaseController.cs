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
    [CustomAuthenticationFilter(Roles = "Admin,User")]
    public class ODZCaseController : ApiController
    {

        private readonly IODZCaseRepository _repository;
        private readonly IIncidentTypeRepository _inctyprepository;
        private readonly IDZRepository _dzrepository;
        private readonly IUserRepository _userrepository;

        public ODZCaseController(IODZCaseRepository repository, IIncidentTypeRepository inctyprepository, IDZRepository dzrepository, IUserRepository userrepository)
        {
            _repository = repository;
            _inctyprepository = inctyprepository;
            _dzrepository = dzrepository;
            _userrepository = userrepository;
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
                CaseCreationDate = Convert.ToDateTime(odzcase.CaseCreationDate),
                CaseCoverageAmount = odzcase.CaseCoverageAmount,
                AssistedPerson = odzcase.AssistedPerson,
                CaseDescription = odzcase.CaseDescription
            };
            _repository.UpdateODZCase(odzcupd);
        }



        [HttpPut, Route("api/ODZCase/{userName}")]
        public void PutCaseValidate([FromUri] string userName, [FromBody] ODZCaseValidateViewModel odzcasevalidatevm)
        {
            var user = new UserDisplayViewModel();
            user = _userrepository.GetUserByUserName(userName);

            var odzcase = new ODZCase()
            {
                ODZCaseID = odzcasevalidatevm.ODZCaseID,
                ValidationDate = DateTime.Now,
                ValidationDesc = odzcasevalidatevm.ValidationDesc,
                ValidatedByUser = user.UserId
            };

            _repository.UpdateODZCaseValidation(odzcase);
        }

        [HttpPut, Route("api/ODZCase/{userName}/CaseClose")]
        public void PutCaseCloser([FromUri] string userName, [FromBody] ODZCaseCloseViewModel odzcaseclosevm)
        {
            var user = new UserDisplayViewModel();
            user = _userrepository.GetUserByUserName(userName);

            var odzcase = new ODZCase()
            {
                ODZCaseID = odzcaseclosevm.ODZCaseID,
                ClosedByDate = DateTime.Now,
                ClosingDesc = odzcaseclosevm.ClosingDesc,
                ClosedByuser = user.UserId
            };

            _repository.UpdateODZCaseClose(odzcase);
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
                CaseCoverageAmount = odzcase.CaseCoverageAmount,
                AssistedPerson = odzcase.AssistedPerson,
                CaseDescription = odzcase.CaseDescription                
            };

            _repository.InsertODZCase(odzcins);
        }
    }
}
