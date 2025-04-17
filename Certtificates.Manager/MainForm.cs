using System.Security.Cryptography.X509Certificates;

namespace Certtificates.Manager
{
    public partial class MainForm : Form
    {
        const string CERTIFICATE_EXTENSION = ".pfx";
        const string CERTIFICATE_PASSWORD = ".pwd";
        public List<CertificateInfo> Certificates { get; set; }
        public MainForm()
        {
            InitializeComponent();
            Certificates = new List<CertificateInfo>();
        }

        private void GetCertificates_Click(object sender, EventArgs e)
        {
            CertificateFolderDialog.ShowDialog();

            LoadCertificatesList();
            LoadGrid();

        }

        private void LoadGrid()
        {
            CertificateList.DataBindings.Clear();
            CertificateList.AutoGenerateColumns = false;
            CertificateList.Columns.Clear();

            // Coluna: Nome do Arquivo
            CertificateList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "File",
                HeaderText = "Arquivo PFX",
                Width = 150
            });

            // Coluna: Nome do Certificado
            CertificateList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Nome do Certificado",
                Width = 300
            });

            // Coluna: Data de Expiração
            CertificateList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ExpireAt",
                HeaderText = "Válido Até",
                Width = 100,
                DefaultCellStyle = { 
                    Format = "dd/MM/yyyy" ,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
            });

            // Coluna: Dias Restantes
            CertificateList.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DaysToExpire",
                HeaderText = "Dias para Expirar",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // Coluna: Já Expirou?
            CertificateList.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Expiraded",
                HeaderText = "Expirado?",
                Width = 80
            });

            CertificateList.DataSource = Certificates;
            CertificateList.ClearSelection();
        }

        private void LoadCertificatesList()
        {
            Certificates.Clear();
            if (CertificateFolderDialog.SelectedPath == null)
            {
                MessageBox.Show("Selecione uma pasta com os certificados.");
                return;
            }

            CertificateFolderPath.Text = CertificateFolderDialog.SelectedPath;

            var folderDirectory = new DirectoryInfo(CertificateFolderDialog.SelectedPath);

            var files = folderDirectory.GetFiles($"*{CERTIFICATE_EXTENSION}");
            if (files.Length == 0)
            {
                MessageBox.Show("Nenhum arquivo de certificado encontrado.");
                return;
            }

            foreach (var file in files)
            {
                var pfxPath = file.FullName;
                var pwdFileNamePath = pfxPath.Replace(CERTIFICATE_EXTENSION, CERTIFICATE_PASSWORD);
                var pwdFile = new FileInfo(pwdFileNamePath);
                var senha = pwdFile.Exists ? File.ReadAllText(pwdFile.FullName) : string.Empty;
                if (string.IsNullOrEmpty(senha))
                {
                    continue;
                }
                X509Certificate2 cert = new X509Certificate2(pfxPath, senha);
                var cn = cert.GetNameInfo(X509NameType.SimpleName, false);

                var certificate = new CertificateInfo
                {
                    File = file.Name,
                    Name = cn,
                    ExpireAt = cert.NotAfter
                };

                Certificates.Add(certificate);
            }
        }

        private void CertificateList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cert = CertificateList.Rows[e.RowIndex].DataBoundItem as CertificateInfo;
            if (cert == null)
                return;

            if (cert.Expiraded)
            {
                CertificateList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                CertificateList.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black; // Melhor contraste
            }

            if (!cert.Expiraded && cert.DaysToExpire < 20)
            {
                CertificateList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                CertificateList.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black; // Melhor contraste
            }
        }
    }
}
