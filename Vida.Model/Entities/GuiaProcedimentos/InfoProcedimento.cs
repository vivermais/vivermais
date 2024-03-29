﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class InfoProcedimento
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        string conceito;

        public virtual string Conceito
        {
            get { return conceito; }
            set { conceito = value; }
        }

        string aplicacao;

        public virtual string Aplicacao
        {
            get { return aplicacao; }
            set { aplicacao = value; }
        }

        string preparo;

        public virtual string Preparo
        {
            get { return preparo; }
            set { preparo = value; }
        }

        string dicas;

        public virtual string Dicas
        {
            get { return dicas; }
            set { dicas = value; }
        }

        string observacao;

        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        IList<EstabelecimentoSaude> unidadesExecutantes;

        public virtual IList<EstabelecimentoSaude> UnidadesExecutantes
        {
            get { return unidadesExecutantes; }
            set { unidadesExecutantes = value; }
        }

        public virtual string CodigoProcedimento
        {
            get
            {
                return this.Procedimento.Codigo;
            }
        }

        public virtual string NomeProcedimento
        {
            get
            {
                return this.Procedimento.Nome;
            }
        }

        public InfoProcedimento()
        {
            unidadesExecutantes = new List<EstabelecimentoSaude>();
        }
    }
}
