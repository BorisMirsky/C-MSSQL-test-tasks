using System.Net.Sockets;
using System.Text;
using System.Net;


namespace KESKO_console_task2
{
    class Program
    {
        static void Main()
        {
            GetData();
        }

        //Server
        static void GetData()
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            var tcpListener = new TcpListener(localAddr, 8888);  //IPAddress.Any
            try
            {
                tcpListener.Start();
                Console.WriteLine("Server is running. Wait connection ");
                while (true)
                {
                    using var tcpClient = tcpListener.AcceptTcpClient();
                    var stream = tcpClient.GetStream();
                    byte[] data = new byte[1024];  
                    int bytes = stream.Read(data);
                    string data1 = Encoding.UTF8.GetString(data, 0, bytes);
                    Console.WriteLine(data1);
                    string[] subs = data1.Split(' ');
                    double NumberI = Convert.ToDouble(subs[0]);
                    double NumberJ = Convert.ToDouble(subs[1]);
                    double result = NumberI * NumberJ;
                    Console.WriteLine(result);
                    // send responce
                    stream.Write(Encoding.UTF8.GetBytes(result.ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                tcpListener.Stop();
            }
        }
    }
}
