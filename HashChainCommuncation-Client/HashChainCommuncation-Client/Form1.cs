using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

namespace HashChainCommuncation_Client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        byte[] firstHashChainInitalVal;
        byte[] secondHashChainInitalVal;
        byte[] tempIV;
        List<byte[]> firstHashChain = new List<byte[]>();
        List<byte[]> secondHashChain = new List<byte[]>();
        List<byte[]> xorKeys = new List<byte[]>();
        static int hashChainLen = 100;
        TempProofProcessor processor = new TempProofProcessor(null, hashChainLen);

        public static byte[] hexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        private static byte[] secureRandomGenerator()
        {
            byte[] val = new byte[16];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(val);
            }
            return val;
        }

        static byte[] hashWithSHA256(byte[] input)
        {
            SHA256CryptoServiceProvider shaHasher = new SHA256CryptoServiceProvider();
            byte[] result = shaHasher.ComputeHash(input);
            return result;
        }

        static string generateHexStringFromByteArray(byte[] input)
        {
            string hexString = BitConverter.ToString(input);
            return hexString.Replace("-", "");
        }

        public static byte[] encryptWithAES128(string input, byte[] key, byte[] IV)
        {
            byte[] byteInput = Encoding.Default.GetBytes(input);
            RijndaelManaged aesObject = new RijndaelManaged();
            aesObject.KeySize = 128;
            aesObject.BlockSize = 128;
            aesObject.Mode = CipherMode.CFB;
            aesObject.FeedbackSize = 128;
            aesObject.Key = key;
            aesObject.IV = IV;
            ICryptoTransform encryptor = aesObject.CreateEncryptor();
            byte[] result = null;

            try
            {
                result = encryptor.TransformFinalBlock(byteInput, 0, byteInput.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }

        public static byte[] decryptWithAES128(string input, byte[] key, byte[] IV)
        {
            // convert input string to byte array
            byte[] byteInput = Encoding.Default.GetBytes(input);
            //byte[] byteInput = hexStringToByteArray(input);


            // create AES object from System.Security.Cryptography
            RijndaelManaged aesObject = new RijndaelManaged();
            // since we want to use AES-128
            aesObject.KeySize = 128;
            // block size of AES is 128 bits
            aesObject.BlockSize = 128;
            // mode -> CipherMode.*
            aesObject.Mode = CipherMode.CFB;
            // feedback size should be equal to block size
            aesObject.FeedbackSize = 128;
            // set the key
            aesObject.Key = key;
            // set the IV
            aesObject.IV = IV;
            // create an decryptor with the settings provided
            ICryptoTransform decryptor = aesObject.CreateDecryptor();
            byte[] result = null;

            try
            {
                result = decryptor.TransformFinalBlock(byteInput, 0, byteInput.Length);
            }
            catch (Exception e) // if decryption fails
            {
                Console.WriteLine(e.Message); // display the cause
            }

            return result;
        }

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = txtServerIP.Text;
            int port;
            if (Int32.TryParse(textBoxServerPort.Text, out port))
            {
                try
                {
                    clientSocket.Connect(IP, port);
                    btnConnectServer.Enabled = false;
                    connected = true;
                    txtSystemMessage.AppendText("Connected to server\n");

                    Thread receiveThread = new Thread(new ThreadStart(Receive));
                    receiveThread.Start();

                    //if connection provided, createhashchains and processor for the first time!
                    var tempval1 = secureRandomGenerator();
                    var tempval2 = secureRandomGenerator();
                    tempIV = secureRandomGenerator();
                    CreateHashChains(tempval1, tempval2);
                    txtSystemMessage.AppendText("Hash Chains have been created!\n");
                    processor.GetHashChains(xorKeys);
                    txtProcessorInfo.AppendText("Keys have been stored!\n");

                    string message = "INITKEY~" + generateHexStringFromByteArray(tempval1) + "+" + generateHexStringFromByteArray(tempval2) + "+" + generateHexStringFromByteArray(tempIV) + "~";
                    byte[] byteMesseage = Encoding.Default.GetBytes(message);
                    clientSocket.Send(byteMesseage);
                    txtOutgoing.AppendText(message + "\n");
                    txtOutgoing.AppendText("---------------------\n");
                }
                catch
                {
                    txtSystemMessage.AppendText("Could not connect to server. Please enter the correct IP-Port pair.\n");
                }
            }
            else
            {
                txtSystemMessage.AppendText("Check the port\n");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonDisconnect_Click(sender, e);
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            connected = false;
            terminating = true;

            if (clientSocket != null)
            {
                clientSocket.Close();
            }

            btnConnectServer.Enabled = true;
            txtSystemMessage.AppendText("Disconnected!!!\n");
        }

        static byte[] hashWithSHA256_string(string input)
        {
            byte[] byteInput = Encoding.Default.GetBytes(input);

            SHA256CryptoServiceProvider shaHasher = new SHA256CryptoServiceProvider();

            byte[] result = shaHasher.ComputeHash(byteInput);

            return result;
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    //Byte[] buffer = new Byte[64];
                    Byte[] buffer = new Byte[2048];
                    clientSocket.Receive(buffer);
                    String bufferText = Encoding.Default.GetString(buffer);
                    bufferText = bufferText.Substring(0, bufferText.IndexOf("\0"));
                    string[] messageSplitArr = bufferText.Split('~');
                    string messageType = messageSplitArr[0];
                    string exactMessage = messageSplitArr[1];

                    switch (messageType)
                    {
                        case "REKEY":
                            var decrpytedMsgReKey = decryptWithAES128(Encoding.Default.GetString(hexStringToByteArray(exactMessage)), processor.GiveKey(), tempIV);
                            var decrpytedReKey = Encoding.Default.GetString(decrpytedMsgReKey);
                            var decryptedReKeySplits = decrpytedReKey.Split('|');

                            if (decrpytedMsgReKey != null && decrpytedMsgReKey.Length > 0)
                            {
                                if (generateHexStringFromByteArray(hashWithSHA256_string(decryptedReKeySplits[0])) == decryptedReKeySplits[1])
                                {
                                    txtSystemMessage.AppendText("Message Integrity Provided!\n");
                                    var hashChainInitalVals = decryptedReKeySplits[0].Split('+');
                                    var reKeyinitval1 = hexStringToByteArray(hashChainInitalVals[0]);
                                    var reKeyinitval2 = hexStringToByteArray(hashChainInitalVals[1]);
                                    CreateHashChains(reKeyinitval1, reKeyinitval2);
                                    txtSystemMessage.AppendText("Hash Chains have been created!\n");
                                    processor.GetHashChains(xorKeys);
                                    processor.FlushKeyIndex();
                                    txtProcessorInfo.AppendText("Keys have been stored!\n");
                                    txtIncoming.AppendText(bufferText + "\n");
                                    txtIncoming.AppendText("---------------------\n");

                                }
                                else
                                {
                                    txtSystemMessage.AppendText("Message Integrity Fail!!!\n");
                                }
                            }                           
                            break;
                        case "MSG":
                            var decrpytedMsg = decryptWithAES128(Encoding.Default.GetString(hexStringToByteArray(exactMessage)), processor.GiveKey(), tempIV);
                            var decrpytedMessageStr = Encoding.Default.GetString(decrpytedMsg);
                            var splittedMessages = decrpytedMessageStr.Split('|');
                            if (decrpytedMsg != null && decrpytedMsg.Length > 0)
                            {
                                if (generateHexStringFromByteArray(hashWithSHA256_string(splittedMessages[0])) == splittedMessages[1])
                                {
                                    txtSystemMessage.AppendText("Message Integrity Provided!\n");
                                    txtIncoming.AppendText("ENC MESSAGE --> " + exactMessage + "\n" + "ORIGINAL MESSAGE --> " + Encoding.Default.GetString(decrpytedMsg) + "\n");
                                    txtIncoming.AppendText("---------------------\n");
                                }
                                else
                                {
                                    txtSystemMessage.AppendText("Message Integrity Fail!!!\n");
                                }
                            }
                            break;
                        case "NEXTKEY":
                            txtSystemMessage.AppendText(exactMessage + "\n");
                            processor.ReKey();
                            break;
                        default:
                            txtIncoming.AppendText("\n" + exactMessage);
                            break;
                    }
                }

                catch
                {
                    if (!terminating)
                    {
                        btnConnectServer.Enabled = true;
                        txtSystemMessage.AppendText("The server has disconnected\n");
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }
   
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                string broadcastMessage = txtMessage.Text;
                var hashedMessageStr = generateHexStringFromByteArray(hashWithSHA256_string(broadcastMessage));
                var key = processor.GiveKey();
                var keyIndex = processor.GetKeyIndex();
                txtProcessorInfo.AppendText("Key Index-->" + (keyIndex).ToString() + "\t" + "Key-->" + generateHexStringFromByteArray(key) + "\n");
                Byte[] encryptedMessage = encryptWithAES128(broadcastMessage + "|" + hashedMessageStr + "|", key, tempIV);
                Byte[] broadcastMessageArr = Encoding.Default.GetBytes("MSG~" + generateHexStringFromByteArray(encryptedMessage) + "~");

                //byte[] byteMostSign = new byte[8];
                //Array.Copy(key, 8, byteMostSign, 0, 8);

                clientSocket.Send(broadcastMessageArr);
                txtOutgoing.AppendText("ENC MESSAGE--> " + Encoding.Default.GetString(broadcastMessageArr) + "\n" + "MESSAGE --> " + broadcastMessage + "\n");
                txtOutgoing.AppendText("---------------------\n");
            }
            catch
            {
                txtSystemMessage.AppendText("Could not send login request to server.\n");
            }
        }

        public static byte[] exclusiveOR(byte[] arr1, byte[] arr2)
        {
            byte[] result = new byte[arr1.Length];

            for (int i = 0; i < arr1.Length; ++i)
                result[i] = (byte)(arr1[i] ^ arr2[i]);

            return result;
        }

        private void CreateHashChains(byte[] firstChainVal, byte[] secondChainVal)
        {
            xorKeys = new List<byte[]>();
            firstHashChain = new List<byte[]>();
            secondHashChain = new List<byte[]>();

            firstHashChainInitalVal = firstChainVal;
            secondHashChainInitalVal = secondChainVal;

            var firstHashValue = hashWithSHA256(firstHashChainInitalVal);
            var secondHashValue = hashWithSHA256(secondHashChainInitalVal);
            firstHashChain.Add(firstHashValue);
            secondHashChain.Add(secondHashValue);
            for (int i = 1; i < hashChainLen; i++)
            {
                firstHashValue = hashWithSHA256(firstHashValue);
                firstHashChain.Add(firstHashValue);
                secondHashValue = hashWithSHA256(secondHashValue);
                secondHashChain.Add(secondHashValue);
            }

            for (int i = 0; i < hashChainLen; i++)
            {
                var firstHashVal = firstHashChain[i];
                var secondHashVal = secondHashChain[hashChainLen - i - 1];
                xorKeys.Add(exclusiveOR(firstHashVal, secondHashVal));
            }
        }


        private void btnReKey_Click(object sender, EventArgs e)
        {
            var tempval1 = secureRandomGenerator();
            var tempval2 = secureRandomGenerator();
         
            string message = "REKEY~" + generateHexStringFromByteArray(tempval1) + "+" + generateHexStringFromByteArray(tempval2) + "~";
            byte[] byteMesseage = Encoding.Default.GetBytes(message);

            try
            {                
                var hashedMessageStr = generateHexStringFromByteArray(hashWithSHA256_string(generateHexStringFromByteArray(tempval1) + "+" + generateHexStringFromByteArray(tempval2)));
                var key = processor.GiveKey();
                var keyIndex = processor.GetKeyIndex();
                txtProcessorInfo.AppendText("Key Index-->" + (keyIndex).ToString() + "\t" + "Key-->" + generateHexStringFromByteArray(key) + "\n");
                Byte[] encryptedMessage = encryptWithAES128(generateHexStringFromByteArray(tempval1) + "+" + generateHexStringFromByteArray(tempval2) + "|" + hashedMessageStr + "|", key, tempIV);
                Byte[] broadcastMessageArr = Encoding.Default.GetBytes("REKEY~" + generateHexStringFromByteArray(encryptedMessage) + "~");

                clientSocket.Send(broadcastMessageArr);

                CreateHashChains(tempval1, tempval2);
                txtSystemMessage.AppendText("Hash Chains have been created!\n");
                processor.GetHashChains(xorKeys);
                processor.FlushKeyIndex();
                txtProcessorInfo.AppendText("Keys have been stored!\n");

                txtOutgoing.AppendText("ENC MESSAGE--> " + Encoding.Default.GetString(broadcastMessageArr) + "\n");
                txtOutgoing.AppendText("---------------------\n");
            }
            catch
            {
                txtSystemMessage.AppendText("Could not send login request to server.\n");
            }
        }

        private void btnNewReKey_Click(object sender, EventArgs e)
        {
            processor.ReKey();
            var key = processor.GiveKey();
            var keyIndex = processor.GetKeyIndex();
            txtSystemMessage.AppendText("Key has been changed!\n");
            txtProcessorInfo.AppendText("Key Index-->" + (keyIndex).ToString() + "\t" + "Key-->" + generateHexStringFromByteArray(key) + "\n");
            clientSocket.Send(Encoding.Default.GetBytes("NEXTKEY~" + "Key has been changed!" + "~"));
        }
    }

    public class TempProofProcessor
    {
        List<byte[]> keys = new List<byte[]>();
        int hashChainLen;
        int index = 0;


        public TempProofProcessor(List<byte[]> _keys, int len)
        {
            keys = _keys;
            hashChainLen = len;
        }

        public void GetHashChains(List<byte[]> _keys)
        {
            keys = _keys;
        }

        public byte[] GiveKey()
        {
            var key = keys[index];
            return key;
        }

        public int GetKeyIndex() { return index; }
        public void ReKey() { index++; }
        public void FlushKeyIndex() { index = 0; }


    }

}
