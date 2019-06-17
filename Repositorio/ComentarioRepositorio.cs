using System;
using System.Collections.Generic;
using System.IO;
using Ponto_Digital_.Models;

namespace Ponto_Digital_.Repositorio
{
    public class ComentarioRepositorio
    {
         private const string PATH = "Database/Comentarios.csv";
        private List<ComentarioModel> comentarios = new List<ComentarioModel> ();
        private ClienteRepositorio clienteRepositorio = new ClienteRepositorio ();

        public ComentarioModel Inserir (ComentarioModel comentario) {
            if (!File.Exists (PATH)) {
                File.Create (PATH).Close ();
            }
            comentario.Id = (ulong) File.ReadAllLines (PATH).Length + 1;

            string dadosDoComentario = $"{comentario.Id};{comentario.NomeUsuario};{comentario.Mensagem};{comentario.DataEnvio}\n";
            File.AppendAllText (PATH, dadosDoComentario);
            return comentario;
        }

        public List<ComentarioModel> Listar () {
            var registros = File.ReadAllLines (PATH);
            foreach (var item in registros) {
                ComentarioModel comentario = new ComentarioModel ();
                var dados = item.Split (";");
                comentario.Id = ulong.Parse (dados[0]);
                comentario.NomeUsuario = dados[1];
                comentario.Mensagem = dados[2];
                comentario.DataEnvio = DateTime.Parse(dados[3]);
                comentarios.Add (comentario);
            }
            return comentarios;
        }
    }
}