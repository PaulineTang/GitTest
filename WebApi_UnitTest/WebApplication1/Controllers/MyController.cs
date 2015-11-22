using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication1.IServices;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MyController : ApiController
    {
        private readonly IssuesSource _issuesSource;
        public MyController(IssuesSource issuesSource)
        {
            _issuesSource = issuesSource;
        }

        [HttpGet]
        public async Task<Issue> Get(string id)
        {
            var issue = await _issuesSource.FindAsync(id);
            if (issue == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return issue;

        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(Issue issue)
        {
            var CreatedIssue = await _issuesSource.CreatAsync(issue);

            var link = Url.Link("DefaultApi", new { Controller = "My", id = CreatedIssue.Id });
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, CreatedIssue);
            response.Headers.Location = new Uri(link);

            return response;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Issue issue,bool  flag=false)
        {
            var CreatedIssue = await _issuesSource.CreatAsync(issue);

            var result = new CreatedAtRouteNegotiatedContentResult<Issue>(
               "DefaultApi",
               new Dictionary<string, object> { { "id", CreatedIssue.Id } },
               CreatedIssue,
               this
               );

            return result;
        }

    }
}
