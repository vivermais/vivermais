using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.Model
{
    public class HorarioPerfil
    {
        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Perfil perfil;

        public virtual Perfil Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }
        private string horainicial;

        public virtual string HoraInicial
        {
            get { return horainicial; }
            set { horainicial = value; }
        }
        private string horafinal;

        public virtual string HoraFinal
        {
            get { return horafinal; }
            set { horafinal = value; }
        }

        private int dia;
        public virtual int Dia
        {
            get { return dia; }
            set { dia = value; }
        }

        private bool bloqueado;
        public virtual bool Bloqueado
        {
            get { return bloqueado; }
            set { bloqueado = value; }
        }

        public virtual string DiaEquivalente
        {
            get
            {
                string equivalente = string.Empty;

                switch ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia.ToString()))
                {
                    case DayOfWeek.Monday:
                        equivalente = "Segunda-Feira";
                        break;
                    case DayOfWeek.Tuesday:
                        equivalente = "Terça-Feira";
                        break;
                    case DayOfWeek.Wednesday:
                        equivalente = "Quarta-Feira";
                        break;
                    case DayOfWeek.Thursday:
                        equivalente = "Quinta-Feira";
                        break;
                    case DayOfWeek.Friday:
                        equivalente = "Sexta-Feira";
                        break;
                    case DayOfWeek.Saturday:
                        equivalente = "Sábado";
                        break;
                    case DayOfWeek.Sunday:
                        equivalente = "Domingo";
                        break;
                    default:
                        equivalente = "Dia Não Encontrado";
                        break;
                }

                return equivalente;
            }
        }

        public virtual bool PeriodoValido
        {
            get
            {
                int horarioatual = int.Parse(DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00"));

                if (horarioatual < int.Parse(this.HoraInicial) || horarioatual > int.Parse(this.HoraFinal))
                    return false;

                return true;
            }
        }

        public HorarioPerfil()
        {
        }
    }
}