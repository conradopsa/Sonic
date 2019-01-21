using System;

namespace Sonic
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuPersonagem = new Supreme_Components.Controls.ContextMenuSupreme();
            this.itemSonic = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.itemTails = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.itemMetalSonic = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.itemRobotnik = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.contextMenuSupreme1 = new Supreme_Components.Controls.ContextMenuSupreme();
            this.contextMenuItemSupreme1 = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.contextMenuItemSupreme2 = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.contextMenuItemSupreme3 = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme();
            this.contextMenuPersonagem.SuspendLayout();
            this.contextMenuSupreme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuPersonagem
            // 
            this.contextMenuPersonagem.BackColor = System.Drawing.Color.Transparent;
            this.contextMenuPersonagem.Controls.Add(this.itemSonic);
            this.contextMenuPersonagem.Controls.Add(this.itemTails);
            this.contextMenuPersonagem.Controls.Add(this.itemMetalSonic);
            this.contextMenuPersonagem.Controls.Add(this.itemRobotnik);
            this.contextMenuPersonagem.CorBorda = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.contextMenuPersonagem.CorFundo = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.contextMenuPersonagem.itens = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme[] {
        this.itemSonic,
        this.itemTails,
        this.itemMetalSonic,
        this.itemRobotnik};
            this.contextMenuPersonagem.Location = new System.Drawing.Point(374, 200);
            this.contextMenuPersonagem.Name = "contextMenuPersonagem";
            this.contextMenuPersonagem.Size = new System.Drawing.Size(123, 91);
            this.contextMenuPersonagem.TabIndex = 2;
            // 
            // itemSonic
            // 
            this.itemSonic.BackColor = System.Drawing.Color.Transparent;
            this.itemSonic.contextMenu = null;
            this.itemSonic.CorSelectText = System.Drawing.Color.Black;
            this.itemSonic.Font = new System.Drawing.Font("Verdana", 10F);
            this.itemSonic.ForeColor = System.Drawing.Color.White;
            this.itemSonic.Icon = ((System.Drawing.Image)(resources.GetObject("itemSonic.Icon")));
            this.itemSonic.Location = new System.Drawing.Point(2, 2);
            this.itemSonic.Name = "itemSonic";
            this.itemSonic.Size = new System.Drawing.Size(119, 21);
            this.itemSonic.TabIndex = 0;
            this.itemSonic.Text = "Sonic";
            this.itemSonic.Click += new System.EventHandler(this.itemSonic_Click);
            // 
            // itemTails
            // 
            this.itemTails.BackColor = System.Drawing.Color.Transparent;
            this.itemTails.contextMenu = null;
            this.itemTails.CorSelectText = System.Drawing.Color.Black;
            this.itemTails.Font = new System.Drawing.Font("Verdana", 10F);
            this.itemTails.ForeColor = System.Drawing.Color.White;
            this.itemTails.Icon = ((System.Drawing.Image)(resources.GetObject("itemTails.Icon")));
            this.itemTails.Location = new System.Drawing.Point(2, 24);
            this.itemTails.Name = "itemTails";
            this.itemTails.Size = new System.Drawing.Size(119, 21);
            this.itemTails.TabIndex = 0;
            this.itemTails.Text = "Tails";
            this.itemTails.Click += new System.EventHandler(this.itemTails_Click);
            // 
            // itemMetalSonic
            // 
            this.itemMetalSonic.BackColor = System.Drawing.Color.Transparent;
            this.itemMetalSonic.contextMenu = null;
            this.itemMetalSonic.CorSelectText = System.Drawing.Color.Black;
            this.itemMetalSonic.Font = new System.Drawing.Font("Verdana", 10F);
            this.itemMetalSonic.ForeColor = System.Drawing.Color.White;
            this.itemMetalSonic.Icon = ((System.Drawing.Image)(resources.GetObject("itemMetalSonic.Icon")));
            this.itemMetalSonic.Location = new System.Drawing.Point(2, 46);
            this.itemMetalSonic.Name = "itemMetalSonic";
            this.itemMetalSonic.Size = new System.Drawing.Size(119, 21);
            this.itemMetalSonic.TabIndex = 0;
            this.itemMetalSonic.Text = "Metal Sonic";
            this.itemMetalSonic.Click += new System.EventHandler(this.itemMetalSonic_Click);
            // 
            // itemRobotnik
            // 
            this.itemRobotnik.BackColor = System.Drawing.Color.Transparent;
            this.itemRobotnik.contextMenu = null;
            this.itemRobotnik.CorSelectText = System.Drawing.Color.Black;
            this.itemRobotnik.Font = new System.Drawing.Font("Verdana", 10F);
            this.itemRobotnik.ForeColor = System.Drawing.Color.White;
            this.itemRobotnik.Icon = ((System.Drawing.Image)(resources.GetObject("itemRobotnik.Icon")));
            this.itemRobotnik.Location = new System.Drawing.Point(2, 68);
            this.itemRobotnik.Name = "itemRobotnik";
            this.itemRobotnik.Size = new System.Drawing.Size(119, 21);
            this.itemRobotnik.TabIndex = 0;
            this.itemRobotnik.Text = "Robotnik";
            this.itemRobotnik.Click += new System.EventHandler(this.itemRobotnik_Click);
            // 
            // contextMenuSupreme1
            // 
            this.contextMenuSupreme1.BackColor = System.Drawing.Color.Transparent;
            this.contextMenuSupreme1.Controls.Add(this.contextMenuItemSupreme1);
            this.contextMenuSupreme1.Controls.Add(this.contextMenuItemSupreme2);
            this.contextMenuSupreme1.Controls.Add(this.contextMenuItemSupreme3);
            this.contextMenuSupreme1.CorBorda = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.contextMenuSupreme1.CorFundo = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.contextMenuSupreme1.itens = new Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme[] {
        this.contextMenuItemSupreme1,
        this.contextMenuItemSupreme2,
        this.contextMenuItemSupreme3};
            this.contextMenuSupreme1.Location = new System.Drawing.Point(250, 200);
            this.contextMenuSupreme1.Name = "contextMenuSupreme1";
            this.contextMenuSupreme1.Size = new System.Drawing.Size(118, 69);
            this.contextMenuSupreme1.TabIndex = 1;
            this.contextMenuSupreme1.VisibleChanged += new System.EventHandler(this.contextMenuSupreme1_VisibleChanged);
            // 
            // contextMenuItemSupreme1
            // 
            this.contextMenuItemSupreme1.BackColor = System.Drawing.Color.Transparent;
            this.contextMenuItemSupreme1.contextMenu = this.contextMenuPersonagem;
            this.contextMenuItemSupreme1.CorSelectText = System.Drawing.Color.Black;
            this.contextMenuItemSupreme1.Font = new System.Drawing.Font("Verdana", 10F);
            this.contextMenuItemSupreme1.ForeColor = System.Drawing.Color.White;
            this.contextMenuItemSupreme1.Icon = null;
            this.contextMenuItemSupreme1.Location = new System.Drawing.Point(2, 2);
            this.contextMenuItemSupreme1.Name = "contextMenuItemSupreme1";
            this.contextMenuItemSupreme1.Size = new System.Drawing.Size(114, 21);
            this.contextMenuItemSupreme1.TabIndex = 0;
            this.contextMenuItemSupreme1.Text = "Personagem";
            // 
            // contextMenuItemSupreme2
            // 
            this.contextMenuItemSupreme2.BackColor = System.Drawing.Color.Transparent;
            this.contextMenuItemSupreme2.contextMenu = null;
            this.contextMenuItemSupreme2.CorSelectText = System.Drawing.Color.Black;
            this.contextMenuItemSupreme2.Font = new System.Drawing.Font("Verdana", 10F);
            this.contextMenuItemSupreme2.ForeColor = System.Drawing.Color.White;
            this.contextMenuItemSupreme2.Icon = null;
            this.contextMenuItemSupreme2.Location = new System.Drawing.Point(2, 24);
            this.contextMenuItemSupreme2.Name = "contextMenuItemSupreme2";
            this.contextMenuItemSupreme2.Size = new System.Drawing.Size(114, 21);
            this.contextMenuItemSupreme2.TabIndex = 0;
            this.contextMenuItemSupreme2.Text = "Tamanho";
            this.contextMenuItemSupreme2.Click += new System.EventHandler(this.contextMenuItemSupreme2_Click);
            this.contextMenuItemSupreme2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contextMenuItemSupreme2_MouseDown);
            // 
            // contextMenuItemSupreme3
            // 
            this.contextMenuItemSupreme3.BackColor = System.Drawing.Color.Transparent;
            this.contextMenuItemSupreme3.contextMenu = null;
            this.contextMenuItemSupreme3.CorSelectText = System.Drawing.Color.Black;
            this.contextMenuItemSupreme3.Font = new System.Drawing.Font("Verdana", 10F);
            this.contextMenuItemSupreme3.ForeColor = System.Drawing.Color.White;
            this.contextMenuItemSupreme3.Icon = null;
            this.contextMenuItemSupreme3.Location = new System.Drawing.Point(2, 46);
            this.contextMenuItemSupreme3.Name = "contextMenuItemSupreme3";
            this.contextMenuItemSupreme3.Size = new System.Drawing.Size(114, 21);
            this.contextMenuItemSupreme3.TabIndex = 0;
            this.contextMenuItemSupreme3.Text = "Fechar";
            this.contextMenuItemSupreme3.Click += new System.EventHandler(this.contextMenuItemSupreme3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.ClientSize = new System.Drawing.Size(552, 386);
            this.Controls.Add(this.contextMenuPersonagem);
            this.Controls.Add(this.contextMenuSupreme1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.contextMenuPersonagem.ResumeLayout(false);
            this.contextMenuSupreme1.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion

        private Supreme_Components.Controls.ContextMenuSupreme contextMenuSupreme1;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme contextMenuItemSupreme1;
        private Supreme_Components.Controls.ContextMenuSupreme contextMenuPersonagem;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme itemSonic;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme itemTails;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme itemMetalSonic;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme contextMenuItemSupreme2;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme contextMenuItemSupreme3;
        private Supreme_Components.Controls.ContextMenuSupreme.ContextMenuItemSupreme itemRobotnik;
    }
}

