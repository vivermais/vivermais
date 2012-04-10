﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class QuimioterapiaAPAC : OncologiaAPAC
    {
        public QuimioterapiaAPAC() { }
        public static string[] co_FormaOrganizacao = new string[7] { "030402", "030403", "030404", "030405", "030406", "030407", "030408" };
        /// <summary>
        /// Preencher com os nomes dos medicamentos que devem ser expressos por meio de siglas.
        /// Pode-se optar por denominar abreviadamente os esquemas terapêuticos.
        /// </summary>
        private string esquema;
        public string Esquema
        {
            get { return esquema; }
            set { esquema = value; }
        }

        private int totalMesesPlanejados;
        public int TotalMesesPlanejados
        {
            get { return totalMesesPlanejados; }
            set { totalMesesPlanejados = value; }
        }

        private int totalMesesAutorizados;
        public int TotalMesesAutorizados
        {
            get { return totalMesesAutorizados; }
            set { totalMesesAutorizados = value; }
        }
    }
}
