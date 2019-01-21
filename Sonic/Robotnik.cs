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
    public class Robotnik : Personagem
    {

        Thread th_Sinalizador;
        public Robotnik(Control plano_, float ampliar_) : base(plano_, Properties.Resources.Robotnik, 1, 4, ampliar_, new FrameSettings(4, 40, 1), null)
        {
            this.cinematica = new RobotnikCinemática(plano_);

            //Só pra simplificar, não tendo que fazer casting toda hora
            robotnikCinemática = (RobotnikCinemática)this.cinematica;
            esfCinemática = robotnikCinemática.esfCinemática;

            InicializarImagens();

            //Sistema para correção da posição da esfera
            /*
            180.a + 1.b = widthArgola
            90.a + 1.b = 0
            */
            coefCorreçãoEsf = SupremeMath.SistemaLinear2(90, 1, 0, 180, 1, drawArgola.Width);

            th_Sinalizador = new Thread(() => sinalizador());
            th_Sinalizador.Start();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (th_Sinalizador != null)
                th_Sinalizador.Abort();
        }

        private void sinalizador()
        {
            int j = 0;
            while (true)
            {
                Thread.Sleep(200);
                drawSinalizador = framesSinalizador[0, j];

                if (j < 1)
                    j++;
                else
                    j=0;
            }
        }

        //Coeficientes para a correção da posição da esfera
        float[] coefCorreçãoEsf;        

        private void InicializarImagens()
        {
            //Inicializando as imagens
            drawArgola = ImageSupreme.MatrixImg(Properties.Resources.Argola, 1, 1, ampliar)[0, 0];

            framesSinalizador = ImageSupreme.MatrixImg(Properties.Resources.Sinalizador, 1, 2, ampliar);
            drawSinalizador = framesSinalizador[0, 0];

            framesEsfera = ImageSupreme.MatrixImg(Properties.Resources.Esfera, 1, 2, ampliar);
            drawEsfera = framesEsfera[0, 0];
        }

        Image drawArgola;

        Image[,] framesSinalizador;
        Image drawSinalizador;

        Image[,] framesEsfera;
        Image drawEsfera;

        public override void GraphDraw(Graphics g)
        {            

            //Size szPers = new Size(draw.Size.Width, draw.Size.Height);

            Point ptPers = new Point(plano.Width / 2 - draw.Size.Width / 2, 0);

            //Desenha o personagem
            g.DrawImage(draw, ptPers);

            //Variáveis do Sinalizador
            Point ptSin = new Point(ptPers.X,
                ptPers.Y + (int)(draw.Size.Height * (46.0f / 50.0f)));

            float razaoSinX = 48.0f / 84.0f;
            //X, quando direita = widthPers * razão
            //X, quando esquerda = widthPers * (1 - razão) - widthSin
            if (direita)
                ptSin.X += (int)(draw.Size.Width * razaoSinX);
            else
                ptSin.X += (int)(draw.Size.Width * (1 - razaoSinX) - drawSinalizador.Size.Width);
                        

            int raio = drawArgola.Height;
            double sin = Math.Sin(esfCinemática.rad);
            double cos = Math.Cos(esfCinemática.rad);

            //Desenha as Argolas
            Point ptArg = new Point(ptSin.X, ptSin.Y );
            for (int i = 1; i <= 4; i++)
            {
                ptArg.Y += (int)(raio * sin);
                ptArg.X += (int)(raio * cos);
                g.DrawImage(drawArgola, ptArg);                
            }

            //Desenha o Sinalizador
            g.DrawImage(drawSinalizador, ptSin);

            //Desenha o personagem
            g.DrawImage(draw, ptPers);

            raio = drawArgola.Height*5;

            //Esfera
            Point ptEsf = new Point(ptSin.X - (drawEsfera.Width / 2 - drawArgola.Width / 2), ptSin.Y);
            ptEsf.Y += (int)(raio * sin);
            ptEsf.X += (int)(raio * cos);

            Image esfera = new Bitmap(drawEsfera.Width, drawEsfera.Height);
            
            //Rotaciona a esfera
            using (Graphics gE = Graphics.FromImage(esfera))
            {
                gE.TranslateTransform(drawEsfera.Width / 2f, drawEsfera.Height / 2f);
                gE.RotateTransform((int)SupremeMath.ConvertToGraus(esfCinemática.rad));
                gE.TranslateTransform(-drawEsfera.Width / 2f, -drawEsfera.Height / 2f);
                
                gE.DrawImage(drawEsfera, new Point(0, 0));
            }


            //Corrige a posição da esfera
            float a = coefCorreçãoEsf[0], 
                b = coefCorreçãoEsf[1];

            float graus = (float)SupremeMath.ConvertToGraus(esfCinemática.rad);
            int res = (int)(a * graus + b);

            ptEsf.X -= res;
            if (direita)
                ptEsf.Y -= res;            
            else
                ptEsf.Y += res;
            
            

            //Desenha a Esfera
            g.DrawImage(esfera, ptEsf);

            
            /*g.DrawString((res).ToString(),
                plano.Font, new SolidBrush(Color.Black),
                new Point(0, 0));*/
        }

        public override void setarTamanho(float ampliar_)
        {
            base.setarTamanho(ampliar_);

            InicializarImagens();
        }
       
        RobotnikCinemática.EsferaCinemática esfCinemática;
        RobotnikCinemática robotnikCinemática;
        public override void Controle(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    controleCorrer(Cinemática.sentido.Positivo);

                    break;
                case Keys.A:
                    controleCorrer(Cinemática.sentido.Negativo);

                    break;
                case Keys.S:
                    controleVoar(Cinemática.sentido.Negativo);

                    break;
                case Keys.W:
                    controleVoar(Cinemática.sentido.Positivo);
                    
                    break;
            }
        }

        private void controleCorrer(Cinemática.sentido sentido)
        {
            //Física
            cinematica.acelerar(sentido);

            //Visual
            Animação(animação.Correr, fSCorrer.FrameLim, fSCorrer.FrameDelay, fSCorrer.AlternarFinal);
        }

        private void controleVoar(Cinemática.sentido sentido)
        {
            robotnikCinemática.voar(sentido);
        }

        public class RobotnikCinemática : Cinemática
        {
            public RobotnikCinemática(Control c) : base(c)
            {
                th_voar = new Thread(() => velocidadeVoo());
                th_voar.Start();

                esfCinemática = new EsferaCinemática(c, this);
            }

            public int DELAY_DESACELERAR_VOO = 40;
            public int DELAY_VELOCIDADE_VOO = 25;

            Thread th_desacelerarVoo;
            void desacelerar(sentido sentVoo)
            {
                while (aceleracaoVoo != 0)
                {
                    Thread.Sleep(DELAY_DESACELERAR_VOO);

                    if (aceleracaoVoo > 0)
                    {
                        if (sentVoo == sentido.Positivo)
                            aceleracaoVoo--;
                        else
                            aceleracaoVoo++;
                    }                        
                    else if (aceleracaoVoo < 0)
                    {
                        if (sentVoo == sentido.Negativo)
                            aceleracaoVoo++;
                        else
                            aceleracaoVoo--;
                    }
                        
                }
            }

            public int aceleracaoVoo;
            void velocidadeVoo()
            {
                while (true)
                {
                    Thread.Sleep(DELAY_VELOCIDADE_VOO);

                    plano.Invoke(new Action(() =>
                    {
                        Point p = new Point(plano.Location.X, plano.Location.Y);

                        p.Y = p.Y - aceleracaoVoo;

                        plano.Location = p;

                    }));

                }
            }
            Thread th_voar;
            public void voar(sentido sent)
            {
                if (sent == sentido.Positivo)
                    aceleracaoVoo++;
                else
                    aceleracaoVoo--;


                if (th_desacelerarVoo == null || !th_desacelerarVoo.IsAlive)
                {
                    th_desacelerarVoo = new Thread(() => desacelerar(sent));
                    th_desacelerarVoo.Start();
                }
            }

            public EsferaCinemática esfCinemática;
            public class EsferaCinemática
            {

                public double rad;
                RobotnikCinemática rC;

                Thread th_Movimento;
                public EsferaCinemática(Control c, RobotnikCinemática rC)
                {
                    th_Movimento = new Thread(() => movimento());
                    th_Movimento.Start();

                    this.rC = rC;
                }

                public void Dispose()
                {
                    if (th_Movimento != null)
                        th_Movimento.Abort();
                }

                public float a, b;
                private void movimento()
                {
                    //Movimento em relação ao ângulo

                    /*
                    0, retorna 90°
                    máx, retorna 180°
                    y = a.x + b
                    Montando o Sistema:                
                    a.0 + b = 90
                    a.Máx + b = 180                
                     */
                    while (true)
                    {
                        Thread.Sleep(50);

                        float[] sistema = SupremeMath.SistemaLinear2(0, 1, 90, rC.MAX_ACELERACAO + 1, 1, 180);
                        float a = sistema[0],
                              b = sistema[1];

                        //y = a.x + b  --y é em graus
                        //Converte graus para radiano
                        rad = SupremeMath.ConvertToRad(a * rC.aceleracao + b);
 
                    }

                }
            }

        }


    }
}
