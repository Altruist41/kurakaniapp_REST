using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace RestfulChatClient
{
    public partial class Form1 : Form
    {
        string userName = string.Empty;
        string roomNumber = string.Empty;

        string getTotalUserURL = @"http://localhost:1234/ajay/api/totalusers/get/room/";
        string getIsRoomValid = @"http://localhost:1234/ajay/api/rooms/get/validity/";
        string getAllMessages = @"http://localhost:1234/ajay/api/messages/get/room/";
        string postMessage = @"http://localhost:1234/ajay/api/messages/post";

        public Form1()
        {
            InitializeComponent();

            var timer = new System.Threading.Timer(
                e => UpdateMessages(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5));
        }

        // Join the chat
        private void button3_Click(object sender, EventArgs e)
        {            
            userName = textBox3.Text;
            roomNumber = textBox4.Text;
            
            string getUserCount = HttpGet(getTotalUserURL + roomNumber);
            dynamic totalUsers = JsonConvert.DeserializeObject(getUserCount.Replace("[", "").Replace("]", ""));

            string getRoomValidity = HttpGet(getIsRoomValid + roomNumber);
            dynamic roomValidity = JsonConvert.DeserializeObject(getRoomValidity.Replace("[", "").Replace("]", ""));

            if ((int)totalUsers.count < 11)
            {
                if ((string)roomValidity.validity == "valid")
                {
                    string messageThread = string.Empty;

                    string getMessages = HttpGet(getAllMessages + roomNumber);
                    dynamic messages = JsonConvert.DeserializeObject(getMessages);

                    int items = ((System.Collections.ICollection)messages).Count;

                    for (int i = 0; i < items; i++)
                    {
                        messageThread += (string)messages[i].user + ": " + (string)messages[i].message + Environment.NewLine;
                    }

                    AppendTextBox(messageThread);

                    panel2.Visible = false;
                }
                else
                {
                    MessageBox.Show("This room expired, all the old messages are deleted.");
                }
            }
            else
            {
                MessageBox.Show("Sorry, you cannot join this room!");
            }
        }

        // Leave the chat
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Post message on button click
        private void button1_Click(object sender, EventArgs e)
        {
            string s = HttpPost(postMessage, "{\"user\":\"" + userName + "\", \"message\":\"" + textBox2.Text + "\", \"room\":\"" + roomNumber + "\"}");
            textBox2.Text = "";
        }
        
        // Post message on enter key press
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
             {
                string s = HttpPost(postMessage, "{\"user\":\"" + userName + "\", \"message\":\"" + textBox2.Text + "\", \"room\":\"" + roomNumber + "\"}");
                textBox2.Text = "";
            }  
        }

        // START - Other supporting functions
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            textBox1.Text = value;
        }

        void UpdateMessages()
        {
            if (userName != string.Empty)
            {
                string messageThread = string.Empty;

                string getMessages = HttpGet(getAllMessages + roomNumber);
                dynamic messages = JsonConvert.DeserializeObject(getMessages);

                int items = ((System.Collections.ICollection)messages).Count;

                for (int i = 0; i < items; i++)
                {
                    messageThread += (string)messages[i].user + ": " + (string)messages[i].message + Environment.NewLine;
                }

                AppendTextBox(messageThread);
            }
        }

        public static string HttpPost(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Adding these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. 
            //Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr =
                  new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        // END - Other supporting functions
    }
}
