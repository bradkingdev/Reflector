using MediatR;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Reflector.Services.CQRS;
using ReflectorAPI.DependencyResolution;
using ReflectorAPI.Filters;

namespace Reflector.API.Controllers
{
    public class RelayController : ApiController
    {

        private readonly IMediator _mediatr;
        public RelayController()
        {
            _mediatr = DependencyResolver.Current.GetService<IMediator>();
        }

        // GET api/relay?customernumber=5&category=Tests&BeginningRange=2016-04-06T22:24:55Z&EndgingRange=2016-04-06T22:24:55Z
        public HttpResponseMessage Get(int? identifier = null, 
                                       string category = null,
                                       DateTimeOffset? beginningRange = null, 
                                       DateTimeOffset? endingRange = null)
        {
            var request = new ArtifactRequest
            {
                Identifier = identifier,
                BeginningRange = beginningRange,
                EndingRange = endingRange,
                Category = category
            };
            
            var envelope = _mediatr.Send(request);
            return Request.CreateResponse(HttpStatusCode.OK, envelope);

        }

        // POST api/<controller 
        [RequiresApiToken]
        public async Task<HttpResponseMessage> Post([FromBody]ArtifactEnvelope envelope)
        {

            try
            {
                await Task.WhenAll(_mediatr.SendAsync(envelope));
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
            catch(NullReferenceException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "POST did not serialize into envelope.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }



    }
}