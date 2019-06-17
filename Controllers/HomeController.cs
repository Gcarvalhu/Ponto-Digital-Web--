using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ponto_Digital_.Models;
using Ponto_Digital_.Repositorio;

namespace Pontodigitaloficial.Controllers
{
    public class HomeController : Controller
    {
        private const string SESSION_EMAIL = "_EMAIL";
        private const string SESSION_CLIENTE = "_CLIENTE";
        private ClienteRepositorio clienteRepositorio =  new ClienteRepositorio();
        private ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio();
        public IActionResult Index(){
            RecuperarUserLogado();
            return View();
        }
        public IActionResult Comentarios(){
            ViewData["avaliacoes"] = comentarioRepositorio.Listar();
            RecuperarUserLogado();
            return View();
        }
        [HttpPost]
        public IActionResult RetornarComentario (IFormCollection form){
            var comentarioRepositorio = new ComentarioRepositorio();
            var comentario = new ComentarioModel();

            comentario.NomeUsuario = form["nome"];
            comentario.Mensagem = form["comentario"];
            comentario.DataEnvio = DateTime.Now;
            comentarioRepositorio.Inserir(comentario);

            return RedirectToAction("Index", "Home");
        }
        void RecuperarUserLogado(){
            var email = HttpContext.Session.GetString(SESSION_EMAIL) == null ? "" : HttpContext.Session.GetString(SESSION_EMAIL);
            var cliente = clienteRepositorio.ObterPor(email);
            ViewData["Usuario"] = cliente;

        }
    }
}