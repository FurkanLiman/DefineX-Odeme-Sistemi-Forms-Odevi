using System.ComponentModel.DataAnnotations.Schema;

namespace DefineX_Odeme_Sistemi_Forms_Odevi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbOdemeTipi = new ComboBox();
            txtTutar = new TextBox();
            lblYontem = new Label();
            lblTutar = new Label();
            btnGonder = new Button();
            lblSonuc = new Label();
            lblSnc = new Label();
            label1 = new Label();
            txtYeniOdemeIsim = new TextBox();
            btnKaydet = new Button();
            label2 = new Label();
            txtYeniOdemeDeger = new TextBox();
            lblKaydet = new Label();
            SuspendLayout();
            // 
            // cmbOdemeTipi
            // 
            cmbOdemeTipi.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            cmbOdemeTipi.FormattingEnabled = true;
            cmbOdemeTipi.Location = new Point(369, 115);
            cmbOdemeTipi.Name = "cmbOdemeTipi";
            cmbOdemeTipi.Size = new Size(250, 48);
            cmbOdemeTipi.TabIndex = 0;
            cmbOdemeTipi.DropDown += cmbOdemeTipi_DropDown;
            // 
            // txtTutar
            // 
            txtTutar.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtTutar.Location = new Point(369, 289);
            txtTutar.Name = "txtTutar";
            txtTutar.Size = new Size(250, 42);
            txtTutar.TabIndex = 1;
            // 
            // lblYontem
            // 
            lblYontem.AutoSize = true;
            lblYontem.Font = new Font("Poppins Light", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblYontem.Location = new Point(117, 118);
            lblYontem.Name = "lblYontem";
            lblYontem.Size = new Size(246, 42);
            lblYontem.TabIndex = 2;
            lblYontem.Text = "Ödeme Yöntemleri:";
            // 
            // lblTutar
            // 
            lblTutar.AutoSize = true;
            lblTutar.Font = new Font("Poppins Light", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblTutar.Location = new Point(280, 292);
            lblTutar.Name = "lblTutar";
            lblTutar.Size = new Size(83, 42);
            lblTutar.TabIndex = 3;
            lblTutar.Text = "Tutar:";
            // 
            // btnGonder
            // 
            btnGonder.Font = new Font("Poppins SemiBold", 11.1428576F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            btnGonder.Location = new Point(254, 395);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new Size(200, 59);
            btnGonder.TabIndex = 4;
            btnGonder.Text = "Ödeme Yap";
            btnGonder.UseVisualStyleBackColor = true;
            btnGonder.Click += btnGonder_Click;
            // 
            // lblSonuc
            // 
            lblSonuc.AutoSize = true;
            lblSonuc.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblSonuc.Location = new Point(230, 507);
            lblSonuc.Name = "lblSonuc";
            lblSonuc.Size = new Size(31, 42);
            lblSonuc.TabIndex = 5;
            lblSonuc.Text = "-";
            // 
            // lblSnc
            // 
            lblSnc.AutoSize = true;
            lblSnc.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblSnc.Location = new Point(127, 507);
            lblSnc.Name = "lblSnc";
            lblSnc.Size = new Size(97, 42);
            lblSnc.TabIndex = 6;
            lblSnc.Text = "Sonuç:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Poppins Light", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.Location = new Point(740, 112);
            label1.Name = "label1";
            label1.Size = new Size(317, 42);
            label1.TabIndex = 7;
            label1.Text = "Yeni Ödeme Yöntemi Adı:";
            // 
            // txtYeniOdemeIsim
            // 
            txtYeniOdemeIsim.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtYeniOdemeIsim.Location = new Point(1063, 112);
            txtYeniOdemeIsim.Name = "txtYeniOdemeIsim";
            txtYeniOdemeIsim.Size = new Size(263, 42);
            txtYeniOdemeIsim.TabIndex = 8;
            // 
            // btnKaydet
            // 
            btnKaydet.Font = new Font("Poppins SemiBold", 11.1428576F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            btnKaydet.Location = new Point(990, 289);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(172, 67);
            btnKaydet.TabIndex = 9;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Poppins Light", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.Location = new Point(710, 184);
            label2.Name = "label2";
            label2.Size = new Size(347, 42);
            label2.TabIndex = 10;
            label2.Text = "Yeni Ödeme Yöntemi Value:";
            // 
            // txtYeniOdemeDeger
            // 
            txtYeniOdemeDeger.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtYeniOdemeDeger.Location = new Point(1063, 184);
            txtYeniOdemeDeger.Name = "txtYeniOdemeDeger";
            txtYeniOdemeDeger.Size = new Size(263, 42);
            txtYeniOdemeDeger.TabIndex = 11;
            // 
            // lblKaydet
            // 
            lblKaydet.AutoSize = true;
            lblKaydet.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblKaydet.Location = new Point(976, 412);
            lblKaydet.Name = "lblKaydet";
            lblKaydet.Size = new Size(31, 42);
            lblKaydet.TabIndex = 12;
            lblKaydet.Text = "-";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1595, 646);
            Controls.Add(lblKaydet);
            Controls.Add(txtYeniOdemeDeger);
            Controls.Add(label2);
            Controls.Add(btnKaydet);
            Controls.Add(txtYeniOdemeIsim);
            Controls.Add(label1);
            Controls.Add(lblSnc);
            Controls.Add(lblSonuc);
            Controls.Add(btnGonder);
            Controls.Add(lblTutar);
            Controls.Add(lblYontem);
            Controls.Add(txtTutar);
            Controls.Add(cmbOdemeTipi);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        [ZorunluAlan]
        private ComboBox cmbOdemeTipi;
        [ZorunluAlan]
        [SayiAlan]
        private TextBox txtTutar;
        private Label lblYontem;
        private Label lblTutar;
        private Button btnGonder;
        private Label lblSonuc;
        private Label lblSnc;
        private Label label1;
        [ZorunluAlan]
        private TextBox txtYeniOdemeIsim;
        private Button btnKaydet;
        private Label label2;
        [ZorunluAlan]
        private TextBox txtYeniOdemeDeger;
        private Label lblKaydet;
    }
}
