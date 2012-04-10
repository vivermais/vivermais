using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class UnidadeMedidaVacina
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        string sigla;

        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        public override string ToString()
        {
            return this.Sigla;
        }
        
        public UnidadeMedidaVacina()
        {
        }

        public virtual string SiglaSemCaracterEspecial()
        {
            string texto = this.Sigla;

            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }

            return texto;
        }
    }
}
