﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ControlePaciente:AModel
    {
        string codigo;
        
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        char controle;

        /// <summary>
        /// Indica o tipo de Operação Realizada: I ou Vazio - Inclusão; A = alteração; X = exclusão; E = com erro (usado pelo CadSUS Federal).
        /// </summary>
        public virtual char Controle
        {
            get { return controle; }
            set { controle = value; }
        }

        char excluido;

        /// <summary>
        /// Indica de o Usuário está excluído logicamente. 0 = ativo; 1 = excluído (ST_CONTROLE = X).
        /// </summary>
        public virtual char Excluido
        {
            get { return excluido; }
            set { excluido = value; }
        }

        DateTime dataOperacao;

        public virtual DateTime DataOperacao
        {
            get { return dataOperacao; }
            set { dataOperacao = value; }
        }

        int numeroVersao;

        public virtual int NumeroVersao
        {
            get { return numeroVersao; }
            set { numeroVersao = value; }
        }

        public ControlePaciente()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty & this.codigo != null;
        }
    }
}