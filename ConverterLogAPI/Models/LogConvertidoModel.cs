namespace ConverterLogAPI.Models
{
    public class LogConvertidoModel
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
