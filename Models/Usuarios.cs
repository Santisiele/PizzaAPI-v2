using System;
using System.Collections.Generic;
namespace Usuarios.API.Models
{
    public class Usuarios
    {
        public int      Id                  { get; set; }
        public string   Nombre              { get; set; }
        public string   Apellido            { get; set; }
        public string   UserName            { get; set; }
        public string   Passwordd            { get; set; }
        public string   Token               { get; set; }
        public DateTime   TokenExpirationDay  { get; set; }
    }
}