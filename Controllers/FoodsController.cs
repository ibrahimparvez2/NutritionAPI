using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using CountingKs.Models;
using CountingKsDataLayer;
using CountingKsDataLayer.Pocos;

namespace CountingKs.Controllers
{
  [RoutePrefix("Foods")]
  public class FoodsController : BaseController
  {
        public FoodsController(ICountingKsRepository repo):base(repo)
        {
        }
        
        [HttpGet]
        [Route("GetFoodsTest/{page?}", Name ="GetFoods")]
        public object Get(bool includeMeasures = true, int page=0)
        {
            IQueryable<Food> query;
            const int PAGE_SIZE = 50;

            if (includeMeasures)
            {
            query = TheRepository.GetAllFoodsWithMeasures();
            }
            else
            {
            query = TheRepository.GetAllFoods();
            }

            var baseQuery = query.OrderBy(f => f.Description);

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

            var helper = new UrlHelper(Request);
            var prevPageUrl = page > 0 ? helper.Link("GetFoods", new { page = page - 1 }) : "";
            var NextPageUrl = page < totalPages - 1 ? helper.Link("GetFoods", new { page = page + 1 }) : "";

            var results = baseQuery.Skip(PAGE_SIZE * page)
                                .Take(PAGE_SIZE)
                                .ToList()
                                .Select(f => TheModelFactory.Create(f));

            return new
            {
                TotalPage = totalPages,
                TotalCount = totalCount,
                PreviousPage = prevPageUrl,
                NextPage = NextPageUrl,
                Results = results
            };
        }

        [HttpGet, Route("GetFoodItem/{foodid}", Name ="GetFoodbyId")]
        public FoodModel Get(int foodid)
        {
            return TheModelFactory.Create(TheRepository.GetFood(foodid));
        }
  }
}
