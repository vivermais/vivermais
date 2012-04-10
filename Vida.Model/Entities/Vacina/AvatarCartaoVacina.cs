using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class AvatarCartaoVacina
    {
        public static int ALEGRIA = 1;
        public static int SERENIDADE = 2;
        public static int MODERNO = 3;
        public static int FLORES = 4;
        public static int PADRAO = 5;
        public static int ESPORTE_CLUBE_BAHIA = 6;
        public static int ESPORTE_CLUBE_VITORIA = 7;
        public static int BRASIL = 8;

        public AvatarCartaoVacina()
        {
        }

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public AvatarCartaoVacina(int codigo)
        {
            this.Codigo = codigo;
        }

        private int cartaovacinaselecionado;

        public virtual int CartaoVacinaSelecionado
        {
            get { return cartaovacinaselecionado; }
            set { cartaovacinaselecionado = value; }
        }

        public virtual string RetornaImagemTopo()
        {
            string img = string.Empty;

            if (this.CartaoVacinaSelecionado == CartaoVacina.CRIANCA)
                img += "crianca.jpg";
            else if (this.CartaoVacinaSelecionado == CartaoVacina.ADOLESCENTE)
                img += "adolescente.jpg";
            else if (this.CartaoVacinaSelecionado == CartaoVacina.ADULTOIDOSO)
                img += "adulto-idoso.jpg";
            else if (this.CartaoVacinaSelecionado == CartaoVacina.HISTORICO)
                img += "historico.jpg";

            return this.Path() + img;
        }

        public virtual string RetornaImagemCabecalho()
        {
            return this.Path() + "dados.jpg";
        }

        private string Path()
        {
            string path = "~/Vacina/img/CartaoVacina/Temas/";

            if (this.Codigo == AvatarCartaoVacina.ALEGRIA)
                path += "Alegria";
            else if (this.Codigo == AvatarCartaoVacina.BRASIL)
                path += "Brasil";
            else if (this.Codigo == AvatarCartaoVacina.ESPORTE_CLUBE_BAHIA)
                path += "Bahia";
            else if (this.Codigo == AvatarCartaoVacina.ESPORTE_CLUBE_VITORIA)
                path += "Vitoria";
            else if (this.Codigo == AvatarCartaoVacina.FLORES)
                path += "Flores";
            else if (this.Codigo == AvatarCartaoVacina.MODERNO)
                path += "Moderno";
            else if (this.Codigo == AvatarCartaoVacina.PADRAO)
                path += "Padrao";
            else if (this.Codigo == AvatarCartaoVacina.SERENIDADE)
                path += "Serenidade";

            return path + "/";
        }

        public virtual string ImagemPrincipal()
        {
            return this.Path() + "imagem_principal.jpg";
        }

        public static IList<AvatarCartaoVacina> RetornaTodosAvatares()
        {
            IList<AvatarCartaoVacina> avatares = new List<AvatarCartaoVacina>();

            for (int i = 1; i < 9; i++)
                avatares.Add(new AvatarCartaoVacina(i));
            
            return avatares;
        }

        public virtual string ImagemThumb
        {
            get
            {
                return this.Path() + "thumb.png";
            }
        }
    }
}
