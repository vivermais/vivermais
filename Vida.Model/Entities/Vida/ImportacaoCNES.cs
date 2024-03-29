﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ImportacaoCNES
    {
        public enum DescricaoStatus { Inicializada = 'I', Finalizada = 'F', Falha = 'E' };

        private long codigo;

        public virtual long Codigo
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

        private string arquivo;

        public virtual string Arquivo
        {
            get { return arquivo; }
            set { arquivo = value; }
        }

        private string tamanhoarquivo;

        public virtual string TamanhoArquivo
        {
            get { return tamanhoarquivo; }
            set { tamanhoarquivo = value; }
        }

        private DateTime horarioinicio;

        public virtual DateTime HorarioInicio
        {
            get { return horarioinicio; }
            set { horarioinicio = value; }
        }

        private DateTime ? horariofinal;

        public virtual DateTime ? HorarioFinal
        {
            get { return horariofinal; }
            set { horariofinal = value; }
        }

        private char status;

        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string StatusToString
        {
            get
            {
                if (Convert.ToChar(DescricaoStatus.Falha) == Status)
                    return "Falha";
                else
                    if (Convert.ToChar(DescricaoStatus.Inicializada) == Status)
                        return "Inicializada";

                return "Finalizada";
            }
        }

        public virtual string HorarioTerminoToString
        {
            get
            {
                if (HorarioFinal.HasValue)
                    return HorarioFinal.Value.ToString("dd/MM/yyyy HH:mm");

                return "";
            }
        }

        public virtual string NomeUsuario
        {
            get { return Usuario.Nome; }
        }

        private string erro;

        public virtual string Erro
        {
            get { return erro; }
            set { erro = value; }
        }

        public ImportacaoCNES()
        {
        }
    }
}
