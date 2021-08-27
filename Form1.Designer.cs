
namespace RaspagemAbrasel
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
			this.PnGeckoFx = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// PnGeckoFx
			// 
			this.PnGeckoFx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnGeckoFx.Location = new System.Drawing.Point(0, 0);
			this.PnGeckoFx.Name = "PnGeckoFx";
			this.PnGeckoFx.Size = new System.Drawing.Size(920, 450);
			this.PnGeckoFx.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 450);
			this.Controls.Add(this.PnGeckoFx);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel PnGeckoFx;
	}
}

