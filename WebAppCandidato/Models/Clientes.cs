﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppCandidato.Models
{
    public class Clientes
    {
        public int idCliente { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string nombres { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string sexo { get; set; }
    }

  
}