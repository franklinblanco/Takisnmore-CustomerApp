using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Rest.Trunking.V1;
using System.Linq;
using System.IO;

namespace Takisnmore.Scripts
{
    public class CustomerClient
    {
        #region Singleton pattern
        private static CustomerClient instance = new CustomerClient();
        private CustomerClient() { }
        public static CustomerClient Instance
        {
            get { return instance; }
        }
        #endregion


        TcpClient clientSocket = new TcpClient();
        NetworkStream clientStream;
        
        public bool ConnectToServer()
        {
            try
            { //Do something in case an exception is thrown...
                clientSocket.Connect(IPAddress.Parse("207.154.255.138"), 5001);
                clientStream = clientSocket.GetStream();

                string authkey = "561039173";
                SendMessage(authkey);
                string r1 = ReceiveMessage();
                if (r1 == "1001")
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
            
        }
        public void DisconnectFromServer()
        {
            clientSocket.Close();
        }
        public bool Reconnect()
        {
            DisconnectFromServer();
            return ConnectToServer();
        }
        public bool Authenticate(string phonenumber, string deviceid, bool otpregister)
        {
            SendMessage(phonenumber);
            string r2 = ReceiveMessage();
            SendMessage(deviceid);
            string r3 = ReceiveMessage();

            if (r3 == "1201")
            {
                return true;
            }
            else if (r3 == "1210")
            {
                if (otpregister)
                {
                    SendMessage("1303");
                    return false;
                }
                SendMessage("1302");
                return false;
            }
            return false;

        }
        public bool SendCustomerCredentials(string name, string password, string gender)
        {
            SendMessage(name + "." + password + "." + gender);
            string r5 = ReceiveMessage();
            if (r5 == "1205")
            {
                return true;
            }
            else if (r5 == "1208")
            {
                return false;
            }
            return false;
        }
        public bool VerifyNumber(string OTP)
        {
            ReceiveMessage();
            SendMessage(OTP);
            string r4 = ReceiveMessage();
            if (r4 == "1207")
            {
                return false;
            }
            else if (r4 == "1204")
            {
                return true;
            }
            return false;
        }
        public string GetHomePageInfo(/*should give the user id so that you can show a personalized one each time*/string what)
        {
            string request = "1102-" + what;
            SendMessage(request);
            return ReceiveMessage();
        }
        public byte[] GetMedia(string id)
        {
            Console.WriteLine("Getting media.");
            string request = "1102-Media-" + id;
            SendMessage(request);
            byte[] datatask = ReceiveData();
            Console.WriteLine("Data received.");
            return datatask;
        }

        #region message sender
        private bool SendMessage(string message)
        {
            try
            {
                byte[] msg = encodeMessage(message);
                clientStream.Write(msg, 0, msg.Length);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }
        #endregion

        #region message receiver
        private string ReceiveMessage()
        {
            byte[] message = new byte[1024];
            try
            {
                int messagesize = clientStream.Read(message, 0, message.Length);
                Array.Resize(ref message, messagesize);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            return decodeMessage(message);
        }
        private byte[] ReceiveData()
        {
            byte[] firstpacket = new byte[1024 * 1000]; //1mbyte
            try
            {
                int firstpacketsize = clientStream.Read(firstpacket, 0, firstpacket.Length);
                Console.WriteLine("Received: " + firstpacketsize + " bytes.");
                Array.Resize(ref firstpacket, firstpacketsize);

                int filesize = ReadHeader(firstpacket);
                byte[] firstpacketnoheader = RemoveHeader(firstpacketsize, filesize, firstpacket);
                Console.WriteLine("First packet size: " + firstpacketnoheader.Length);
                if (firstpacketnoheader.Length != filesize)
                {
                    //FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "receivedimage.jpg"), FileMode.OpenOrCreate, FileAccess.Write);
                    List<byte> file = firstpacketnoheader.ToList();
                    //fs.Write(firs);
                    Console.WriteLine("Waiting for the rest of the data... ");
                    while (file.Count < filesize)
                    {
                        byte[] buffer = new byte[1024 * 1000];
                        int buffersize = clientStream.Read(buffer, 0, buffer.Length);
                        Array.Resize(ref buffer, buffersize);
                        file.AddRange(buffer.ToList());
                        Console.WriteLine("Added: " + buffersize + " bytes");
                    }
                    Console.WriteLine("Final File Size: " + file.Count + " bytes.");
                    return file.ToArray();
                }
                else
                {
                    return firstpacketnoheader;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception has ocurred in method ReceiveData: ");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        #endregion


        #region encoders
        private string decodeMessage(byte[] message)
        {
            return Encoding.Default.GetString(message);
        }
        private byte[] encodeMessage(string message)
        {
            return Encoding.Default.GetBytes(message);
        }
        #endregion

        #region Header Handlers

        private int ReadHeader(byte[] packet)
        {
            bool isint32 = BitConverter.ToBoolean(packet, 0);
            if (!isint32)
            {

                Int16 size = BitConverter.ToInt16(packet, 1);
                Console.WriteLine("It's a 16 bit int, Of length " + size);
                return size;
            }
            else
            {
                Int32 size = BitConverter.ToInt32(packet, 1);
                Console.WriteLine("It's a 32 bit int, Of length " + size);
                return size;
            }
        }

        private byte[] RemoveHeader(int packetsize, int filesize, byte[] packet)
        {
            int difference = 5;
            if (filesize <= 32767)
            {
                difference = 3;
            }
            List<byte> Packet = packet.ToList();
            Console.WriteLine("Packet size when converted to list " + Packet.Count() + " difference: " + difference);
            Packet.RemoveRange(0, difference);
            Console.WriteLine("Removed " + difference + " elements from the packet.");
            return Packet.ToArray();
        }

        #endregion


    }
}
