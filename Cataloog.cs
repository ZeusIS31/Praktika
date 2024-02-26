using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAKTIKA
{
    public class Cataloog
    {
        public int Id { get; set; }
        public string Nazvanie { get; set; }
        public string Opisanie { get; set; }
        public string Kartinka { get; set; }
        public int price { get; set; }
    }
}
