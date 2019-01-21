using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;
using Supreme_Components.Controls;

namespace Sonic
{
    public partial class Form1 : Form
    {
        /*
         Sonic Desktop - Release 2

         Código desenvolvido por:
         Supreme Developer (ConradoPSA)

         Imagens originais do Sonic The Hedgehog 1 e 2

         Obs: A organização desse código mudou bastante em relação à release 1
        */

        Personagem pers;
        float Tamanho = 1.75f;

        public Form1()
        {
            InitializeComponent();


            ajustarTamanhoForm(46, 41);

            //Personagem de início
            pers = new Personagem(this, Properties.Resources.Sonic, 4, 11, Tamanho, 
                new FrameSettings(11, 80), new FrameSettings(5, 80));            

        }
        
        //Tamanhos Referenciais de um personagem
        Size perRef;
        private void ajustarTamanhoForm(int setPerRefWidth = 0, int setPerRefHeight = 0)
        {
            if (setPerRefWidth != 0 && setPerRefHeight != 0)
                perRef = new Size(setPerRefWidth, setPerRefHeight);            

            Size newSz = new Size((int)(perRef.Width * Tamanho), (int)(perRef.Height * Tamanho));

            this.MinimumSize = newSz;
            this.Size = newSz;            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Comandos do Personagem
            pers.Controle(e);
        }       

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            try
            {
                //Desenha o Personagem
                pers.GraphDraw(g);
            }
            catch (InvalidOperationException)
            {

            }
            
        }

        Size tempThisSize;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Parar de atualizar animação
                pers.congelar = true;

                contextMenuSupreme1.Location = e.Location;

                int SomaWidth = contextMenuSupreme1.Location.X + contextMenuSupreme1.Width +
                    contextMenuPersonagem.Width + 1;
                int SomaHeight = contextMenuSupreme1.Location.Y + contextMenuSupreme1.Height +
                    contextMenuPersonagem.Height + 1;

                tempThisSize = this.Size;

                if (SomaWidth > this.Width)                
                    this.Width = SomaWidth;
                
                if (SomaHeight > this.Height)
                    this.Height = SomaHeight;
                

                //Exibe o Menu               
                contextMenuSupreme1.Show();
            }
            
        }

        private void contextMenuSupreme1_VisibleChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Visible == false)
            {
                this.Size = tempThisSize;

                //Voltar a atualizar a animação
                if (pers != null)
                    pers.congelar = false;
            }
                
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            this.Size = this.MinimumSize;
        }

        private void contextMenuItemSupreme2_MouseDown(object sender, MouseEventArgs e)
        {
            //Setar Tamanho

            SetarValor sV = new SetarValor();
            sV.numericUpDown1.Value = Convert.ToDecimal(Tamanho);
            
            if (sV.ShowDialog() == DialogResult.OK)
            {
                Tamanho = (float)sV.numericUpDown1.Value;
                pers.setarTamanho(Tamanho);
                ajustarTamanhoForm();
            }
        }

        #region Escolher Personagem
        private void itemSonic_Click(object sender, EventArgs e)
        {
            ajustarTamanhoForm(46, 41);

            pers.Dispose();
            pers = new Personagem(this, Properties.Resources.Sonic, 4, 11, Tamanho, new FrameSettings(11, 80), new FrameSettings(5, 80));
        }

        private void itemTails_Click(object sender, EventArgs e)
        {
            ajustarTamanhoForm(46, 41);

            pers.Dispose();
            pers = new Personagem(this, Properties.Resources.Tails, 4, 11, Tamanho, new FrameSettings(11, 80), new FrameSettings(5, 80));
        }

        private void itemMetalSonic_Click(object sender, EventArgs e)
        {

            ajustarTamanhoForm(64, 78);

            pers.Dispose();
            pers = new MetalSonic(this, Tamanho);
        }

        private void itemRobotnik_Click(object sender, EventArgs e)
        {
            ajustarTamanhoForm(240, 178);

            pers.Dispose();
            pers = new Robotnik(this, Tamanho);
        }


        #endregion

        private void contextMenuItemSupreme3_Click(object sender, EventArgs e)
        {
            Process pS = Process.GetCurrentProcess();
            pS.Kill();
            
        }

        private void contextMenuItemSupreme2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
