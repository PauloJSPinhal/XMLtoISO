using System;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Xml.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace XMLtoISO
{
    class XMLtoISO
    {
        static void Main()
        {
            Controller controller = new Controller();
            controller.IniciarPrograma();
            controller.EscolheFicheiroXML();
        }
    }
}
