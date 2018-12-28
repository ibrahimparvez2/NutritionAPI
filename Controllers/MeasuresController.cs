using CountingKs.Models;
using CountingKsDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CountingKs.Controllers
{
    [RoutePrefix("Measures")]
    public class MeasuresController : BaseController
    {

        public MeasuresController(ICountingKsRepository repo) : base(repo)
        {
        }

        [Route("GetFoodMeasures/{foodid}", Name ="GetAllMeasuresforFood")]
        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = TheRepository.GetMeasuresForFood(foodid)
                               .ToList()
                               .Select(m => TheModelFactory.Create(m));

            return results;
        }

        [Route("GetFoodMeasures/{foodid}/{id}", Name = "GetMeasureforFood")]
        public MeasureModel Get(int foodid, int id)
        {
            var results = TheRepository.GetMeasure(id);

            if (results.Food.Id == foodid)
            {
                return TheModelFactory.Create(results);
            }

            return null;
        }
    }
}

