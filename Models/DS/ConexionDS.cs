using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DS
{
    public class ConexionDS
    {
        public int NoTienda { get; set; }

        public string Nombre { get; set; }
        public string IP { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public int Actualizacion { get; set; }
        public int Zona { get; set; }
        public int SQL2008 { get; set; }
    }
}
