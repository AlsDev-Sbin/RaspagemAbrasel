
namespace RaspagemAbrasel
{
	partial class ViewMain
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
			this.BtProcurar = new MaterialSkin.Controls.MaterialButton();
			this.TxCaminho = new MaterialSkin.Controls.MaterialTextBox();
			this.BtIniciarRaspagem = new MaterialSkin.Controls.MaterialButton();
			this.LbRegioes = new MaterialSkin.Controls.MaterialLabel();
			this.LbEstabelecimento = new MaterialSkin.Controls.MaterialLabel();
			this.LbCidades = new MaterialSkin.Controls.MaterialLabel();
			this.Progress = new MaterialSkin.Controls.MaterialProgressBar();
			this.SuspendLayout();
			// 
			// BtProcurar
			// 
			this.BtProcurar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BtProcurar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
			this.BtProcurar.Depth = 0;
			this.BtProcurar.HighEmphasis = true;
			this.BtProcurar.Icon = null;
			this.BtProcurar.Location = new System.Drawing.Point(481, 104);
			this.BtProcurar.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.BtProcurar.MouseState = MaterialSkin.MouseState.HOVER;
			this.BtProcurar.Name = "BtProcurar";
			this.BtProcurar.Size = new System.Drawing.Size(97, 36);
			this.BtProcurar.TabIndex = 5;
			this.BtProcurar.Text = "Procurar";
			this.BtProcurar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
			this.BtProcurar.UseAccentColor = false;
			this.BtProcurar.UseVisualStyleBackColor = true;
			this.BtProcurar.Click += new System.EventHandler(this.BtProcurar_Click);
			// 
			// TxCaminho
			// 
			this.TxCaminho.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TxCaminho.Depth = 0;
			this.TxCaminho.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.TxCaminho.Hint = "Caminho onde será salvo a planilha";
			this.TxCaminho.LeadingIcon = null;
			this.TxCaminho.Location = new System.Drawing.Point(21, 90);
			this.TxCaminho.MaxLength = 50;
			this.TxCaminho.MouseState = MaterialSkin.MouseState.OUT;
			this.TxCaminho.Multiline = false;
			this.TxCaminho.Name = "TxCaminho";
			this.TxCaminho.Size = new System.Drawing.Size(441, 50);
			this.TxCaminho.TabIndex = 7;
			this.TxCaminho.Text = "";
			this.TxCaminho.TrailingIcon = null;
			// 
			// BtIniciarRaspagem
			// 
			this.BtIniciarRaspagem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BtIniciarRaspagem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BtIniciarRaspagem.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
			this.BtIniciarRaspagem.Depth = 0;
			this.BtIniciarRaspagem.HighEmphasis = true;
			this.BtIniciarRaspagem.Icon = null;
			this.BtIniciarRaspagem.Location = new System.Drawing.Point(213, 357);
			this.BtIniciarRaspagem.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.BtIniciarRaspagem.MouseState = MaterialSkin.MouseState.HOVER;
			this.BtIniciarRaspagem.Name = "BtIniciarRaspagem";
			this.BtIniciarRaspagem.Size = new System.Drawing.Size(155, 36);
			this.BtIniciarRaspagem.TabIndex = 8;
			this.BtIniciarRaspagem.Text = "Iniciar raspagem";
			this.BtIniciarRaspagem.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
			this.BtIniciarRaspagem.UseAccentColor = false;
			this.BtIniciarRaspagem.UseVisualStyleBackColor = true;
			this.BtIniciarRaspagem.Click += new System.EventHandler(this.BtIniciarRaspagem_Click);
			// 
			// LbRegioes
			// 
			this.LbRegioes.AutoSize = true;
			this.LbRegioes.Depth = 0;
			this.LbRegioes.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.LbRegioes.Location = new System.Drawing.Point(18, 159);
			this.LbRegioes.MouseState = MaterialSkin.MouseState.HOVER;
			this.LbRegioes.Name = "LbRegioes";
			this.LbRegioes.Size = new System.Drawing.Size(78, 19);
			this.LbRegioes.TabIndex = 9;
			this.LbRegioes.Tag = "Regiões : {0}";
			this.LbRegioes.Text = "Regiões : 0";
			// 
			// LbEstabelecimento
			// 
			this.LbEstabelecimento.AutoSize = true;
			this.LbEstabelecimento.Depth = 0;
			this.LbEstabelecimento.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.LbEstabelecimento.Location = new System.Drawing.Point(18, 242);
			this.LbEstabelecimento.MouseState = MaterialSkin.MouseState.HOVER;
			this.LbEstabelecimento.Name = "LbEstabelecimento";
			this.LbEstabelecimento.Size = new System.Drawing.Size(139, 19);
			this.LbEstabelecimento.TabIndex = 10;
			this.LbEstabelecimento.Tag = "Estabelecimento : {0}";
			this.LbEstabelecimento.Text = "Estabelecimento : 0";
			// 
			// LbCidades
			// 
			this.LbCidades.AutoSize = true;
			this.LbCidades.Depth = 0;
			this.LbCidades.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.LbCidades.Location = new System.Drawing.Point(18, 200);
			this.LbCidades.MouseState = MaterialSkin.MouseState.HOVER;
			this.LbCidades.Name = "LbCidades";
			this.LbCidades.Size = new System.Drawing.Size(90, 19);
			this.LbCidades.TabIndex = 11;
			this.LbCidades.Tag = "Cidade(s) : {0}";
			this.LbCidades.Text = "Cidade(s) : 0";
			// 
			// Progress
			// 
			this.Progress.Depth = 0;
			this.Progress.Location = new System.Drawing.Point(-1, 64);
			this.Progress.MouseState = MaterialSkin.MouseState.HOVER;
			this.Progress.Name = "Progress";
			this.Progress.Size = new System.Drawing.Size(597, 5);
			this.Progress.Step = 2;
			this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.Progress.TabIndex = 12;
			this.Progress.Value = 100;
			// 
			// ViewMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(594, 407);
			this.Controls.Add(this.Progress);
			this.Controls.Add(this.LbCidades);
			this.Controls.Add(this.LbEstabelecimento);
			this.Controls.Add(this.LbRegioes);
			this.Controls.Add(this.BtIniciarRaspagem);
			this.Controls.Add(this.TxCaminho);
			this.Controls.Add(this.BtProcurar);
			this.Name = "ViewMain";
			this.Text = "Raspagem da abrasel";
			this.Load += new System.EventHandler(this.ViewMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MaterialSkin.Controls.MaterialButton BtProcurar;
		private MaterialSkin.Controls.MaterialTextBox TxCaminho;
		private MaterialSkin.Controls.MaterialButton BtIniciarRaspagem;
		private MaterialSkin.Controls.MaterialLabel LbRegioes;
		private MaterialSkin.Controls.MaterialLabel LbEstabelecimento;
		private MaterialSkin.Controls.MaterialLabel LbCidades;
		private MaterialSkin.Controls.MaterialProgressBar Progress;
	}
}

