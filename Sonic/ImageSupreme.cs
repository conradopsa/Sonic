using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Sonic
{
    public static class ImageSupreme
    {
        private static Point p0 = new Point(0, 0);
        public static Image[,] MatrixImg(Image resourceImg, int linha, int coluna, float ampliar = 1.0f)
        {

            #region Deixando a imagem do Resource no tamanho desejado


            Size szRes = new Size((int)(resourceImg.Width * ampliar),
                (int)(resourceImg.Height * ampliar));

            //Encontrando uma Altura próxima divisível por n linhas
            //Encontrando uma Largura próxima divisível por n Colunas
            while (!(szRes.Height % linha == 0 && szRes.Width % coluna == 0))
            {
                szRes.Height++;
                szRes.Width++;
            }

            Bitmap resourceBmpAux = new Bitmap(szRes.Width, szRes.Height);


            using (Graphics g = Graphics.FromImage(resourceBmpAux))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                g.DrawImage(resourceImg, 0, 0, resourceBmpAux.Width, resourceBmpAux.Height);
            }

            resourceImg = resourceBmpAux;

            #endregion

            //Tamanho de cada frame (largura / coluna), (altura / linha)
            Size SzFrame = new Size((int)((float)resourceImg.Width / coluna),
                (int)((float)resourceImg.Height / linha));

            Image[,] Frames = new Image[linha, coluna];

            Point pAux = new Point(0, 0);

            //Bitmap bmpImage = new Bitmap(ImgInteira);
            for (int i = 0; i < linha; i++)
            {
                for (int j = 0; j < coluna; j++)
                {
                    pAux.X = SzFrame.Width * j;
                    pAux.Y = SzFrame.Height * i;
                    
                    Bitmap Personagem = new Bitmap(SzFrame.Width, SzFrame.Height);

                    //Recorte
                    using (Graphics g = Graphics.FromImage(Personagem))
                    {
                        GraphicsUnit gU = GraphicsUnit.Pixel;
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;

                        g.DrawImage(resourceImg,
                            new RectangleF(new Point(0, 0), resourceImg.Size), new RectangleF(pAux, resourceImg.Size), gU);
                    }

                    Frames[i, j] = Personagem;
                    //Personagem.Save(@"C:\Users\Derp\Desktop\a\" + i.ToString() + ", " + j.ToString() + ".png", ImageFormat.Png);
                }
            }

            return Frames;
        }

        public static Image ContrarioX(Image img)
        {
            Bitmap btm = new Bitmap(1,1);
            try
            {
                btm = new Bitmap(img.Width, img.Height);

                Graphics g = Graphics.FromImage(btm);
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                //Ponta esquerda superior da imagem vai pro meio
                g.TranslateTransform(img.Width / 2.0f, img.Height / 2.0f);

                //A partir desse ponto, a imagem rotaciona
                g.RotateTransform(180);

                //Volta a posição inicial
                g.TranslateTransform(-img.Width / 2.0f, -img.Height / 2.0f);

                Rectangle Rect = new Rectangle(p0, img.Size);
                g.DrawImage(img, Rect);

                //Inverte Y
                btm.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            catch (InvalidOperationException)
            {
                
            }

            return btm;
        }
    }
}
