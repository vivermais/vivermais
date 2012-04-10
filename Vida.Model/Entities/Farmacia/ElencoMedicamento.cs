using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
   
    /// <summary>
    /// Essa entidade é a antiga Grupo. GrupoMedicamento, que era many-to-many
    /// foi excluida e foi criado um IList de GrupoMedicamento(essa) em Medicamento
    /// Excluir esse comentário depois das implementações
    /// 
    /// Modificado por Murilo em 26/11/09:
    /// Esta entidade, antes GrupoMedicamento foi renomada para ElencoMedicamento
    /// para atender a uma mudança de requisitos. O cliente passou a chamar grupo 
    /// de elenco logo renomeei a classe. Também retirei a lista que estava em medicamento
    /// porque esta parou de fazer sentido. O mapeamento agora em n-n
    /// tanto aqui quanto em subelenco
    /// ElencoMedicamenteo N-N SubElencoMedicamento
    /// ElencoMedicamento N-N Medicamento
    /// SubElencoMedicamento N-N Medicamento
    /// </summary>
    [Serializable]
    public class ElencoMedicamento
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

        IList<SubElencoMedicamento> subelencos;

        public virtual IList<SubElencoMedicamento> SubElencos 
        {
            get { return subelencos; }
            set { subelencos = value; }
        }

        IList<Medicamento> medicamentos;

        public virtual IList<Medicamento> Medicamentos
        {
            get { return medicamentos; }
            set { medicamentos = value; }
        }
        
        public ElencoMedicamento()
        {

        }

        public override string ToString()
        {
            return this.Nome;
        }

        public override bool Equals(object obj)
        {
            if (this.Codigo == ((ElencoMedicamento)obj).Codigo)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 73;
        }
    }
}
