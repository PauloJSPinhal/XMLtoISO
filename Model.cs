using Newtonsoft.Json;
using System;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq.Expressions;

namespace XMLtoISO
{
    class Model
    {
        private Controller controller;
        private View view;
        private List<string> dadosIso;
        private string xml;
        private string json;
        private string json_xml;
        private string nomeProgIso;
        private Iso iso = new Iso();
        private Exception exception = null;
        public Model(Controller c, View v) {
            controller = c;
            view = v;
        }
        public Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }
        public List<string> DadosIso
        {
            get { return dadosIso; }
        }
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }
        public string Json
        {
            get { return json; }
            set { json = value; }
        }
        public string Json_xml
        {
            get { return json_xml; }
            set { json_xml = value; }
        }
        public string NomeProgIso
        {
            get { return nomeProgIso; }
        }
        public string XML_toJSON()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                return JsonConvert.SerializeXmlNode(doc);
            } catch (Exception ex)
            {
                Exception = ex;
                return "";
            }
/*
            string sr = JsonConvert.SerializeXmlNode(doc.FirstChild.NextSibling);
            int x = sr.IndexOf('{', sr.IndexOf('{') + 1);
            int y = sr.LastIndexOf('}', sr.LastIndexOf('}'));
            return y > x ? sr.Substring(x, y - x) : sr;
*/
        }
        public void XML_toString(ref List<string> dIso)
        {
            try
            {
                nomeProgIso = dIso.ToArray()[0] + ".PRG";
                dadosIso = dIso;
                dadosIso.RemoveAt(0);
            }
            catch (Exception e)
            {
                Exception = e;
                view.MsgErro(1000, Exception);
                return;
            }
            if ((Exception = iso.carregaIso(dadosIso)) != null)
            {
                view.MsgErro(1010, Exception);
                return;
            }
            Xml = iso.ToXML(Exception);
            if (Exception != null)
            {
                view.MsgErro(1020, Exception);
                return;
            }
            Json = iso.ToJSON(Exception);
            if (Exception != null)
            {
                view.MsgErro(1030, Exception);
                return;
            }
            Json_xml = XML_toJSON();
            if (Exception != null)
            {
                view.MsgErro(1040, Exception);
                return;
            }
            if ((Exception = GravaFicheiroIso()) != null)
            {
                view.MsgErro(1050, Exception);
                return;
            }
            controller.CondicaoBoleanaObjetoValido();
        }
        public Exception GravaFicheiroIso()
        {
            try
            {
                StreamWriter escritor = new StreamWriter($"{NomeProgIso}");
                foreach (PropertyInfo prop in iso.GetType().GetProperties())
                {
                    escritor.WriteLine($"{prop.Name} {prop.GetValue(iso, null)}");
                }
                escritor.Close();
            } catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
    }
    public class Iso
    {
        private string g17;
        private string m06;
        private string g54;
        private string g98;
        private string g28;
        private string g92;
        private string g00;
        private string m96;
        public string G17
        {
            get { return g17; }
            set { g17 = value; }
        }
        public string M06
        {
            get { return m06; }
            set { m06 = value; }
        }
        public string G54
        {
            get { return g54; }
            set { g54 = value; }
        }
        public string G98
        {
            get { return g98; }
            set { g98 = value; }
        }
        public string G28
        {
            get { return g28; }
            set { g28 = value; }
        }
        public string G92
        {
            get { return g92; }
            set { g92 = value; }
        }
        public string G00
        {
            get { return g00; }
            set { g00 = value; }
        }
        public string M96
        {
            get { return m96; }
            set { m96 = value; }
        }
        public Exception carregaIso(List<string> ls)
        {
            try
            {
                foreach (string s in ls)
                {
                    if (s != null && s.Length > 4)
                    {
                        string chave = s.Substring(0, 3);
                        string valor = s.Substring(4);
                        this.GetType().GetProperty(chave).SetValue(this, valor, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }
        public string ToXML(Exception exc)
        {
            try
            {
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(stringwriter, this);
                    return stringwriter.ToString();
                }
            } catch (Exception ex)
            {
                exc = ex;
                return "";
            }
        }
        public string ToJSON(Exception exc)
        {
            try
            {
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception ex)
            {
                exc = exc;
                return "";
            }
        }
    }
}