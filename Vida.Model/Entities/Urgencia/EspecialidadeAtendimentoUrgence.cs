﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    /// <summary>
    /// Esta especialidade é utilizada para direcionar um paciente atendido em determinada especialidade.
    /// Para cada PA deverá ser feito o cadastro de pelo menos uma especialidade e somente uma principal, quando for implantar
    /// o Urgence no estabelecimento, e vinculá-la com o código do senhador [Sistema de Gestão In9Midia].
    /// </summary>
    public class EspecialidadeAtendimentoUrgence
    {
        private CBO especialidade;
        public virtual CBO Especialidade
        {
            get { return especialidade; }
            set { especialidade = value; }
        }

        private EstabelecimentoSaude unidade;
        public virtual EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }

        private bool especialidadeprincipal;
        public virtual bool EspecialidadePrincipal
        {
            get { return especialidadeprincipal; }
            set { especialidadeprincipal = value; }
        }

        private string codigoespecialidadesenhador;
        public virtual string CodigoEspecialidadeSenhador
        {
            get { return codigoespecialidadesenhador; }
            set { codigoespecialidadesenhador = value; }
        }

        public virtual string NomeEspecialidade
        {
            get { return this.Especialidade.Nome; }
        }

        public virtual string CodigoEspecialidade
        {
            get { return this.Especialidade.Codigo; }
        }

        public EspecialidadeAtendimentoUrgence()
        {
        }

        public override bool Equals(object obj)
        {
            return this.Especialidade.Equals(obj) &&
                   this.Unidade.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 71;
        }
    }
}
