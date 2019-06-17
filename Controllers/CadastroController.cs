using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ponto_Digital_.Models;
using Ponto_Digital_.Repositorio;

namespace Pontodigitaloficial.Controllers {
    public class CadastroController : Controller {

         public const string SESSION_EMAIL = "_EMAIL";
        public const string SESSION_SENHA = "_SENHA";
         public ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
        public IActionResult Index(){
            var email = HttpContext.Session.GetString(SESSION_EMAIL) == null ? "" : HttpContext.Session.GetString(SESSION_EMAIL);
            var cliente = clienteRepositorio.ObterPor(email);
            return View(cliente);
    
        }

        public IActionResult Cadastrar(IFormCollection form){

            ClienteModel cliente = new ClienteModel();
            cliente.Nome = form["nome"];
            cliente.Endereco = form["endereco"];
            cliente.Telefone = form["telefone"];
            cliente.Senha = form["senha"];
            cliente.Email = form["email"];
            cliente.DataNascimento = DateTime.Parse(form["data-nascimento"]);

            clienteRepositorio.Inserir(cliente);

            ViewData["Action"] = "Cadastro";

            return RedirectToAction("Index","Home");
        }
        public IActionResult Login(){
            return View();
        }
        public IActionResult FazerLogin (IFormCollection form){
            var lista = clienteRepositorio.Listar();
            var email = form["email"];
            var senha = form["senha"];

            var clienteRetornado = clienteRepositorio.ObterPor(email);

            if(clienteRetornado == null){
                System.Console.WriteLine("################ Usuario não encontrado");
                
            }
            else if(clienteRetornado != null && senha == clienteRetornado.Senha){
                HttpContext.Session.SetString (SESSION_EMAIL, clienteRetornado.Email);
                HttpContext.Session.SetString (SESSION_SENHA, clienteRetornado.Senha);
                System.Console.WriteLine("################ Fez login");
            }
            return RedirectToAction ("Index", "Home");
        }
    }
}