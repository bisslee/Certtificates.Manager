namespace Certtificates.Manager
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            CertificateFolderDialog = new FolderBrowserDialog();
            CertificateList = new DataGridView();
            GetCertificates = new Button();
            CertificateFolderPath = new TextBox();
            ((System.ComponentModel.ISupportInitialize)CertificateList).BeginInit();
            SuspendLayout();
            // 
            // CertificateList
            // 
            CertificateList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CertificateList.Location = new Point(25, 63);
            CertificateList.Name = "CertificateList";
            CertificateList.Size = new Size(767, 333);
            CertificateList.TabIndex = 3;
            CertificateList.CellFormatting += CertificateList_CellFormatting;
            // 
            // GetCertificates
            // 
            GetCertificates.Location = new Point(654, 27);
            GetCertificates.Name = "GetCertificates";
            GetCertificates.Size = new Size(138, 24);
            GetCertificates.TabIndex = 4;
            GetCertificates.Text = "Get";
            GetCertificates.UseVisualStyleBackColor = true;
            GetCertificates.Click += GetCertificates_Click;
            // 
            // CertificateFolderPath
            // 
            CertificateFolderPath.BorderStyle = BorderStyle.FixedSingle;
            CertificateFolderPath.Location = new Point(25, 27);
            CertificateFolderPath.Name = "CertificateFolderPath";
            CertificateFolderPath.ReadOnly = true;
            CertificateFolderPath.Size = new Size(608, 23);
            CertificateFolderPath.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 417);
            Controls.Add(CertificateFolderPath);
            Controls.Add(GetCertificates);
            Controls.Add(CertificateList);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Gerenciador de Certificados";
            ((System.ComponentModel.ISupportInitialize)CertificateList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog CertificateFolderDialog;
        private DataGridView CertificateList;
        private Button GetCertificates;
        private TextBox CertificateFolderPath;
    }
}
