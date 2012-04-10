using System;
using System.IO.Compression;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Threading;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using Vida.DAO;

namespace Vida.View.Urgencia
{
    public static class HelperView
    {
        public static void ExecutarPlanoContingencia(int co_usuario, long co_prontuario)
        {
            try
            {
                StartBackgroundThread(delegate { PlanoContingencia(co_usuario, co_prontuario); });
            }
            catch { }
        }

        private static void PlanoContingencia(int co_usuario, long co_prontuario)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();

            try
            {
                iProntuario.ExecutarPlanoContingencia(co_usuario, co_prontuario);
            }
            catch { }
        }

        /// <summary>
        /// Função que executa um procedimento em background
        /// </summary>
        /// <param name="threadStart"></param>
        private static void StartBackgroundThread(ThreadStart threadStart)
        {
            if (threadStart != null)
            {
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();
            }
        }
    }
}