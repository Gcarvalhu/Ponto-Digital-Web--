using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ponto_Digital_.Repositorio;

namespace Ponto_Digital_.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteRepositorio clienteRepository = new ClienteRepositorio();

        private const string SESSION_EMAIL = "_EMAIL";
        private const string SESSION_CLIENTE = "_CLIENTE";

        [HttpGet]
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection form){
            
            var usuario = form["email"];
            var senha = form["senha"];
            var cliente = clienteRepository.ObterPor(usuario);

            if (cliente != null && cliente.Senha.Equals(senha))
            {
                HttpContext.Session.SetString(SESSION_EMAIL, usuario);
                HttpContext.Session.SetString(SESSION_CLIENTE, cliente.Nome);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login", "Cliente");
        }

        public IActionResult Logout(){
            HttpContext.Session.Remove(SESSION_EMAIL);
            HttpContext.Session.Remove(SESSION_CLIENTE);
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}