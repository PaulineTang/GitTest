using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.IServices
{
    public interface IssuesSource
    {
        Task<Issue> FindAsync(string id);

        Task<Issue> CreatAsync(Issue issue);

    }
}