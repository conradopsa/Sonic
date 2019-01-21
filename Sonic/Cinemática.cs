using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic
{
    public class Cinemática
    {
        public Control plano;

        public Cinemática(Control _plano)
        {
            plano = _plano;

            Th_Velocidade = new Thread(() => velocidade());
            Th_Velocidade.Start();

            Th_Desacelerar = new Thread(() => desacelerar());
        }

        public virtual void Dispose()
        {
            if (Th_Velocidade != null)
                Th_Velocidade.Abort();

            if (Th_Desacelerar != null)
                Th_Desacelerar.Abort();

            if (Th_Movimentos != null)
                Th_Movimentos.Abort();
        }

        //enum direcao { Horizontal, Vertical }

        public enum sentido { Negativo, Positivo }

        Thread Th_Velocidade;
        Thread Th_Desacelerar;
        public Thread Th_Movimentos;

        public int DELAY_DESACELERAR = 70; //Direto a Velocidade
        public int MAX_ACELERACAO = 15;
        public int DELAY_VELOCIDADE = 10; //Inverso a Velocidade

        public int ALTURA_MAX_DESCIDA = 100;
        public int INCREMENTO_DESCIDA = 10; //Direto a Velocidade
        public int TEMPO_DESCIDA = 10;  //Inverso a Velocidade

        public int ALTURA_MAX_PULO = 150;
        public int INCREMENTO_PULO = 10; //Direto a Velocidade
        public int DELAY_PULO = 10; //Inverso a Velocidade
        public int PULO_VOLTAR = 75;

        public int aceleracao = 0;


        void velocidade()
        {
            while (true)
            {
                Thread.Sleep(DELAY_VELOCIDADE);

                plano.Invoke(new Action(() =>
                {
                    Point p = new Point(plano.Location.X, plano.Location.Y);

                    p.X = p.X + aceleracao;


                    plano.Location = p;

                    Teleportar();

                    
                }));

            }
        }

        void Teleportar()
        {
            Size Tela = Screen.PrimaryScreen.Bounds.Size;

            Point ThisP = new Point(plano.Location.X, plano.Location.Y);

            //Canto Direito
            if ((ThisP.X - plano.Width) > (Tela.Width))
            {
                ThisP.X = -(plano.Width);

                plano.Location = ThisP;
            }
            //Canto Esquerdo
            else if (ThisP.X < -(plano.Width))
            {
                ThisP.X = Tela.Width + plano.Width;

                plano.Location = ThisP;
            }
            //Cima
            else if (ThisP.Y < -(plano.Height))
            {
                ThisP.Y = Tela.Height + plano.Height;

                plano.Location = ThisP;
            }
            //Baixo
            else if ((ThisP.Y - plano.Height) > Tela.Height)
            {
                ThisP.Y = -(plano.Height);

                plano.Location = ThisP;
            }
        }

        void desacelerar()
        {
            while (aceleracao != 0)
            {
                Thread.Sleep(DELAY_DESACELERAR);

                if (aceleracao > 0)
                    aceleracao--;
                else if (aceleracao < 0)
                    aceleracao++;
            }
        }

        public void acelerar(sentido s)
        {
            //Mudar futuramente esse intervalo (<= para <), que está errado
            //A animação se comporta melhor desse jeito, mas está errado
            if ((Math.Abs(aceleracao) <= MAX_ACELERACAO))
            {
                if (s == sentido.Negativo)
                    aceleracao--;
                else
                    aceleracao++;
            }
            

            if (!Th_Desacelerar.IsAlive)
            {
                Th_Desacelerar = new Thread(() => desacelerar());
                Th_Desacelerar.Start();
            }
        }

        public bool pulando = false;
        public void pular()
        {
            if (Th_Movimentos != null && Th_Movimentos.IsAlive)
                return;

            pulando = true;
            Th_Movimentos = new Thread(() => {

                Point p = new Point(plano.Location.X, plano.Location.Y);

                //PULAR
                int i = 1;
                while (i <= ALTURA_MAX_PULO)
                {
                    Thread.Sleep(DELAY_PULO);
                    p.Y -= INCREMENTO_PULO;


                    plano.Invoke(new Action(() => {
                        p = new Point(plano.Location.X, p.Y);
                        plano.Location = p;
                    }));


                    i += INCREMENTO_PULO;
                }

                //VOLTAR
                i = 1;
                while (i <= PULO_VOLTAR)
                {
                    Thread.Sleep(DELAY_PULO);
                    p.Y += INCREMENTO_PULO;

                    plano.Invoke(new Action(() => {
                        p = new Point(plano.Location.X, p.Y);
                        plano.Location = p;
                    }));

                    i += INCREMENTO_PULO;
                }

                pulando = false;
            });

            Th_Movimentos.Start();
            
        }


        public bool descendo = false;
        public void descer()
        {
            if (Th_Movimentos != null && Th_Movimentos.IsAlive)
                return;

            descendo = true;
            Th_Movimentos = new Thread(() =>
            {
                

                Point p = new Point(plano.Location.X, plano.Location.Y);

                int i = 1;
                while (i <= ALTURA_MAX_DESCIDA)
                {
                    Thread.Sleep(TEMPO_DESCIDA);
                    p.Y += INCREMENTO_DESCIDA;

                    plano.Invoke(new Action(() =>
                    {
                        p = new Point(plano.Location.X, p.Y);
                        plano.Location = p;
                    }));

                    //Incremento
                    i += INCREMENTO_DESCIDA;
                }

                descendo = false;
            });
            Th_Movimentos.Start();
        }
        
    }
}
