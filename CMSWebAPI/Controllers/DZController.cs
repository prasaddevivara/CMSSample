using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace CMSWebAPI.Controllers
{
    public class DZController : ApiController
    {
        private readonly IDZRepository _repository;

        public DZController(IDZRepository repository)
        {
            _repository = repository;
        }
                   
        public IEnumerable<SelectListItem> GetDZs()
        {
            return _repository.GetDZs();
        }
        [Route("api/DZ/Edit")]
        public IEnumerable<DZ> GetAllDZs()
        {
            return _repository.GetAllDZs();
        }
    }
}
