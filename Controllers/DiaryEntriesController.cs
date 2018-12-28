using CountingKs.Models;
using CountingKs.Services;
using CountingKsDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{  
    [RoutePrefix("DiaryEntries")]
    public class DiaryEntriesController : BaseController
    {
        private ICountingKsIdentityService _identityService;

        public DiaryEntriesController(ICountingKsRepository repo, ICountingKsIdentityService identityService)
          : base(repo)
        {
            _identityService = identityService;
        }
    
        [HttpGet]
        [Route("GetDiaryEntrybyId/{diaryId}", Name = "GetDiaryEntrybyId")]
        public HttpResponseMessage Get(DateTime diaryId)
        {
            var results = TheRepository.GetDiaryEntries(_identityService.CurrentUser, diaryId.Date)
                                        .ToList()
                                        .Select(e => TheModelFactory.Create(e));

            if (results != null)
            {
                    return (Request.CreateResponse(HttpStatusCode.OK, results));
            }

            else
            {
                return (Request.CreateResponse(HttpStatusCode.NoContent));   
            }
 
        }

        [HttpGet]
        [Route("GetDiaryEntrybyFoodId/{diaryId}/{foodId}", Name = "GetDiaryEntrybyFoodId")]
        public HttpResponseMessage Get(DateTime diaryId, int FoodId)
        {

            var results = TheRepository.GetDiaryEntries(_identityService.CurrentUser, diaryId.Date);
            var resultsbyfood = results.Where(f => f.FoodItem.Id == FoodId).ToList().Select(f => TheModelFactory.Create(f));

            if (resultsbyfood == null)
            {
            return Request.CreateResponse(HttpStatusCode.NotFound);
            }

        
            return Request.CreateResponse(HttpStatusCode.OK, resultsbyfood);
        }
         
        [HttpPost]
        [Route("PostNewDiaryEntry/{diaryId}", Name = "PostNewDiaryEntry")]
        public HttpResponseMessage Post(DateTime diaryID, [FromBody]DiaryEntryModel model)
        {
            try
            {
                var entity = TheModelFactory.Parse(model);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot read diary entry from Request Body");
                }

                var diary = TheRepository.GetDiary(_identityService.CurrentUser, diaryID);

                //check if diary exists
                if (diary == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot find diary");
                }
                //check for duplicate entries
                if (diary.Entries.Any(e => e.Measure.Id == entity.Measure.Id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot create diary entry as it already exists would you like to update entry");
                }

                diary.Entries.Add(entity);
                if (TheRepository.SaveAll())
                {

                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(entity));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot save to db");
                }
            }
            catch (Exception e)

            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not create entity");
            }

        }

        [HttpPatch]
        [Route("Update/{diaryId}/{Id}", Name ="UpdateDiaryEntry")]
        public HttpResponseMessage Patch(DateTime diaryId, int Id, [FromBody] DiaryEntryModel model)
        {

            try
            {
                var entity = TheRepository.GetDiaryEntry(_identityService.CurrentUser, diaryId, Id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot find diary entry");
                }

                var parsedValue = TheModelFactory.Parse(model);

                if (parsedValue == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot parse model to diaryentry in model factory");
                }

                if (entity.Quantity != parsedValue.Quantity)
                {
                    entity.Quantity = parsedValue.Quantity;

                    if (TheRepository.SaveAll())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

            //FALLBACK
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception e)

            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not find entity");
            }


        }

     
    }
}
