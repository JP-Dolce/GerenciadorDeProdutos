using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace GerenciadorDePodutos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly Repositories.User _repoUser;
            public LoginController()
        {
            _repoUser = new Repositories.User(Configurations.Database.getConnectionString());
        }
        [HttpPost]
        [Route("api/Login")]
        public IHttpActionResult Login([FromBody] Models.Usuarios usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Senha))
            {
                return BadRequest("Nome e senha são obrigatórios.");
            }

            Models.Usuarios userExistente = _repoUser.Select(usuario.Nome, usuario.Senha);
            if (userExistente != null)
            {
               
                var ticket = new FormsAuthenticationTicket(
                    1,
                    usuario.Nome,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    userExistente.Papel  
                );

                string encTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                {
                    Expires = ticket.Expiration
                };

                HttpContext.Current.Response.Cookies.Add(cookie);
                return Ok("Login bem-sucedido!");
            }
            return Unauthorized();
        }



    }
}
