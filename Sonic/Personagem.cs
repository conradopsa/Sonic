using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic
{
    public class FrameSettings
    {
        public FrameSettings(int _FrameLim, int _FrameDelay, int _AlternarFinal = 2)
        {
            FrameLim = _FrameLim;
            FrameDelay = _FrameDelay;
            AlternarFinal = _AlternarFinal;
        }

        public int FrameLim;
        public int FrameDelay;
        public int AlternarFinal;
    }

    public class Personagem
    {
        public float ampliar;
        public Control plano;
        Thread Th_Atualizar;
        public Personagem(Control plano_, Image resource, int qtdLinha, int qtdColuna, float ampliar_, FrameSettings fSetCorrer_, FrameSettings fSetRolar_)
        {
            plano = plano_;

            resourceImg = resource;
            qtdlinha = qtdLinha;
            qtdcoluna = qtdColuna;
            ampliar = ampliar_;

            frames = ImageSupreme.MatrixImg(resource, qtdLinha, qtdColuna, ampliar);
            linha = 0;
            coluna = 0;

            fSCorrer = fSetCorrer_;
            fSRolar = fSetRolar_;

            cinematica = new Cinemática(plano);

            Th_Atualizar = new Thread(() => atualizar());
            Th_Atualizar.Start();
        } 

        public virtual void Dispose()
        {
            //Descarte

            if (Th_Correr != null)
                Th_Correr.Abort();

            if (Th_Rolar != null)
                Th_Rolar.Abort();
            
            if (Th_Atualizar != null)
                Th_Atualizar.Abort();

            GC.Collect();
        }

        public bool congelar = false;

        private void atualizar()
        {
            while (true)
            {
                Thread.Sleep(50);
                if (congelar == false)
                    plano.Invalidate();
            }            
        }

        private int qtdlinha, qtdcoluna;
        public virtual void setarTamanho(float ampliar_)
        {
            ampliar = ampliar_;
            frames = ImageSupreme.MatrixImg(resourceImg, qtdlinha, qtdcoluna, ampliar);

            if (direita)
                draw = frames[0, 0];
            else
                draw = ImageSupreme.ContrarioX(frames[0, 0]);
        }

        public Cinemática cinematica;
        public Image resourceImg;
        public Image draw; //Frame Atual
        public Image[,] frames;


        public virtual void GraphDraw(Graphics g)
        {
            //Desenha o personagem
            g.DrawImage(draw, new Point(0, 0));
        }

        #region Controle

        public FrameSettings fSCorrer; //= new FrameSettings(11, 80);
        public FrameSettings fSRolar; //= new FrameSettings(5, 80);

        public virtual void Controle(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    //Física
                    cinematica.acelerar(Cinemática.sentido.Positivo);

                    //Visual
                    Animação(animação.Correr, fSCorrer.FrameLim, fSCorrer.FrameDelay, fSCorrer.AlternarFinal);

                    break;
                case Keys.A:
                    //Física
                    cinematica.acelerar(Cinemática.sentido.Negativo);

                    //Visual
                    Animação(animação.Correr, fSCorrer.FrameLim, fSCorrer.FrameDelay, fSCorrer.AlternarFinal);

                    break;
                case Keys.Space:
                    //Física
                    cinematica.pular();

                    //Visual
                    Animação(animação.Pular, fSRolar.FrameLim, fSRolar.FrameDelay);

                    break;
                case Keys.S:
                    //Física
                    cinematica.descer();

                    //Visual
                    Animação(animação.Descer, fSRolar.FrameLim, fSRolar.FrameDelay);

                    break;
            }
        }

        #endregion

        #region Animações
        private Thread Th_Correr;
        private Thread Th_Rolar;

        public bool direita = true;
        private void FrCorrer(int frameLIM, int alternar = 2)
        {
            //i = Linha, j = Coluna
            int i = 0;
            int lastFrame = frameLIM - 1;
            float razao = Math.Abs(lastFrame / (float)cinematica.MAX_ACELERACAO);
            

            int k = alternar;
            while (cinematica.aceleracao != 0)
            {
                Thread.Sleep(80);

                

                int j = (int)(Math.Abs(cinematica.aceleracao) * razao);
                int jj = j;

                //Alternar ultimos frames
                if (j == lastFrame)
                {
                    k--;
                    jj = j - k;
                    
                    if (k == 0)
                        k = alternar;
                }

                if (cinematica.aceleracao > 0)
                {
                    draw = frames[i, jj];
                    direita = true;
                }                    
                else if ((cinematica.aceleracao < 0))
                {
                    draw = ImageSupreme.ContrarioX(frames[i, jj]);
                    direita = false;
                }
                    
            }

            if (direita)
                draw = frames[0, 0];
            else
                draw = ImageSupreme.ContrarioX(frames[0, 0]);
        }

        public enum animação { Correr, Pular, Descer }
    
        public void Animação(animação ação, int frameLIM, int delay, int alternar = 2)
        {
            switch (ação)
            {
                case animação.Correr:
                    if (Th_Correr != null && Th_Correr.IsAlive)
                        return;

                    Th_Correr = new Thread(() => FrCorrer(frameLIM, alternar));
                    Th_Correr.Start();
                    
                    break;
                case animação.Pular:
                    if (Th_Rolar != null && Th_Rolar.IsAlive)
                        return;

                    if (Th_Correr != null)
                        Th_Correr.Abort();

                    Th_Rolar = new Thread(() => pular(frameLIM, delay));
                    Th_Rolar.Start();

                    break;
                case animação.Descer:
                    if (Th_Rolar != null && Th_Rolar.IsAlive)
                        return;

                    if (Th_Correr != null)
                        Th_Correr.Abort();

                    Th_Rolar = new Thread(() => descer(frameLIM, delay));
                    Th_Rolar.Start();

                    break;
            }
        }

        public void descer(int frameLIM, int delay)
        {

            //i = Linha, j = Coluna
            int i = 2;
            int lastFrame = frameLIM - 1;

            while (cinematica.descendo)
            {
                for (int j = 0; j < frameLIM; j++)
                {

                    this.draw = this.frames[i, j];

                    if (!cinematica.descendo)
                        break;
                    Thread.Sleep(delay);
                }
            }

            Thread.Sleep(100);
            //Volta a animação padrão           
            Animação(animação.Correr, frameLIM, delay);
        }

        public void pular(int frameLIM, int delay)
        {

            //i = Linha, j = Coluna
            int i = 2;
            int lastFrame = frameLIM - 1;

            while (cinematica.pulando)
            {
                for(int j=0; j<frameLIM; j++)
                {
                        
                    this.draw = this.frames[i, j];

                    if (!cinematica.pulando)                        
                        break;
                    Thread.Sleep(delay);
                }
            }

            //Volta a animação padrão           
            Animação(animação.Correr, frameLIM, delay);
        }

        #endregion

        #region Propriedades
        private int _linha;
        public int linha
        {
            get
            {
                return _linha;
            }

            set
            {
                _linha = value;
                draw = frames[value, coluna];
            }
        }

        private int _coluna;
        public int coluna
        {
            get
            {
                return _coluna;
            }

            set
            {
                _coluna = value;
                draw = frames[linha, value];
            }
        }
        #endregion

    }
}
