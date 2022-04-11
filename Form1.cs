using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WFJogoMemoria
{
    public partial class Form1 : Form
    {
        int jogada, contador, fim, tagIndex;
        Image[] img = new Image[5];
        List<string> lista = new List<string>();
        int[] tagg = new int[2];




        public Form1()
        {
            InitializeComponent();
        }

        //Botão de jogada:
        private void btn_jogar_Click(object sender, EventArgs e)
        {
            // if para caso não seja a primeira jogada do jogador, zerar as variáveis e instâncias 
            if(fim != 0)
            {
                MessageBox.Show("Jogo encerrado, completou " + fim.ToString() + " de 10 cartas");
                contador = 0; jogada = 0; fim = 0;
                lista.Clear();
                lb_movimento.Text = "Movimentos:" + jogada.ToString();
            }
            //reposiciono as cartas
            Posicoes();
            //faço um foreach, que passará pelo menos uma vez em cada classe
            foreach (PictureBox pic in Controls.OfType<PictureBox>())
            {
                //pego a Tag da imagem e atribuo seu valor a tagIndex(conversão complexa por ser um objeto para string)
                int tagIndex = int.Parse(String.Format("{0}", pic.Tag));
                //joga o index como número do vetor e o seu valor é a foto da imagem 
                img[tagIndex] = pic.Image;
                //habilita para o jogador poder clicar na imagem
                pic.Enabled = true;
                //atualiza imagem por imagem para que todas terminem o processo e não ocorra um "lag"
                pic.Refresh();

            }
            //espera 2 segundos, para poder ver as cartas
            Thread.Sleep(2000);

            //faço um foreach, que passará pelo menos uma vez em cada classe
            foreach (PictureBox pic in Controls.OfType<PictureBox>())
            {
            //vira a imagem
                pic.Image = Properties.Resources.verso;
            }
        }


        private void Posicoes()
        {
            //faço um foreach, que passará pelo menos uma vez em cada classe
            foreach (PictureBox pic in Controls.OfType<PictureBox>())
            {
               // cria uma variável de aleatoriedade
                Random rdn = new Random();

                // pega os valores de x e y
                int[] xP = { 56, 186, 316, 446, 576 };
                int[] yP = { 141, 271 };

            Volta:
                // pega um valor aleatório de x e y e cria um a variável
                var X = xP[rdn.Next(0, xP.Length)];
                var Y = yP[rdn.Next(0, yP.Length)];

                //adiciona uma variável para a verificacao para não ocorrer duas imagens no mesmo lugar
                string verificacao = X.ToString() + Y.ToString();
                //verifica se dentro da lista criada já há uma string igual com as posições
                if (lista.Contains(verificacao))
                    // volta o código para poder pegar um novo valor de  x e y
                    goto Volta;

                else
                {
                    // se não houver define a nova posição da pictureBox
                    pic.Location = new Point(X, Y);
                    // e adiciona a variável para que não haja repetição
                    lista.Add(verificacao);
                }
            }
        }



        private void ImagensClick_Click(object sender, EventArgs e)
        {
            bool Par = false;
            PictureBox pic = (PictureBox)sender;
            // adiciona o valor de 1 clique de 2
            contador++;
            // pega o valor da tag
            tagIndex = int.Parse(String.Format("{0}", pic.Tag));
            // coloca a imagem correta de acordo com a tag 
            pic.Image = img[tagIndex];
            // atualiza a pictureBox
            pic.Refresh();

            // se for o primeiro clique...
            if (contador == 1)
                // adiciona no array o valor da tag do 1 item
                tagg[0] = int.Parse(String.Format("{0}", pic.Tag));
            //se for o segundo clique
            else if (contador == 2)
            {
                //adiciona jogada ( que vai aparecer em ''movimento'')
                jogada++;
                lb_movimento.Text = "Movimentos:" + jogada.ToString();
                //salva no array o valor da tag do 2 item
                tagg[1] = int.Parse(String.Format("{0}", pic.Tag));
                //compara o valor do 1 array com o valor do 2 array, se for true são iguais
                Par = ChecagemPares();
                //envia a verificação para o método desvirar
                Desvirar(Par);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private bool ChecagemPares()
        {
            contador = 0;
            if (tagg[0] == tagg[1]) { return true; } else { return false; }
        }



        private void Desvirar(bool check)
        {
            // espera meio segundo para virar de volta
            Thread.Sleep(500);
            //faço um foreach, que passará pelo menos uma vez em cada classe
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                //está passando por todas as variáveis, então se for a com as últimas tags que cliquei somente que serão
                //realizadas ações
                if (int.Parse(String.Format("{0}", item.Tag)) == tagg[0] || int.Parse(String.Format("{0}", item.Tag)) == tagg[1])
                {
                    //se o par for igual desabilita a opção de clicar novamente e adiciona um valor no contador de final de jogo
                    if (check == true)
                    { item.Enabled = false; fim++; }

                    //senão vira a imagem novamente
                    else
                    {
                        item.Image = Properties.Resources.verso;
                        item.Refresh();
                    }
                }
            }
        }
    }
}
