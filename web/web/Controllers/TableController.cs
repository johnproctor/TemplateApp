using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Model.Repository;
using Model;

namespace Web.Controllers
{
    public class TableController : AsyncController
    {
        private readonly IGenericRepository _repo;

        public TableController(IGenericRepository repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult> Get(Int32 id)
        {
            return View(await _repo.GetByIdAsync<Table>(id));
        }

    }
}