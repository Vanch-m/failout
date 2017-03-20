using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class DoggieForm : Form
    {
        public string access_token;
        public string usre_id;

        public DoggieForm()
        {
            InitializeComponent();
        }

        private void SpamForm_Load(object sender, EventArgs e)
        {


            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=5889617&display=..");
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string Url;

            Url = e.Url.ToString();
            if (Url.Contains("access_token"))
            {

                int index = Url.IndexOf("access_token");
                index = index + 13;
                int index1 = Url.IndexOf("&");
                access_token = Url.Substring(index, index1 - index);


            }
            if (Url.Contains("user_id="))
            {
                //string user_id; 
                int index_id = Url.IndexOf("user_id=");
                index_id = index_id + 8;
                usre_id = Url.Substring(index_id);

            }
            XmlDocument Doc = new XmlDocument();
            Doc.Load("https://api.vk.com/method/friends.get.xml?access_token=" + access_token + "&user_ids=" + usre_id + "&fields=photo_100e");
            listView1.Items.Clear();
            foreach (XmlNode tag in Doc.SelectNodes("response"))
            {
                foreach (XmlNode tag1 in tag.SelectNodes("user"))
                {
                    string name = "";
                    foreach (XmlNode tag2 in tag1.SelectNodes("first_name"))
                    {
                        label1.Text = tag2.InnerText;
                        name = tag2.InnerText;
                    }

                    foreach (XmlNode tag3 in tag1.SelectNodes("last_name"))
                    {
                        label1.Text = label1.Text + " " + tag3.InnerXml;
                        name = name + "" + tag3.InnerText;
                    }

                    foreach (XmlNode tag4 in tag1.SelectNodes("photo_100"))
                    {
                        pictureBox1.ImageLocation = tag4.InnerText;
                    }

                    listView1.Items.Add(name);
                    XmlDocument Doc1 = new XmlDocument();
                    Doc1.Load("https://api.vk.com/method/groups.get.xml?user_id=" + usre_id + "&access_token=" + access_token + "&v=5.62");
                    webBrowser1.Visible = false;
                    XmlDocument Doc2 = new XmlDocument();
                    Doc2.Load("https://api.vk.com/method/groups.getMembers.xml?user_id=" + usre_id + "&access_token=" + access_token + "&v=5.62");


                }
            }
        }

       
       


    }
}