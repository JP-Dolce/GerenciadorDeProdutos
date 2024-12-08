using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GerenciadorDePodutos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {

        private readonly Repositories.User _repoUser;
        public UserController()
        {
            _repoUser = new Repositories.User(Configurations.Database.getConnectionString());
        }
        // GET: api/User
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Models.Usuarios> user = _repoUser.SelectAll();
            if (user != null)
            {
                if (user.Count == 0)
                    return BadRequest("Usuário não encontrado!");
                return Ok(user);
            }
            else
                return InternalServerError();
        }


        [HttpPost]
        // POST: api/User
        public IHttpActionResult Post([FromBody] Models.Usuarios user)
        {
            if (!_repoUser.CreateUser(user))
                return InternalServerError();
            return Ok("User criado com sucesso!");
        }

    }
}
