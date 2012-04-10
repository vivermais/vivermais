using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class DeficienciaPaciente: AModel
    {
        //private string codigopaciente;
        ///// <summary>
        ///// Codigo do paciente
        ///// </summary>
        //public string CodigoPaciente
        //{
        //    get { return codigopaciente; }
        //    set { codigopaciente = value; }
        //}

        public DeficienciaPaciente()
        {
            this.Deficiente = false;
            //this.codigo = string.Empty;
        }

        ///// <summary>
        ///// Gera um nova deficiência a partir do código do paciente
        ///// </summary>
        ///// <param name="codigo">código do paciente</param>
        //public DeficienciaPaciente(string CodigoPaciente)
        //{
        //    //this.CodigoPaciente = codigopaciente;
        //    this.Deficiente = false;
        //}

        private bool deficiente;
        public bool Deficiente
        {
            get { return deficiente; }
            set
            {
                deficiente = value;

                if (!this.deficiente)
                {
                    this.usaortese = false;
                    this.deficiencias = new List<Deficiencia>();
                    this.origens = new List<OrigemDeficiencia>();
                    this.proteses = new List<ProteseDeficiencia>();
                    this.locomocoes = new List<LocomocaoDeficiencia>();
                    this.comunicacoes = new List<ComunicacaoDeficiencia>();
                }
            }
        }

        private bool usaortese;
        public bool UsaOrtese
        {
            get { return usaortese; }
            set { usaortese = value; }
        }

        private List<Deficiencia> deficiencias;
        public List<Deficiencia> Deficiencias
        {
            get { return deficiencias; }
            set { deficiencias = value; }
        }

        private List<OrigemDeficiencia> origens;
        public List<OrigemDeficiencia> Origens
        {
            get { return origens; }
            set { origens = value; }
        }

        private List<ProteseDeficiencia> proteses;
        public List<ProteseDeficiencia> Proteses
        {
            get { return proteses; }
            set { proteses = value; }
        }

        private List<LocomocaoDeficiencia> locomocoes;
        public List<LocomocaoDeficiencia> Locomocoes
        {
            get { return locomocoes; }
            set { locomocoes = value; }
        }

        private List<ComunicacaoDeficiencia> comunicacoes;
        public List<ComunicacaoDeficiencia> Comunicacoes
        {
            get { return comunicacoes; }
            set { comunicacoes = value; }
        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
