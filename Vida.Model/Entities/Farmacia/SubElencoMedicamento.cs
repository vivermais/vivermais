﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    /// <summary>
    /// Essa entidade é a antiga SubGrupo. GrupoMedicamento, que era many-to-many
    /// foi excluida e foi criado um IList de SubGrupoMedicamento(essa) em Medicamento
    /// Excluir esse comentário depois das implementações
    /// 
    /// </summary>
     [Serializable]
    public class SubElencoMedicamento
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

        IList<Medicamento> medicamentos;

        public virtual IList<Medicamento> Medicamentos
        {
            get { return medicamentos; }
            set { medicamentos = value; }
        }
        
        public SubElencoMedicamento()
        {

        }

        public override string ToString()
        {
            return this.Nome;
        }

        public override bool Equals(object obj)
        {
            if (this.Codigo == ((ViverMais.Model.SubElencoMedicamento)obj).Codigo)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 37;
        }
    }
}
