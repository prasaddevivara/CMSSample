using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;

namespace CMSWebAPI.Controllers
{
    public class IncidentTypeController : ApiController
    {
        private readonly IIncidentTypeRepository _repository;

        public IncidentTypeController(IIncidentTypeRepository repository)
        {
            _repository = repository;
        }
                
        //[CustomAuthenticationFilter]
        public IEnumerable<SelectListItem> GetIncidentTypes()
        {
            return _repository.GetIncidentTypes();
        }

    }
}
