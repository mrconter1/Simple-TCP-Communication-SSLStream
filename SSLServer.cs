using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SSLServer
{
    class Program
    {
        static void Main(string[] args)
        {

            //Starts Tcp Listener
            var listener = new TcpListener(IPAddress.Loopback, 5300);
            listener.Start();

            //Wait for clients
            var client = listener.AcceptTcpClient();

            //Get client stream
            var stream = client.GetStream();

            //Wrap client in SSLstream
            SslStream sslStream = new SslStream(stream, false);

            //Read self-signed certificate
            var certificate = new X509Certificate2("server.pfx", "password");

            //Authenticate server with self-signed certificate, false (for not requiring client authentication), SSL protocol type, ...
            sslStream.AuthenticateAsServer(certificate,
                                                false,
                                                System.Security.Authentication.SslProtocols.Default,
                                                false);

            //Read messages through SSLStream
            byte[] buffer = new byte[client.ReceiveBufferSize];
            while (client.Connected)
            {
                int bytesRead = sslStream.Read(buffer, 0, client.ReceiveBufferSize);
                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesRead));
            }
        }

    }
}
