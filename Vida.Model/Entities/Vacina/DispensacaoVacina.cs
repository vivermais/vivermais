﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class DispensacaoVacina
    {
        ////Retirar este campo depois
        //string codigopaciente;
        //public virtual string CodigoPaciente
        //{
        //    get { return codigopaciente; }
        //    set { codigopaciente = value; }
        //}
        ////Fim da retirada

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Paciente paciente;

        public virtual Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }
        
        DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        Usuario usuario;

        /// <summary>
        /// Entidade responsável por guardar o usuário responsável pela dispensação
        /// </summary>
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private Estrategia estrategia;

        public virtual Estrategia Estrategia
        {
            get { return estrategia; }
            set { estrategia = value; }
        }

        SalaVacina sala;

        public virtual SalaVacina Sala
        {
            get { return sala; }
            set { sala = value; }
        }

        private GrupoAtendimento grupoAtendimento;

        public virtual GrupoAtendimento GrupoAtendimento
        {
            get { return grupoAtendimento; }
            set { grupoAtendimento = value; }
        }

        public virtual string NomeSala
        {
            get { return this.Sala.Nome; }
        }

        public DispensacaoVacina()
        {
        }

        public static IList<DispensacaoVacina> Filtrar(IList<DispensacaoVacina> dispensacoes, DateTime data)
        {
            return (from _dispensacao in dispensacoes
                    where
                        _dispensacao.Data.ToString("dd/MM/yyyy") == data.ToString("dd/MM/yyyy")
                    select _dispensacao).ToList();
        }

        public static IList<DispensacaoVacina> ExcluirDispensacao(IList<DispensacaoVacina> dispensacoes, int co_dispensacao)
        {
            if (dispensacoes.Count() > 0)
            {
                var indextotal = dispensacoes.Select((Item, index) => new { index, Item }).Where(p => p.Item.Codigo == co_dispensacao).First();
                dispensacoes.RemoveAt(indextotal.index);
            }

            return dispensacoes;
        }
    }
}
