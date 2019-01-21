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
    class MetalSonic : Personagem
    {
        public MetalSonic(Control plano_, float ampliar = 1.0f) :
            base(plano_, Properties.Resources.MetalSonic, 5, 4, ampliar,
                new FrameSettings(4, 80), new FrameSettings(3, 80))
        {
            cinematica = new MetalSonicCinemática(plano_);

            metalSonicCinematica = (MetalSonicCinemática)cinematica;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (th_Voar != null)
                th_Voar.Abort();

            metalSonicCinematica.Dispose();
        }

        MetalSonicCinemática metalSonicCinematica;
        public override void Controle(KeyEventArgs e)
        {
            base.Controle(e);

            switch (e.KeyCode)
            {
                case Keys.W:
                    //Física
                    metalSonicCinematica.voar();

                    //Visual
                    MetalSonicAnimação(metalSonicAnima.voar, 80, 2);

                    break;
            }


        }

        #region Animação
        public enum metalSonicAnima { voar }
        public void MetalSonicAnimação(metalSonicAnima ação, int delay, int alternar = 2)
        {
            switch (ação)
            {
                case metalSonicAnima.voar:
                    if (th_Voar != null && th_Voar.IsAlive)
                        return;

                    th_Voar = new Thread(() => voar(alternar));
                    th_Voar.Start();

                    break;
            }
        }

        Thread th_Voar;

        void voar(int alternar)
        {
            while (metalSonicCinematica.aceleracaoVoo != 0)
            {
                int i = 1;
                for(int j=0; j<alternar; j++)
                {
                    if (direita)
                        draw = frames[i, j];
                    else
                        draw = ImageSupreme.ContrarioX(frames[i, j]);

                    if (metalSonicCinematica.aceleracaoVoo == 0)
                        break;
                }
            }

            Animação(animação.Correr, 4, 80, 1);
        }

        #endregion

        public class MetalSonicCinemática : Cinemática
        {
            public MetalSonicCinemática(Control c) : base(c)
            {
                th_desacelerarVoo = new Thread(() => desacelerar());
                th_desacelerarVoo.Start();

                th_voar = new Thread(() => velocidadeVoo());
                th_voar.Start();
            }

            public override void Dispose()
            {
                base.Dispose();

                if (th_desacelerarVoo != null)
                    th_desacelerarVoo.Abort();

                if (th_voar != null)
                    th_desacelerarVoo.Abort();
            }

            public int DELAY_DESACELERAR_VOO = 40;
            public int DELAY_VELOCIDADE_VOO = 25;

            Thread th_desacelerarVoo;
            void desacelerar()
            {
                while (aceleracaoVoo != 0)
                {
                    Thread.Sleep(DELAY_DESACELERAR_VOO);

                    if (aceleracaoVoo > 0)
                        aceleracaoVoo--;
                    else if (aceleracaoVoo < 0)
                        aceleracaoVoo++;
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
            public void voar()
            {

                aceleracaoVoo++;

                if (!th_desacelerarVoo.IsAlive)
                {
                    th_desacelerarVoo = new Thread(() => desacelerar());
                    th_desacelerarVoo.Start();
                }
            }
        }

    }
}
