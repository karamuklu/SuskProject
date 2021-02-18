namespace INF_SUSK_Project
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnExcelOku = new System.Windows.Forms.Button();
            this.btnSUSK_Kaydet = new System.Windows.Forms.Button();
            this.txtSUSK = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prgsBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDepoTransfer = new System.Windows.Forms.Button();
            this.dgw_Ekran = new DevExpress.XtraGrid.GridControl();
            this.dgvListe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDurumKontrol = new System.Windows.Forms.Button();
            this.btnYariMamulSUSK = new System.Windows.Forms.Button();
            this.txtSUSKGirisDepo = new System.Windows.Forms.TextBox();
            this.txtSUSKCikisDepo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSUSKCikisDepoLbl = new System.Windows.Forms.Label();
            this.txtIsemriNo = new System.Windows.Forms.TextBox();
            this.btnAspirin = new System.Windows.Forms.Button();
            this.txtParola = new System.Windows.Forms.TextBox();
            this.txtIsemriBarkod = new System.Windows.Forms.TextBox();
            this.txtSeriNo = new System.Windows.Forms.TextBox();
            this.btnDirekIsemriAc = new System.Windows.Forms.Button();
            this.ilkSeviyeIsemriAc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw_Ekran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExcelOku
            // 
            this.btnExcelOku.Location = new System.Drawing.Point(12, 9);
            this.btnExcelOku.Name = "btnExcelOku";
            this.btnExcelOku.Size = new System.Drawing.Size(102, 47);
            this.btnExcelOku.TabIndex = 2;
            this.btnExcelOku.Text = "Exceli Aç";
            this.btnExcelOku.UseVisualStyleBackColor = true;
            this.btnExcelOku.Click += new System.EventHandler(this.btnExcelOku_Click);
            // 
            // btnSUSK_Kaydet
            // 
            this.btnSUSK_Kaydet.Location = new System.Drawing.Point(806, 58);
            this.btnSUSK_Kaydet.Name = "btnSUSK_Kaydet";
            this.btnSUSK_Kaydet.Size = new System.Drawing.Size(110, 46);
            this.btnSUSK_Kaydet.TabIndex = 5;
            this.btnSUSK_Kaydet.Text = "SUSK Kaydet";
            this.btnSUSK_Kaydet.UseVisualStyleBackColor = true;
            this.btnSUSK_Kaydet.Click += new System.EventHandler(this.btnSUSK_Kaydet_Click);
            // 
            // txtSUSK
            // 
            this.txtSUSK.Location = new System.Drawing.Point(806, 36);
            this.txtSUSK.Name = "txtSUSK";
            this.txtSUSK.Size = new System.Drawing.Size(110, 20);
            this.txtSUSK.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(803, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "SUSK Başlangıç Harf";
            // 
            // prgsBar
            // 
            this.prgsBar.Location = new System.Drawing.Point(0, 206);
            this.prgsBar.Name = "prgsBar";
            this.prgsBar.Size = new System.Drawing.Size(1142, 23);
            this.prgsBar.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1023, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Kayıt Sayısı";
            // 
            // btnDepoTransfer
            // 
            this.btnDepoTransfer.Location = new System.Drawing.Point(0, 153);
            this.btnDepoTransfer.Name = "btnDepoTransfer";
            this.btnDepoTransfer.Size = new System.Drawing.Size(124, 47);
            this.btnDepoTransfer.TabIndex = 12;
            this.btnDepoTransfer.Text = "Hammadde Kontrol & Depo Transfer";
            this.btnDepoTransfer.UseVisualStyleBackColor = true;
            this.btnDepoTransfer.Click += new System.EventHandler(this.btnDepoTransfer_Click);
            // 
            // dgw_Ekran
            // 
            this.dgw_Ekran.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgw_Ekran.Location = new System.Drawing.Point(0, 232);
            this.dgw_Ekran.MainView = this.dgvListe;
            this.dgw_Ekran.Name = "dgw_Ekran";
            this.dgw_Ekran.Size = new System.Drawing.Size(1142, 517);
            this.dgw_Ekran.TabIndex = 13;
            this.dgw_Ekran.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvListe});
            // 
            // dgvListe
            // 
            this.dgvListe.GridControl = this.dgw_Ekran;
            this.dgvListe.Name = "dgvListe";
            this.dgvListe.OptionsBehavior.Editable = false;
            // 
            // btnDurumKontrol
            // 
            this.btnDurumKontrol.Location = new System.Drawing.Point(130, 57);
            this.btnDurumKontrol.Name = "btnDurumKontrol";
            this.btnDurumKontrol.Size = new System.Drawing.Size(130, 47);
            this.btnDurumKontrol.TabIndex = 14;
            this.btnDurumKontrol.Text = "Yarı Mamul Durum Kontrol";
            this.btnDurumKontrol.UseVisualStyleBackColor = true;
            this.btnDurumKontrol.Click += new System.EventHandler(this.btnDurumKontrol_Click);
            // 
            // btnYariMamulSUSK
            // 
            this.btnYariMamulSUSK.Location = new System.Drawing.Point(130, 153);
            this.btnYariMamulSUSK.Name = "btnYariMamulSUSK";
            this.btnYariMamulSUSK.Size = new System.Drawing.Size(140, 47);
            this.btnYariMamulSUSK.TabIndex = 16;
            this.btnYariMamulSUSK.Text = "Yarı Mamul SUSK";
            this.btnYariMamulSUSK.UseVisualStyleBackColor = true;
            this.btnYariMamulSUSK.Click += new System.EventHandler(this.btnYariMamulSUSK_Click);
            // 
            // txtSUSKGirisDepo
            // 
            this.txtSUSKGirisDepo.Location = new System.Drawing.Point(586, 12);
            this.txtSUSKGirisDepo.Name = "txtSUSKGirisDepo";
            this.txtSUSKGirisDepo.Size = new System.Drawing.Size(100, 20);
            this.txtSUSKGirisDepo.TabIndex = 17;
            this.txtSUSKGirisDepo.Text = "118";
            // 
            // txtSUSKCikisDepo
            // 
            this.txtSUSKCikisDepo.Location = new System.Drawing.Point(586, 36);
            this.txtSUSKCikisDepo.Name = "txtSUSKCikisDepo";
            this.txtSUSKCikisDepo.Size = new System.Drawing.Size(100, 20);
            this.txtSUSKCikisDepo.TabIndex = 18;
            this.txtSUSKCikisDepo.Text = "118";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(514, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Giriş Depo";
            // 
            // txtSUSKCikisDepoLbl
            // 
            this.txtSUSKCikisDepoLbl.AutoSize = true;
            this.txtSUSKCikisDepoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSUSKCikisDepoLbl.Location = new System.Drawing.Point(514, 39);
            this.txtSUSKCikisDepoLbl.Name = "txtSUSKCikisDepoLbl";
            this.txtSUSKCikisDepoLbl.Size = new System.Drawing.Size(68, 13);
            this.txtSUSKCikisDepoLbl.TabIndex = 20;
            this.txtSUSKCikisDepoLbl.Text = "Çıkış Depo";
            // 
            // txtIsemriNo
            // 
            this.txtIsemriNo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtIsemriNo.Location = new System.Drawing.Point(130, 12);
            this.txtIsemriNo.Name = "txtIsemriNo";
            this.txtIsemriNo.Size = new System.Drawing.Size(130, 20);
            this.txtIsemriNo.TabIndex = 22;
            this.txtIsemriNo.Text = "İşemri için Harf Giriniz";
            this.txtIsemriNo.Enter += new System.EventHandler(this.txtIsemriNo_Enter);
            this.txtIsemriNo.Leave += new System.EventHandler(this.txtIsemriNo_Leave);
            // 
            // btnAspirin
            // 
            this.btnAspirin.Location = new System.Drawing.Point(806, 146);
            this.btnAspirin.Name = "btnAspirin";
            this.btnAspirin.Size = new System.Drawing.Size(110, 54);
            this.btnAspirin.TabIndex = 23;
            this.btnAspirin.Text = "123-ATEŞŞŞ--->>";
            this.btnAspirin.UseVisualStyleBackColor = true;
            this.btnAspirin.Click += new System.EventHandler(this.btnAspirin_Click);
            // 
            // txtParola
            // 
            this.txtParola.Location = new System.Drawing.Point(806, 120);
            this.txtParola.Name = "txtParola";
            this.txtParola.PasswordChar = '*';
            this.txtParola.Size = new System.Drawing.Size(110, 20);
            this.txtParola.TabIndex = 24;
            this.txtParola.TextChanged += new System.EventHandler(this.txtParola_TextChanged);
            // 
            // txtIsemriBarkod
            // 
            this.txtIsemriBarkod.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtIsemriBarkod.Location = new System.Drawing.Point(275, 12);
            this.txtIsemriBarkod.Name = "txtIsemriBarkod";
            this.txtIsemriBarkod.Size = new System.Drawing.Size(143, 20);
            this.txtIsemriBarkod.TabIndex = 25;
            this.txtIsemriBarkod.Text = "İşemri Barkod Okutunuz";
            this.txtIsemriBarkod.Enter += new System.EventHandler(this.txtIsemriBarkod_Enter);
            this.txtIsemriBarkod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIsemriBarkod_KeyDown);
            this.txtIsemriBarkod.Leave += new System.EventHandler(this.txtIsemriBarkod_Leave);
            // 
            // txtSeriNo
            // 
            this.txtSeriNo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtSeriNo.Location = new System.Drawing.Point(987, 28);
            this.txtSeriNo.Name = "txtSeriNo";
            this.txtSeriNo.Size = new System.Drawing.Size(108, 20);
            this.txtSeriNo.TabIndex = 26;
            this.txtSeriNo.Text = "Seri No Okutunuz";
            this.txtSeriNo.Enter += new System.EventHandler(this.txtSeriNo_Enter);
            this.txtSeriNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeriNo_KeyDown);
            this.txtSeriNo.Leave += new System.EventHandler(this.txtSeriNo_Leave);
            // 
            // btnDirekIsemriAc
            // 
            this.btnDirekIsemriAc.Location = new System.Drawing.Point(398, 153);
            this.btnDirekIsemriAc.Name = "btnDirekIsemriAc";
            this.btnDirekIsemriAc.Size = new System.Drawing.Size(126, 47);
            this.btnDirekIsemriAc.TabIndex = 27;
            this.btnDirekIsemriAc.Text = "Direk İşemri Aç";
            this.btnDirekIsemriAc.UseVisualStyleBackColor = true;
            this.btnDirekIsemriAc.Click += new System.EventHandler(this.btnDirekIsemriAc_Click);
            // 
            // ilkSeviyeIsemriAc
            // 
            this.ilkSeviyeIsemriAc.Location = new System.Drawing.Point(586, 153);
            this.ilkSeviyeIsemriAc.Name = "ilkSeviyeIsemriAc";
            this.ilkSeviyeIsemriAc.Size = new System.Drawing.Size(128, 47);
            this.ilkSeviyeIsemriAc.TabIndex = 28;
            this.ilkSeviyeIsemriAc.Text = "1. Seviye İşemri Aç";
            this.ilkSeviyeIsemriAc.UseVisualStyleBackColor = true;
            this.ilkSeviyeIsemriAc.Click += new System.EventHandler(this.ilkSeviyeIsemriAc_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 749);
            this.Controls.Add(this.ilkSeviyeIsemriAc);
            this.Controls.Add(this.btnDirekIsemriAc);
            this.Controls.Add(this.txtSeriNo);
            this.Controls.Add(this.txtIsemriBarkod);
            this.Controls.Add(this.txtParola);
            this.Controls.Add(this.btnAspirin);
            this.Controls.Add(this.txtIsemriNo);
            this.Controls.Add(this.txtSUSKCikisDepoLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSUSKCikisDepo);
            this.Controls.Add(this.txtSUSKGirisDepo);
            this.Controls.Add(this.btnYariMamulSUSK);
            this.Controls.Add(this.btnDurumKontrol);
            this.Controls.Add(this.dgw_Ekran);
            this.Controls.Add(this.btnDepoTransfer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prgsBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSUSK);
            this.Controls.Add(this.btnSUSK_Kaydet);
            this.Controls.Add(this.btnExcelOku);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serbest Üretim Sonu Kaydı v.2.1.2 - İnform Elektronik";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.dgw_Ekran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnExcelOku;
		private System.Windows.Forms.Button btnSUSK_Kaydet;
		private System.Windows.Forms.TextBox txtSUSK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar prgsBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDepoTransfer;
        private DevExpress.XtraGrid.GridControl dgw_Ekran;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvListe;
        private System.Windows.Forms.Button btnDurumKontrol;
        private System.Windows.Forms.Button btnYariMamulSUSK;
        private System.Windows.Forms.TextBox txtSUSKGirisDepo;
        private System.Windows.Forms.TextBox txtSUSKCikisDepo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtSUSKCikisDepoLbl;
        private System.Windows.Forms.TextBox txtIsemriNo;
        private System.Windows.Forms.Button btnAspirin;
        private System.Windows.Forms.TextBox txtParola;
        private System.Windows.Forms.TextBox txtIsemriBarkod;
        private System.Windows.Forms.TextBox txtSeriNo;
        private System.Windows.Forms.Button btnDirekIsemriAc;
        private System.Windows.Forms.Button ilkSeviyeIsemriAc;
    }
}

