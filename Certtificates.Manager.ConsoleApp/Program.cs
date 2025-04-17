using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Certtificates.Manager.ConsoleApp
{
    internal static class Program
    {
        private static readonly string certPath = @"C:\cert\";
        static void Main(string[] args)
        {

            var dir = new DirectoryInfo(certPath);
            var result = new StringBuilder();
            foreach (var file in dir.GetFiles("*.pfx"))
            {
                var pfxPath = file.FullName;
                var pwdFileNamePath = pfxPath.Replace(".pfx", ".pwd");
                var pwdFile = new FileInfo(pwdFileNamePath);
                var senha = pwdFile.Exists ? File.ReadAllText(pwdFile.FullName) : string.Empty;

                if (string.IsNullOrEmpty(senha))
                {
                    result.AppendLine($"| {file.Name}: Sem Senha                                                             |");
                    continue;
                }


                result.Append($"| {file.Name} ");

                X509Certificate2 cert = new X509Certificate2(pfxPath, senha);
                var cn = cert.GetNameInfo(X509NameType.SimpleName, false);
                result.Append($"| {cn} ");
                result.Append($"| {cert.NotBefore}");
                result.Append($"| {cert.NotAfter}");


                var daysToExpire = (cert.NotAfter - DateTime.Now).TotalDays;
                var expired = daysToExpire < 0;

                var status = expired
                    ? "Expirado"
                    : (daysToExpire < 30)
                            ? "expirando"
                            : "Válido"
                    ;

                result.Append($"| {status}");

                if (!expired)
                {
                    result.Append($"| {daysToExpire} dias");
                }

                result.AppendLine(" |");


            }

            Console.WriteLine(result.ToString());
        }
    }

    public class Certificado
    {
        public string File { get; set; }
        public string Name { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool Expirado { get; set; }
        public int DiasParaExpirar
        {
            get
            {
                if (ExpireAt < DateTime.Now)
                {
                    Expirado = true;
                    return 0;
                }

                Expirado = false;
                var days = (int)(ExpireAt - DateTime.Now).TotalDays;

                return days < 0 ? 0 : days;
            }
        }
    }
}
