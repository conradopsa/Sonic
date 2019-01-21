using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonic
{
    static class SupremeMath
    {
        public static float[] SistemaLinear2(float a, float b, float c,
                        float d, float e, float f)
        {
            //Retorno: [0] é X, [1] é Y

            //ax + by = c
            //dx + ey = f

            #region Explicação
            /*
            ax + by = c
            dx + ey = f

            Equação 1:
            ax + by = c
            ax = c - by
            x = (c - by) / a

            Equação 2:
            dx + ey = f
            ey = f - dx
            y = (f - dx) / e

            Susbstituindo y em x:

            x = (c - by) / a
            x = (c - b.( (f - dx) / e) ) / a     --Fatoração por evidência ali no b
            x = (c - (b.f)/e + (b.d.x)/e) / a       --Igualando os denominadores
            x = ((e.c) - (b.f) + (b.d.x)) / (e.a)       -- Passando o (b.d.x) / (e.a) pro outro lado
            x - (b.d.x) / (e.a) = (e.c - b.f) / (e.a)       --Igualando os denominadores
            (e.a.x - b.d.x) / (e.a) = (e.c - b.f) / (e.a)     --Multiplicando cada lado por (e.a)
            (e.a.x - b.d.x) = (e.c) - (b.f)      --(e.a).x - (b.d).x equivale a (e.a - b.d).x   Ex.: x-5x = (1-5)x
            (e.a - b.d)x = (e.c) - (b.f)        --Passando o (e.a - b.d) pro outro lado
            x = ((e.c) - (b.f)) / (e.a - b.d)

            Fica mais amigável no papel!

            Créditos a ConradoPSA (SupremeDev)
            
            */
            #endregion
            float x = (e * c - b * f) / (e * a - b * d);

            #region Explicação 
            /*
            a.x + b.y = c
            b.y = c - a.x
            y = (c - a.x) / b
            */
            #endregion
            float y = (c - a * x) / b;

            return new float[] { x, y };
        }


        public static double ConvertToGraus(double rad)
        {
            //PI rad ---> 180°
            //x rad ----> y°
            //PI.y = 180.x
            //y = 180.x / PI

            return 180 * rad / Math.PI;
        }

        public static double ConvertToRad(double graus)
        {
            //PI rad ---> 180°
            //x rad ----> y°
            //180.x = PI.y
            //x = PI.y / 180

            return Math.PI * graus / 180.0;
        }
    }
}
