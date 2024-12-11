namespace ConverterLogAPI.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public int StatusCodigo { get; set; }
        public string MetodoHttp { get; set; }
        public string Uri { get; set; }
        public string Tempo { get; set; }
        public string CacheStatus { get; set; }
    }
}
