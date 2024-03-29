﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViverMais.View.Urgencia
{
    public partial class FormTemporizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ViewState["tempo_atendimento"] = 0;
                OnTick_Temporizador(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// Chamada para atualização do horário de atendimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnTick_Temporizador(object sender, EventArgs e)
        {
            Session["tempo_atendimento"] = int.Parse(Session["tempo_atendimento"] != null ? Session["tempo_atendimento"].ToString() : "0") + 1;
            Label_Tempo.Text = RetornaTempo(int.Parse(Session["tempo_atendimento"].ToString()));
        }

        /// <summary>
        /// Retorna o tempo decorrido no atendimento do paciente
        /// </summary>
        /// <param name="tempo">tempo guardado em segundos</param>
        /// <returns>Tempo no formato HH:MM:SS</returns>
        private string RetornaTempo(int tempo)
        {
            int horas = tempo / 3600;
            int minutos = (tempo % 3600) / 60;
            int segundos = (tempo % 3600) % 60;

            return (horas >= 10 ? horas.ToString() : "0" + horas.ToString()) + ":" + (minutos >= 10 ? minutos.ToString() : "0" + minutos.ToString()) + ":" + (segundos >= 10 ? segundos.ToString() : "0" + segundos.ToString());
        }
    }
}
