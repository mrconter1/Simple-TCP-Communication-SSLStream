using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SSLClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create tcp client
            TcpClient client = new TcpClient("127.0.0.1", 5300);
            var stream = client.GetStream();

            //Wrap the stream and use "RemoteCertificateValidationCallback" in some way that allows all certificates
            SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(CertificateValidationCallback));

            //Authenticate 
            sslStream.AuthenticateAsClient("clientName");

            //Start sending encrypted messages
            string message = "Sending encrypted message";
            while (true)
            {
                sslStream.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            }

        }

        //Callback function that allows all certificates
        static bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
