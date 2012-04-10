﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{

    #region Classe Parte Variavel Procedimento

    public class ParteVariavelProcedimento
    {
        public static String RetornaCidFormatado(Cid cid)
        {
            string co_Cid = cid.Codigo;
            while (co_Cid.Length < 4)
                co_Cid += " ";
            return co_Cid;
        }

        public static String RetornaZeros(int qtdPosicoes)
        {
            string texto = string.Empty;
            while (texto.Length < qtdPosicoes)
                texto += "0";
            return texto;
        }

        public static String RetornaEspacoVazio(int qtdPosicoes)
        {
            string texto = string.Empty;
            while (texto.Length < qtdPosicoes)
                texto += " ";
            return texto;
        }

        public static String RetornaDataFormadata(DateTime data)
        {
            return data.ToString("yyyyMMdd");
        }

        public ParteVariavelProcedimento() { }

        public ParteVariavelProcedimento(char identificador, Cid cidPrincipal, Cid cidSecundario)
        {
            this.Identificador = identificador;
            this.CidPrincipal = cidPrincipal;
            this.CidSecundario = cidSecundario;
        }
        
        private char identificador;
        /// <summary>
        /// R (RADIOTERAPIA), Q (Quimioterapia), N (Nefrologia), 3 (Medicamentos), B (Bariatrica)
        /// </summary>
        public char Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }
        
        private Cid cidPrincipal;
        /// <summary>
        /// Cid principal do Procedimento
        /// </summary>
        public Cid CidPrincipal
        {
            get { return cidPrincipal; }
            set { cidPrincipal = value; }
        }
        
        private Cid cidSecundario;
        /// <summary>
        /// Cid Secundário do Procedimento
        /// </summary>
        public Cid CidSecundario
        {
            get { return cidSecundario; }
            set { cidSecundario = value; }
        }
    }
    #endregion
}