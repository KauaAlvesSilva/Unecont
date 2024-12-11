using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string FormatoOriginal { get; set; }
        public string FormatoTransformado { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
