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
using iTextSharp.text;
using System.Collections.Generic;

namespace ViverMais.View.Helpers
{
    public static class HelperCheckerTimeOfExecution
    {
        private static DateTime pontoInicial;
        private static List<List<String>> pontosIntermediarios;
        private static DateTime pontoFinal;

        public static void DefinirPontoInicial()
        {
            pontosIntermediarios = new List<List<String>>();
            pontoInicial = DateTime.Now;
        }

        public static void DefinirPontosIntermediarios(string descricao)
        {
            List<string> tempo = new List<String>();
            tempo.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
            tempo.Add(descricao);
            pontosIntermediarios.Add(tempo);
        }

        public static void DefinirPontoFinal()
        {
            pontoFinal = DateTime.Now;
        }

        public static List<List<string>> ResgataTemposDeExecucoesIntermediarios()
        {
            List<List<string>> tempos = new List<List<string>>();
            for (int x = 0; x < pontosIntermediarios.Count; x++)
            {
                List<string> tempoDescricao = new List<string>();
                if (x == 0)
                {
                    TimeSpan tempo = DateTime.Parse(pontosIntermediarios[x][0]) - pontoInicial;
                    tempoDescricao.Add(tempo.TotalSeconds.ToString());
                    tempoDescricao.Add(pontosIntermediarios[x][1]);

                }
                else
                {
                    TimeSpan tempo = (DateTime.Parse(pontosIntermediarios[x][0]) - DateTime.Parse(pontosIntermediarios[x - 1][0]));
                    tempoDescricao.Add(tempo.TotalSeconds.ToString());
                    tempoDescricao.Add(pontosIntermediarios[x][1]);
                }
                tempos.Add(tempoDescricao);
            }
            return tempos;
        }

        public static string ResgataTempoDeExecucaoTotal()
        {
            TimeSpan timeSpan = (pontoFinal - pontoInicial);
            return timeSpan.TotalSeconds.ToString();
        }
    }
}