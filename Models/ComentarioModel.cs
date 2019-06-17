using System;

namespace Ponto_Digital_.Models
{
    public class ComentarioModel
    {
        public ulong Id {get;set;}
        public string NomeUsuario {get;set;}

        public string Mensagem {get;set;}
        public DateTime DataEnvio {get;set;}
    }
}