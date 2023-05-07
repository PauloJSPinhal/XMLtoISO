// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace XMLtoISO
{
    public class Controller
    {
        private Model model;
        private View view;
        List<string> lsiso;
        bool objetoValido = false;
        public void IniciarPrograma()
        {
            view = new View(this);
            model = new Model(this, view);
            view.ModelRef = model;

            lsiso = view.MostrarMenu();
        }
        public void EscolheFicheiroXML()
        {
            VerificarValidadeXML();
        }
        private void VerificarValidadeXML()
        {
            model.XML_toString(ref lsiso);
            if (objetoValido)
            {
                view.MsgOutput();
            }
        }
        public void CondicaoBoleanaObjetoValido()
        {
            objetoValido = true;
        }
    }
}
