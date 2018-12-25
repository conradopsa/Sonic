using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Sonic
{
    public partial class Form1 : Form
    {
        /*
         Sonic Desktop - Release 1

         Código desenvolvido por:
         Supreme Developer (ConradoPSA)

         Imagens originais do Sonic The Hedgehog 1 e 2

         Obs: Este código não está muito amigável, prometo organizar melhor ele nas próximas versões
        */

        Image[] resource;
        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(10, 10);
            this.Size = new Size(92, 84);

            w = (int)(wMIN * Tamanho);
            h = (int)(hMIN * Tamanho);

            resource = new Image[2];
            //Para o lado direito
            resource[0] = Properties.Resources.Tails;
            //Para o lado Esquerdo
            resource[1] = ContrarioX(Properties.Resources.Tails);

            SetarMovimento(ref sonic, movimento.Parado_Direita);

            Th_Velocidade = new Thread(() => velocidade());
            Th_Velocidade.Start();

            Th_Desacelerar = new Thread(() => desacelerar());

            
        }

        /*
         IMAGEM PARA MATRIZ (IMPLEMENTAR NO FUTURO)
            
         private Image[,] gerarPersonagem(Image ImgInteira, int linha, int coluna)
        {
            //Tamanho de cada frame (largura / coluna) (altura / linha)
            Size Sz_Frame = new Size((int)Math.Ceiling(1.0f * ImgInteira.Width / coluna), 
                (int)Math.Ceiling(1.0f * ImgInteira.Height / linha));


            Bitmap[,] Frames = new Bitmap[linha, coluna];

            Point pAux = new Point(0, 0);

            for (int i=0; i<linha; i++)
            {
                for (int j=0; j<coluna; j++)
                {
                    pAux.X = Sz_Frame.Width * j;
                    pAux.Y = Sz_Frame.Height * i;
                    
                    Frames[i, j] = new Bitmap(Sz_Frame.Width, Sz_Frame.Height);

                    //Graphics g = Graphics.FromImage(Frames[i, j]);
                    //g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    //g.DrawLine(new Pen(Color.Red), new Point(0, 0), new Point(100, 0));

                    Bitmap bmpImage = new Bitmap(ImgInteira);
                    Frames[i, j] = bmpImage.Clone(new Rectangle(pAux, Sz_Frame), bmpImage.PixelFormat);

                    System.Runtime.GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
                    GC.Collect();
                    //g.DrawImage(Properties.Resources.Sonic, new Rectangle(new Point(0, 0), ImgInteira.Size));

                    //Frames[i, j].Save(@"C:\Users\Derp\Desktop\a\" + i.ToString() + "," + j.ToString() + ".png");
                }
            }


            for (int i = 0; i < linha; i++)
            {
                for (int j = 0; j < coluna; j++)
                {

                    Frames[i, j].Save(@"C:\Users\Derp\Desktop\a\" + i.ToString() + "," + j.ToString() + ".png");
                }
            }

            return Frames;
        }*/

        #region Física
        enum direcao { Horizontal, Vertical }

        enum sentido { Negativo, Positivo }

        Thread Th_Velocidade;
        Thread Th_Desacelerar;
        Thread Th_Movimentos;

        Image sonic;
        
        const int TEMPO_DESACELERAR = 70; //Inverso a Velocidade
        const int MAX_ACELERACAO = 20;
        const int TEMPO_VELOCIDADE = 10; //Inverso a Velocidade

        const int ALTURA_MAX_DESCIDA = 100;
        const int INCREMENTO_DESCIDA = 10; //Direto a Velocidade
        const int TEMPO_DESCIDA = 10;  //Inverso a Velocidade

        const int ALTURA_MAX_PULO = 150;
        const int INCREMENTO_PULO = 10; //Direto a Velocidade
        const int TEMPO_PULO = 10; //Inverso a Velocidade
        const int PULO_VOLTAR = 75;

        int aceleracao = 0;
  
        void velocidade()
        {
            while (true)
            {
                Thread.Sleep(TEMPO_VELOCIDADE);

                this.Invoke(new Action(() =>
                {
                    Point p = new Point(this.Location.X, this.Location.Y);

                    p.X = p.X + aceleracao;


                    this.Location = p;

                    Teleportar();
                }));
                    
            }    
        }

        void Teleportar()
        {
            

            Size Tela = Screen.PrimaryScreen.Bounds.Size;

            Point ThisP = new Point(this.Location.X, this.Location.Y);

            //Canto Direito
            if ((ThisP.X - this.Width) > (Tela.Width))
            {                
                ThisP.X = -(this.Width);

                this.Location = ThisP;
            }
            //Canto Esquerdo
            else if(ThisP.X < -(this.Width))
            {
                ThisP.X = Tela.Width + this.Width;

                this.Location = ThisP;
            }
            //Cima
            else if (ThisP.Y < -(this.Height))
            {
                ThisP.Y = Tela.Height + this.Height;

                this.Location = ThisP;
            }
            //Baixo
            else if ((ThisP.Y - this.Height) > Tela.Height)
            {
                ThisP.Y = - (this.Height);

                this.Location = ThisP;
            }
        }

        void desacelerar()
        {
            while (aceleracao != 0)
            {
                Thread.Sleep(TEMPO_DESACELERAR);

                if (aceleracao > 0)
                    aceleracao--;
                else if (aceleracao < 0)
                    aceleracao++;
            }
        }

        void acelerar(sentido s)
        {
            if ((s == sentido.Negativo) && (aceleracao >= -MAX_ACELERACAO))
            {
                aceleracao--;
            }
            else if ((s == sentido.Positivo) && (aceleracao <= MAX_ACELERACAO))
            {
                aceleracao++;
            }
            
            if (!Th_Desacelerar.IsAlive)
            {
                Th_Desacelerar = new Thread(() => desacelerar());
                Th_Desacelerar.Start();
            }
        }

        static bool pulando = false;
        void pular()
        {
            pulando = true;

            Point p = new Point(this.Location.X, this.Location.Y);

            //PULAR
            int i = 1;
            while (i <= ALTURA_MAX_PULO)
            {
                Thread.Sleep(TEMPO_PULO);
                p.Y -= INCREMENTO_PULO;


                this.Invoke(new Action(() => {
                    p = new Point(this.Location.X, p.Y);
                    this.Location = p;
                }));


                i += INCREMENTO_PULO;
            }

            //VOLTAR
            i = 1;
            while (i <= PULO_VOLTAR)
            {
                Thread.Sleep(TEMPO_PULO);
                p.Y += INCREMENTO_PULO;

                this.Invoke(new Action(() => {
                    p = new Point(this.Location.X, p.Y);
                    this.Location = p;
                }));

                i += INCREMENTO_PULO;
            }

            pulando = false;
        }

        bool descendo = false;
        private void descer()
        {
            descendo = true;

            Point p = new Point(this.Location.X, this.Location.Y);

            int i = 1;
            while (i <= ALTURA_MAX_DESCIDA)
            {
                Thread.Sleep(TEMPO_DESCIDA);
                p.Y += INCREMENTO_DESCIDA;

                this.Invoke(new Action(() => {
                    p = new Point(this.Location.X, p.Y);
                    this.Location = p;
                }));                

                //Incremento
                i += INCREMENTO_DESCIDA;
            }

            descendo = false;
        }


        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    //Física
                    acelerar(sentido.Positivo);

                    //Visual
                    SetarMovimento(ref sonic, movimento.Correr_Direita);

                    break;
                case Keys.A:

                    //Física
                    acelerar(sentido.Negativo);

                    //Visual
                    SetarMovimento(ref sonic, movimento.Correr_Esquerda);

                    break;
                case Keys.Space:

                    pulando = true;

                    //Visual
                    SetarMovimento(ref sonic, movimento.RolarPulo);

                    //Física
                    if (Th_Movimentos == null || !Th_Movimentos.IsAlive)
                    {
                        Th_Movimentos = new Thread(() => pular());
                        Th_Movimentos.Start();
                    }

                    

                    break;
                case Keys.S:
                    
                    descendo = true;

                    //Visual
                    SetarMovimento(ref sonic, movimento.RolarDesce);

                    //Física
                    if (Th_Movimentos == null || !Th_Movimentos.IsAlive)
                    {
                        Th_Movimentos = new Thread(() => descer());
                        Th_Movimentos.Start();
                    }

                    

                    break;
            }
        }

        enum movimento { Parado_Direita, Correr_Direita, Correr_Esquerda, RolarPulo, RolarDesce }

        int wMIN = 46, hMIN = 41;
        int w, h;
        float Tamanho = 2;
        float proporcao = 1.1219512195121951219512195121951f;

        Point P0 = new Point(0, 0);

        Thread Th_Visual;

        Bitmap resD, resE;
        void SetarMovimento(ref Image saida, movimento m)
        {            
            Bitmap resD = new Bitmap(resource[0]);
            Bitmap resE = new Bitmap(resource[1]);

            Point p = P0;

            Size size = new Size((int)(resD.Width * proporcao * Tamanho),
                    (int)(resD.Height * proporcao * Tamanho));

            Bitmap btmp = new Bitmap(size.Width, size.Height);

            Graphics g = Graphics.FromImage(btmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.None;

            Size resourceSize = new Size((int)(resD.Width * Tamanho), (int)(resD.Height * Tamanho));


            switch (m)
            {
                case movimento.Parado_Direita:
                    p = P0;
                    g.DrawImage(resD, new Rectangle(p, resourceSize));
                    saida = btmp;
                    this.Invalidate();

                    break;
                case movimento.RolarPulo:                    

                    if (Th_Visual == null || !Th_Visual.IsAlive)
                    {
                        Th_Visual = new Thread(() => {

                            p = new Point(0, -(h * 2));
                           
                            while (pulando)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    Thread.Sleep(100);
                                    g.Clear(Color.Transparent);
                                    p.X = -(w * i);
                                    //MessageBox.Show(p.X.ToString());
                                    g.DrawImage(resD, new Rectangle(p, resourceSize));
                                    sonic = btmp;

                                    this.Invoke(new Action(() => {
                                        this.Invalidate();
                                    }));

                                    if (!pulando)
                                        break;
                                }
                            }

                            g.Clear(Color.Transparent);
                            g.DrawImage(resD, new Rectangle(P0, resourceSize));
                            sonic = btmp;

                            this.Invoke(new Action(() => {
                                this.Invalidate();
                            }));
                        });
                        Th_Visual.Start();
                    }
                    break;
                case movimento.RolarDesce:
                    p = new Point(0, -(h * 2));

                    if (Th_Visual == null || !Th_Visual.IsAlive)
                    {
                        Th_Visual = new Thread(() =>
                        {

                            while (descendo)
                            {
                                for (int i = 1; i < 5; i++)
                                {
                                    Thread.Sleep(100);
                                    g.Clear(Color.Transparent);
                                    p.X = -(w * i);
                                    //MessageBox.Show(p.X.ToString());
                                    g.DrawImage(resD, new Rectangle(p, resourceSize));
                                    sonic = btmp;

                                    this.Invoke(new Action(() =>
                                    {
                                        this.Invalidate();
                                    }));

                                    if (!descendo)
                                        break;
                                }
                            }

                            g.Clear(Color.Transparent);
                            g.DrawImage(resD, new Rectangle(P0, resourceSize));
                            sonic = btmp;

                            this.Invoke(new Action(() =>
                            {
                                this.Invalidate();
                            }));

                        });
                        Th_Visual.Start();
                    }

                    break;
                case movimento.Correr_Direita:

                    //Se th NÃO é nulo E th está operando, então não faz nada
                    if (Th_Visual != null && Th_Visual.IsAlive)
                        return;

                    Th_Visual = new Thread(() => visualCorrer(ref g, ref btmp, resourceSize, resD, resE, true));

                    Th_Visual.Start();
                    

                    break;

                case movimento.Correr_Esquerda:

                    //Se th NÃO é nulo E th está operando, então não faz nada
                    if (Th_Visual != null && Th_Visual.IsAlive)
                        return;

                    Th_Visual = new Thread(() => visualCorrer(ref g, ref btmp, resourceSize, resD, resE, false));

                    Th_Visual.Start();


                    break;

                default:
                    p = P0;

                    break;
            }

            GC.Collect();
        }

        void visualCorrer(ref Graphics g, ref Bitmap btmp, Size resourceSize, Bitmap resD, Bitmap resE, bool direita)
        {

            Point p = P0;

            while (aceleracao != 0)
            {
                int i = 0;
                int val = Math.Abs(aceleracao);
                while (true)
                {
                    Image Parado;
                    Point ParadoP;

                    //Alternar movimentos
                    if (i == 0)
                        i = 1;
                    else
                        i = 0;

                    //Tempo dos frames
                    Thread.Sleep(200);

                    g.Clear(Color.Transparent);

                    //Desenha
                    if (direita == true)
                    {
                        Parado = resD;
                        ParadoP = P0;

                        if (aceleracao < 0)
                            return;

                        //Passa os frames
                        p.X = -(w * (val + i));

                        g.DrawImage(resD, new Rectangle(p, resourceSize));
                    }
                    else {
                        Parado = resE;
                        ParadoP = P0;
                        ParadoP.X = -(resourceSize.Width - w);

                        if (aceleracao > 0)
                            return;

                        //Passa os frames
                        p.X = -(resourceSize.Width - (w * (val + i + 1)));
                    
                        g.DrawImage(resE, new Rectangle(p, resourceSize));
                    }

                    //Atualiza
                    sonic = btmp;
                    this.Invoke(new Action(() =>
                    {
                        this.Invalidate();
                    }));

                    //g.DrawString(val.ToString(), new Font("Arial", 12.0f, FontStyle.Bold), new SolidBrush(Color.White), P0);

                    //Se a = 0 então exibirá a imagem P(0,0) (Sonic parado)
                    if (aceleracao == 0)
                    {
                        g.Clear(Color.Transparent);
                        g.DrawImage(Parado, new Rectangle(ParadoP, resourceSize));
                        sonic = btmp;
                        break;
                    }
                    else if (Math.Abs(aceleracao) >= 10) //Limite do frame (Apartir daquim o movimento ficará alternado )
                    {
                        val = 9;
                    }
                    else
                    {
                        val = Math.Abs(aceleracao);
                    }

                    //Coleta de lixo
                    GC.Collect();
                }
            }


            this.Invoke(new Action(() =>
            {
                this.Invalidate();
            }));

        }

        private Image ContrarioX(Image img)
        {
            Bitmap btm = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(btm);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            //Ponta esquerda superior da imagem vai pro meio
            g.TranslateTransform(img.Width / 2.0f, img.Height / 2.0f);

            //A partir desse ponto, a imagem rotaciona
            g.RotateTransform(180);

            //Volta a posição inicial
            g.TranslateTransform(-img.Width / 2.0f, -img.Height / 2.0f);

            Rectangle Rect = new Rectangle(P0, img.Size);
            g.DrawImage(img, Rect);

            //Inverte Y
            btm.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return btm;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            g.DrawImage(sonic, new Point(0, 0));
        }

    }
}
