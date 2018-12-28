using CountingKs.Models;
using CountingKsDataLayer;
using CountingKsDataLayer.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Routing;

namespace CountingKs.Controllers
{
    [RoutePrefix("MyHome")]
    public class HomeController : ApiController
    {
    
        protected string Greetings;
        ModelFactory _modelFactory;
        ICountingKsRepository _repo;

        protected ICountingKsRepository TheRepository
        {
            get
            {
                if (_repo == null)
                {
                    _repo = new CountingKsRepository(new CountingKsContext());
                }

                return _repo;
            }
        }
        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, _repo);
                }
                return _modelFactory;
            }
        }

        [HttpGet, Route("GetResults")]
        public IEnumerable<FoodModel> Index()
        {

            //var repo = new CountingKsRepository(new CountingKsContext());

            var results = TheRepository.GetAllFoodsWithMeasures()
                            .Take(25)
                            .ToList().Select(f => TheModelFactory.Create(f));

            return results;

        }

    }

    public class MyClass
    {
        private readonly string Greeting;
        public string ModifiedResult;

        public MyClass(string greeting)
        {
            this.Greeting = greeting;
        }

        public void Modify()
        {
            this.ModifiedResult = this.Greeting.Substring(0, this.Greeting.Length - 1);
            this.FuncB();
        }

        private void FuncB()
        {
            this.ModifiedResult = this.Greeting.Substring(0, this.Greeting.Length - 1);
            this.FuncC();
        }

        private void FuncC()
        {
            this.ModifiedResult = this.Greeting.Substring(0, this.Greeting.Length - 1);
        }
    }

}
