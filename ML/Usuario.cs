using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public  class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Pais Pais { get; set; }
        public ML.Estado Estado { get; set; }
        public ML.Direccion Direccion { get; set; }
        public ML.Municipio Municipio { get; set; }
        public ML.Colonia Colonia { get; set; }

    }
}
