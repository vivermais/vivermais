using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ViverMais.Model
{
    [Serializable] 
    public class Documento:AModel
    {
        ControleDocumento controleDocumento;
        public virtual ControleDocumento ControleDocumento
        {
            get { return controleDocumento; }
            set { controleDocumento = value; }
        }

        OrgaoEmissor orgaoEmissor;

        public virtual OrgaoEmissor OrgaoEmissor
        {
            get { return orgaoEmissor; }
            set { orgaoEmissor = value; }
        }

        string nomeCartorio;

        public virtual string NomeCartorio
        {
            get { return nomeCartorio; }
            set { nomeCartorio = value; }
        }

        string numeroLivro;

        public virtual string NumeroLivro
        {
            get { return numeroLivro; }
            set { numeroLivro = value; }
        }

        string numeroFolha;

        public virtual string NumeroFolha
        {
            get { return numeroFolha; }
            set { numeroFolha = value; }
        }

        string numeroTermo;

        public virtual string NumeroTermo
        {
            get { return numeroTermo; }
            set { numeroTermo = value; }
        }

        DateTime? dataEmissao;

        public virtual DateTime? DataEmissao
        {
            get { return dataEmissao; }
            set { dataEmissao = value; }
        }

        DateTime? dataChegadaBrasil;

        public virtual DateTime? DataChegadaBrasil
        {
            get { return dataChegadaBrasil; }
            set { dataChegadaBrasil = value; }
        }

        string numeroPortaria;

        public virtual string NumeroPortaria
        {
            get { return numeroPortaria; }
            set { numeroPortaria = value; }
        }

        DateTime? dataNaturalizacao;

        public virtual DateTime? DataNaturalizacao
        {
            get { return dataNaturalizacao; }
            set { dataNaturalizacao = value; }
        }

        string numero;

        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        string serie;

        public virtual string Serie
        {
            get { return serie; }
            set { serie = value; }
        }

        string zonaEleitoral;

        public virtual string ZonaEleitoral
        {
            get { return zonaEleitoral; }
            set { zonaEleitoral = value; }
        }

        string secaoEleitoral;

        public virtual string SecaoEleitoral
        {
            get { return secaoEleitoral; }
            set { secaoEleitoral = value; }
        }

        string complemento;

        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        UF uf;

        public virtual UF UF
        {
            get { return uf; }
            set { uf = value; }
        }

        //Paciente paciente;

        //public virtual Paciente Paciente
        //{
        //    get { return paciente; }
        //    set { paciente = value; }
        //}

        public Documento()
        {

        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
