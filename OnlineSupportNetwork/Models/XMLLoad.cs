using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;

namespace OnlineSupportNetwork.Models
{
    public class XMLLoad
    {
        public string username = "", accounttype = "", password = "", repetpassword = "", accountnumber = "", bankname = "", accountname = "", check = "";

        public void LoadFile(string path, string nod)
        {
            XmlDocument xmlLoadf = new XmlDocument();
            xmlLoadf.Load(path.ToString());
            XmlNodeList nodeList = xmlLoadf.DocumentElement.SelectNodes(nod.ToString());// ("/Table/Product");"/User"

            //ClientName();

            foreach (XmlNode node in nodeList)
            {
                username = node.SelectSingleNode("username").InnerText;
                accounttype = node.SelectSingleNode("accounttype").InnerText;
                password = node.SelectSingleNode("password").InnerText;
                repetpassword = node.SelectSingleNode("repetpassword").InnerText;
                accountnumber = node.SelectSingleNode("accountnumber").InnerText;
                bankname = node.SelectSingleNode("bankname").InnerText;
                accountname = node.SelectSingleNode("accountname").InnerText;
                check = node.SelectSingleNode("check").InnerText;


            }
        }

        public void SaveFile(string path, string nod, string[] param)
        {
            XmlDocument xmlSavef = new XmlDocument();
            xmlSavef.Load(path.ToString());
            XmlNodeList nodeList = xmlSavef.DocumentElement.SelectNodes(nod.ToString());

            //ClientName();

            foreach (XmlNode node in nodeList)
            {
                node.SelectSingleNode("username").InnerText = param[0].ToString();
                node.SelectSingleNode("accounttype").InnerText = param[1].ToString();
                node.SelectSingleNode("password").InnerText = param[2].ToString();
                node.SelectSingleNode("repetpassword").InnerText = param[3].ToString();

                if (param[4].ToString() != "---")
                {
                    node.SelectSingleNode("accountnumber").InnerText = param[4].ToString();
                }
                if (param[5].ToString() != "---")
                {
                    node.SelectSingleNode("bankname").InnerText = param[5].ToString();
                }
                if (param[6].ToString() != "---")
                {
                    node.SelectSingleNode("accountname").InnerText = param[6].ToString();
                }

                node.SelectSingleNode("check").InnerText = param[7].ToString();

            }
            xmlSavef.Save(path.ToString());
        }

        public void CreateFile(string path, string[] param)
        {
            string xmlText = string.Empty;
          

            //ClientName();
            
            xmlText = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" + "\n" +
                           "<User>" + "\n" +
                           "  <" + param[0].ToString() + ">" + "\n" +
                           "    <username>" + param[0].ToString() + "</username>" + "\n" +
                           "    <accounttype>" + param[1].ToString() + "</accounttype>" + "\n" +
                           "    <password>" + param[2].ToString() + "</password>" + "\n" +
                           "    <repetpassword >" + param[3].ToString() + "</repetpassword>" + "\n" +
                           "    <accountnumber>" + param[4].ToString() + "</accountnumber>" + "\n" +
                           "    <bankname>" + param[5].ToString() + "</bankname>" + "\n" +
                           "    <accountname>" + param[6].ToString() + "</accountname>" + "\n" +
                           "    <check>" + param[7].ToString() + "</check>" + "\n" +
                           "  </" + param[0].ToString() + ">" + "\n" +
                           "</User>";
            File.WriteAllText(path, xmlText);
        }

        public void AddClient(string path, string nod, string[] param)
        {
            string xmlText = string.Empty;

            xmlText = File.ReadAllText(path);

            /*
            if (xmlText.IndexOf(username.ToString()) > -1)//az ghabl client set shode bashad
            {
                XmlDocument xmlSaved = new XmlDocument();
                xmlSaved.Load(path.ToString());
                XmlNodeList nodeList = xmlSaved.DocumentElement.SelectNodes(nod.ToString());

                foreach (XmlNode node in nodeList)
                {
                    node.SelectSingleNode("username").InnerText = username.ToString();
                    node.SelectSingleNode("accounttype").InnerText = accounttype.ToString();
                    node.SelectSingleNode("password").InnerText = password.ToString();
                    node.SelectSingleNode("repetpassword").InnerText = repetpassword.ToString();
                    node.SelectSingleNode("accountnumber").InnerText = accountnumber.ToString();
                    node.SelectSingleNode("bankname").InnerText = bankname.ToString();
                    node.SelectSingleNode("accountname").InnerText = accountname.ToString();
                }
                xmlSaved.Save(path.ToString());
            }
            else//baraye bar avval client set Mishavad
            {
            */
                xmlText = xmlText.Replace("</User>", "");
                xmlText = xmlText.ToString();

                xmlText = xmlText + "\n" +
                               "  <" + param[0].ToString() + ">" + "\n" +
                           "    <username>" + param[0].ToString() + "</username>" + "\n" +
                           "    <accounttype>" + param[1].ToString() + "</accounttype>" + "\n" +
                           "    <password>" + param[2].ToString() + "</password>" + "\n" +
                           "    <repetpassword >" + param[3].ToString() + "</repetpassword>" + "\n" +
                           "    <accountnumber>" + param[4].ToString() + "</accountnumber>" + "\n" +
                           "    <bankname>" + param[5].ToString() + "</bankname>" + "\n" +
                           "    <accountname>" + param[6].ToString() + "</accountname>" + "\n" +
                           "    <check>" + param[7].ToString() + "</check>" + "\n" +
                           "  </" + param[0].ToString() + ">" + "\n" +
                               "</User>";
                File.WriteAllText(path.ToString(), xmlText);
            //}

        }

        public void CreateQuestionFile(string path, string[] param)
        {
            string xmlText = string.Empty;


            //ClientName();

            xmlText = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" + "\n" +
                           "<User>" + "\n" +
                           "  <" + param[0].ToString() + ">" + "\n" +
                           "    <username>" + param[0].ToString() + "</username>" + "\n" +
                           "    <accountname>" + param[1].ToString() + "</accountname>" + "\n" +
                           "    <productname>" + param[2].ToString() + "</productname>" + "\n" +
                           "    <hardsoft >" + param[3].ToString() + "</hardsoft>" + "\n" +
                           "    <productmodel>" + param[4].ToString() + "</productmodel>" + "\n" +
                           "    <modulename>" + param[5].ToString() + "</modulename>" + "\n" +
                           "    <bankname>" + param[6].ToString() + "</bankname>" + "\n" +
                           "    <message>" + param[7].ToString() + "</message>" + "\n" +
                           "  </" + param[0].ToString() + ">" + "\n" +
                           "</User>";
            File.WriteAllText(path, xmlText);
        }

        public void ClientName()
        {
            /*
            GetComputerName computername = new GetComputerName();
            GetMAC MAC = new GetMAC();
            GetIPAdd IP = new GetIPAdd();

            string X = string.Empty;
            X = computername.name.ToString() + MAC.firstMacAddress.ToString() + "$.Borji";// + IP.ip.ToString();
            int x = X.GetHashCode();
            proClientName = "Client" + x.ToString();

            */
        }
    }
}