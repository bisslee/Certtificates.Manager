namespace Certtificates.Manager
{
    public class CertificateInfo
    {
        public string File { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public bool Expiraded => ExpireAt < DateTime.Now;
        public int DaysToExpire => ((ExpireAt - DateTime.Now).Days > 0) ? (ExpireAt - DateTime.Now).Days : 0;

    }
}
