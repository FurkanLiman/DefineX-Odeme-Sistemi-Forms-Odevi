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
            SuspendLayout();
            // 
            // cmbOdemeTipi
            // 
            cmbOdemeTipi.FormattingEnabled = true;
            cmbOdemeTipi.Location = new Point(435, 132);
            cmbOdemeTipi.Name = "cmbOdemeTipi";
            cmbOdemeTipi.Size = new Size(250, 38);
            cmbOdemeTipi.TabIndex = 0;
            cmbOdemeTipi.DropDown += cmbOdemeTipi_DropDown;
            // 
            // txtTutar
            // 
            txtTutar.Location = new Point(435, 308);
            txtTutar.Name = "txtTutar";
            txtTutar.Size = new Size(250, 35);
            txtTutar.TabIndex = 1;
            // 
            // lblYontem
            // 
            lblYontem.AutoSize = true;
            lblYontem.Font = new Font("Poppins Light", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblYontem.Location = new Point(181, 131);
            lblYontem.Name = "lblYontem";
            lblYontem.Size = new Size(246, 42);
            lblYontem.TabIndex = 2;
            lblYontem.Text = "Ödeme Yöntemleri:";
            // 
            // lblTutar
            // 
            lblTutar.AutoSize = true;
            lblTutar.Font = new Font("Poppins Light", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblTutar.Location = new Point(344, 305);
            lblTutar.Name = "lblTutar";
            lblTutar.Size = new Size(83, 42);
            lblTutar.TabIndex = 3;
            lblTutar.Text = "Tutar:";
            // 
            // btnGonder
            // 
            btnGonder.Font = new Font("Poppins SemiBold", 11.1428576F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 162);
            btnGonder.Location = new Point(318, 408);
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
            lblSonuc.Location = new Point(294, 520);
            lblSonuc.Name = "lblSonuc";
            lblSonuc.Size = new Size(31, 42);
            lblSonuc.TabIndex = 5;
            lblSonuc.Text = "-";
            // 
            // lblSnc
            // 
            lblSnc.AutoSize = true;
            lblSnc.Font = new Font("Poppins", 9.857143F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblSnc.Location = new Point(191, 520);
            lblSnc.Name = "lblSnc";
            lblSnc.Size = new Size(97, 42);
            lblSnc.TabIndex = 6;
            lblSnc.Text = "Sonuç:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 646);
            Controls.Add(lblSnc);
            Controls.Add(lblSonuc);
            Controls.Add(btnGonder);
            Controls.Add(lblTutar);
            Controls.Add(lblYontem);
            Controls.Add(txtTutar);
            Controls.Add(cmbOdemeTipi);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbOdemeTipi;
        private TextBox txtTutar;
        private Label lblYontem;
        private Label lblTutar;
        private Button btnGonder;
        private Label lblSonuc;
        private Label lblSnc;
    }
}
