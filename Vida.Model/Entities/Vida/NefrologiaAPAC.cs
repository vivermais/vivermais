using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class NefrologiaAPAC : ParteVariavelProcedimento
    {
        public NefrologiaAPAC() { }

        public static string co_SubGrupo = "0305";
        /// <summary>
        /// Data da primeira Diálise realizada
        /// </summary>
        private DateTime dataPrimeiraDialise;
        public DateTime DataPrimeiraDialise
        {
            get { return dataPrimeiraDialise; }
            set { dataPrimeiraDialise = value; }
        }

        /// <summary>
        /// Altura do paciente em Centímetros
        /// </summary>
        private int alturaPaciente;
        public int AlturaPaciente
        {
            get { return alturaPaciente; }
            set { alturaPaciente = value; }
        }

        /// <summary>
        /// Peso do paciente em Kg
        /// </summary>
        private int pesoPaciente;
        public int PesoPaciente
        {
            get { return pesoPaciente; }
            set { pesoPaciente = value; }
        }

        /// <summary>
        /// Diurese em ML
        /// </summary>
        private int diurese;
        public int Diurese
        {
            get { return diurese; }
            set { diurese = value; }
        }

        private char acessoVascular;
        /// <summary>
        /// S = Sim, N = Não
        /// </summary>
        public char AcessoVascular
        {
            get { return acessoVascular; }
            set { acessoVascular = value; }
        }


        private char ultraSonografiaAbdominal;
        /// <summary>
        /// S = Sim, N = Não
        /// </summary>
        public char UltraSonografiaAbdominal
        {
            get { return ultraSonografiaAbdominal; }
            set { ultraSonografiaAbdominal = value; }
        }

        private int glicose;
        /// <summary>
        /// Glicose (mg/dl)
        /// </summary>
        public int Glicose
        {
            get { return glicose; }
            set { glicose = value; }
        }

        private int tru;
        /// <summary>
        /// Taxa de redução da uréia (%) - Indicador da eficácia do tratamento dialítico 
        /// </summary>
        public int Tru
        {
            get { return tru; }
            set { tru = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int qtdIntervencaoFistola;
        public int QtdIntervencaoFistola
        {
            get { return qtdIntervencaoFistola; }
            set { qtdIntervencaoFistola = value; }
        }

        private char inscritoListaCNCDO;
        /// <summary>
        /// S = Sim, N = Não
        /// </summary>
        public char InscritoListaCNCDO
        {
            get { return inscritoListaCNCDO; }
            set { inscritoListaCNCDO = value; }
        }

        private int albumina;
        /// <summary>
        /// Albumina em g%
        /// </summary>
        public int Albumina
        {
            get { return albumina; }
            set { albumina = value; }
        }

        private char indicaPresencaAntiCorposDeHCV;
        /// <summary>
        /// Indicativo de Presença de Anti-Corpos de HCV (P = Positivo, N = Negativo)
        /// </summary>
        public char IndicaPresencaAntiCorposDeHCV
        {
            get { return indicaPresencaAntiCorposDeHCV; }
            set { indicaPresencaAntiCorposDeHCV = value; }
        }

        private char indicaHBsAg;
        /// <summary>
        /// Indicativo de HBsAG (P = Positivo, N = Negativo)
        /// </summary>
        public char IndicaHBsAg
        {
            get { return indicaHBsAg; }
            set { indicaHBsAg = value; }
        }

        private char indicaPresencaAntiCorposDeHIV;
        /// <summary>
        /// Indicativo de Presença de Anti-Corpos de HIV (P = Positivo, N = Negativo)
        /// </summary>
        public char IndicaPresencaAntiCorposDeHIV
        {
            get { return indicaPresencaAntiCorposDeHIV; }
            set { indicaPresencaAntiCorposDeHIV = value; }
        }

        private int hb;
        /// <summary>
        /// HB em g%
        /// </summary>
        public int Hb
        {
            get { return hb; }
            set { hb = value; }
        }
    }
}
