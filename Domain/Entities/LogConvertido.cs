using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LogConvertido
    {
        public int Id { get; set; }
        public string Provedor { get; set; }
        public string MetodoHttp { get; set; }
        public int StatusCodigo { get; set; }
        public string CaminhoUrl { get; set; }
        public string Tempo { get; set; }
        public int TamanhoResposta { get; set; }
        public string StatusCache { get; set; }
    }
}
