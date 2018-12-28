using CountingKs.Models;
using CountingKsDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CountingKs.Controllers
{
    public class BaseController : ApiController
    {
        ICountingKsRepository _repo;
        ModelFactory _modelFactory;

        public BaseController(ICountingKsRepository repo)
        {
            _repo = repo;
        }

        protected ICountingKsRepository TheRepository
        {
            get
            {
                return _repo;
            }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, TheRepository);
                }
                return _modelFactory;
            }
        }

    }
}
