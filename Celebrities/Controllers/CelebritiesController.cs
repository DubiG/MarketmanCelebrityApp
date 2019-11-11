using Celebrities.DAL;
using Celebrities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Celebrities.Controllers
{
    public class CelebritiesController : ApiController
    {
        private readonly IRepository<Celebrity> repository;

        public CelebritiesController(IRepository<Celebrity> repo)
        {
            repository = repo;
        }
        public IEnumerable<Celebrity> Get()
        {
            return repository.GetAll().Where(c => c.isViewable);
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return Ok();

            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IHttpActionResult ResetList()
        {
            repository.Reset();
            return Ok();
        }
    }
}
