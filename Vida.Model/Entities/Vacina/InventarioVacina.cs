using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class InventarioVacina
    {
        public enum DescricaoSituacao { Aberto = 'A', Fechado = 'F' };

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        DateTime datainventario;
        public virtual DateTime DataInventario
        {
            get { return datainventario; }
            set { datainventario = value; }
        }

        DateTime? datafechamento;
        public virtual DateTime? DataFechamento
        {
            get { return datafechamento; }
            set { datafechamento = value; }
        }

        char situacao;
        public virtual char Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        SalaVacina sala;
        public virtual SalaVacina Sala
        {
            get { return sala; }
            set { sala = value; }
        }

        public InventarioVacina()
        {
        }
    }
}
