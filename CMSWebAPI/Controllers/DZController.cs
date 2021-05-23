using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMSWebAPI.Controllers
{
    public class DZController : ApiController
    {
        private readonly IDZRepository _repository;

        public DZController(IDZRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]        
        public IEnumerable<DZ> GetDZs()
        {
            return _repository.GetDZs();
        }
    }
}
