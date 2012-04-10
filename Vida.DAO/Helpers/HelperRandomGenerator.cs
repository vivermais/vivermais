﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ViverMais.DAO.Helpers
{
    public static class HelperRandomGenerator
    {
        static Random gen = new Random(DateTime.Now.Millisecond);

        public static long Next
        {
            get { return gen.Next(); }
        }

        public static int NextIdentificador
        {
            get { return gen.Next(99999); }
        }

        /// <summary>
        /// Gera uma palavra com as letras do alfabeto
        /// </summary>
        /// <param name="numeroLetras">número de letras da palavra</param>
        /// <returns></returns>
        public static string GerarPalavra(int numeroLetras)
        {
            int TAMANHO_ALFABETO = 26;
            char[] palavra = new char[numeroLetras];

            if (numeroLetras <= TAMANHO_ALFABETO)
            {
                char[] alfabeto = new char[TAMANHO_ALFABETO];
                char letraSorteada = '\0';
                int contador = 0;

                for (char letra = 'A'; letra <= 'Z'; letra++)
                {
                    alfabeto[contador] = letra;
                    contador++;
                }

                Random randomizar = new Random(DateTime.Now.Millisecond);

                contador = 0;

                while (contador < numeroLetras)
                {
                    letraSorteada = alfabeto[randomizar.Next(alfabeto.Length)];

                    while (palavra[contador] == letraSorteada)
                        letraSorteada = alfabeto[randomizar.Next(alfabeto.Length)];

                    palavra[contador] = letraSorteada;
                    contador++;
                }
            }

            return new string(palavra);
        }
    }
}