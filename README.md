# Simple-TCP-Communication-SSLStream-C#

This is a simple example of a TCP server and client both wrapped with a SSLStream with a self-signed certificate. Companies usually buy a certified certificates but this can be unnecessarily for small projects. If you use a self-signed certificate instead as in this case you will only be left with the encryption part of SSLStream. By using the native C# SSLStream library you don't need to implement your own network encryption method with, say, RSA + AES. Although you won't get the authorization part of SSL, the stream will still be encrypted.
- The code is written in C#.

**Guide to set up:**
1. Create two executable projects using the code found in "SslClient.cs" and "SslServer.cs".
2. The SslServer needs a certification file. You can either use the one found on this repository or you can create your own. 
3. Place the certification file in the same folder as the SslServer executable before starting the server. After the server is running, start the client.

**Guide to creating your own self-signed certificate on Windows:**
1. Find the "makecert.exe" and "pvk2pfx.exe" files by navigating to "C:\Program Files (x86)\Windows Kits\8.0\bin\x64". *If you cannot find the folder you need to install the "Microsoft Windows SDK" found [Here](https://www.microsoft.com/en-us/download/details.aspx?id=8279).*
2. Navigate to the folder with the terminal. You can start the terminal in a folder by right-clicking and choosing "Open command window here".
3. Run the following two commands to create a certification file named "server.pfx":

`"makecert.exe -r -pe -n "CN=localhost" -sky exchange -sv server.pkv server.cer"`

`pvk2pfx -pvk server.pkv -spc server.cer -pfx server.pfx -pi password`

*"password" can be replaced by whatever you entered when you ran "makecert.exe". I choose "password" everywhere.*
  
