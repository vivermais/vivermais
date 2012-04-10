using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class UsuarioVacina
    {
        public UsuarioVacina()
        {
        }

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Usuario usuario;
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private bool responsavel;
        public virtual bool Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        private SalaVacina sala;
        public virtual SalaVacina Sala
        {
            get { return sala; }
            set { sala = value; }
        }

        public virtual int CodigoUsuario
        {
            get { return Usuario.Codigo; }
        }

        public virtual string NomeUsuario 
        {
            get { return this.Usuario.Nome; }
        }

        public virtual string CartaoSUSUsuario 
        {
            get { return this.Usuario.CartaoSUS; }
        }

        public virtual DateTime DataNascimentoUsuario 
        {
            get { return this.Usuario.DataNascimento; }
        }
    }
}
