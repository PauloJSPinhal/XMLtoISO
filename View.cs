using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
namespace XMLtoISO
{
    class View
    {
        private XMLtoISO xmltoiso;
        private Controller controller;
        private Model model;
        public View(Controller c)
        {
            controller = c;
        }
        public Model ModelRef { set { model = value; } }
        public List<string> MostrarMenu()
        {
            Console.WriteLine("Um produto de SimProgramming");
            Console.WriteLine("Equipa3");
            Console.WriteLine("XMLtoISO");
            Console.WriteLine("Máquinas ONA ");
            Console.WriteLine("Preparação de comando numérico.\n");
            Console.WriteLine("Menu");
            Console.WriteLine("1-->Programa de 1 elétrodo!");
            Console.WriteLine("2-->Programa com várias peças e elétrodos!");
            Console.Write("\nOpção: ");
            char opcao;
            while ((opcao = (Console.ReadKey(true).KeyChar)) != '1' && opcao != '2');
            Console.WriteLine(opcao);
            Console.WriteLine(opcao == '1' ? "\nElétrodo" : "\nPeças");
            return opcao == '1' ? eletrodo() : pecas();
        }
        private List<string> eletrodo()
        {
            string X, Y, Z, C, GAP, T;

            //Console.Write("Inserir numero de eletrodo (Exemplo:1E100): ");
            //string TE = Console.ReadLine();
            Console.Write("\nInserir número de posição do garfo: ");
            T = Console.ReadLine();

            Console.Write("Cota X: ");
            X = Console.ReadLine();

            Console.Write("Cota Y: ");
            Y = Console.ReadLine();

            Console.Write("Cota Z: ");
            Z = Console.ReadLine();

            Console.Write("Cota C: ");
            C = Console.ReadLine();

            Console.Write("GAP (0.1, 0.15, 0.2, .., 1): ");
            GAP = Console.ReadLine();

            //            escritor.WriteLine("ID0000TX0101%");

            return new List<string>()
            {
                "1",
                "G17 C100",
                $"M06 X{T}",
                $"G54 X1",
                "G98 Z0",
                $"G28 X{X} Y{Y} Z{Z} C{C}",
                "G92 Z300",
                "G00 Z30",
                "M96 X95" + obterGAP(GAP)
            };
//            escritor.WriteLine("G98 Z0");
//            escritor.WriteLine("G28 Z200 C0");
//            escritor.WriteLine("%");
        }
        private List<string> pecas()
        {
            string X, Y, Z, C, GAP, T, PRG, TCp;

            Console.Write("\nInserir numero de programa a utilizar (Exemplo: 1 a 8999): ");
            PRG = Console.ReadLine();
            //Console.WriteLine("Inserir numero de eletrodo (Exemplo:1E100) ");
            //string TEp = Console.ReadLine();
            Console.Write("Inserir número de posição do garfo: ");
            T = Console.ReadLine();
            Console.Write("Inserir numero de centramento de peça (Exemplo:1 a 255): ");
            TCp = Console.ReadLine();

            Console.Write("Cota X: ");
            X = Console.ReadLine();

            Console.Write("Cota Y: ");
            Y = Console.ReadLine();

            Console.Write("Cota Z: ");
            Z = Console.ReadLine();

            Console.Write("Cota C: ");
            C = Console.ReadLine();

            Console.Write("GAP (0.1, 0.15, 0.2, .., 1): ");
            GAP = Console.ReadLine();

//            escritor.WriteLine("ID0000TX0101%");

            return new List<string>()
            {
                PRG,
                "G17 C100",
                $"M06 X{T}",
                $"G54 X{TCp}",
                "G98 Z0",
                $"G28 X{X} Y{Y} Z{Z} C{C}",
                "G92 Z300",
                "G00 Z30",
                "M96 X95" + obterGAP(GAP)
            };
        //            escritor.WriteLine("G98 Z0");
        //            escritor.WriteLine("G28 Z200 C0");
        //            escritor.WriteLine("%");
        }
        private string obterGAP(string s)
        {
            double gapp = Convert.ToDouble(s, System.Globalization.CultureInfo.InvariantCulture);
            double[] gn = { 0.1, 0.15, 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5, 0.55, 0.6, 0.65, 0.7, 0.75, 0.8, 0.85, 1 };
            string[] gs = { "10", "15", "20", "25", "30", "35", "40", "45", "50", "55", "60", "65", "70", "65", "65", "85", "85" };
            int gind = Array.IndexOf(gn, gapp);
            return gind == -1 ? gs[4] : gs[gind];
        }

        public void MsgOutput()
        {
            Console.WriteLine("\nNome do ficheiro ISO: " + model.NomeProgIso + '\n');
            Console.WriteLine("Dados no formato ISO:");
            foreach (string s in model.DadosIso)
            {
                Console.WriteLine(s);
            }
            Console.Write("\nPremir uma tecla para continuar...");
            Console.ReadKey(true);
            Console.WriteLine("\n\nDados no formato XML convertidos a partir do formato ISO:");
            Console.WriteLine(model.Xml);
            Console.Write("\nPremir uma tecla para continuar...");
            Console.ReadKey(true);
            Console.WriteLine("\n\nDados no formato JSON convertidos a partir do formato XML:");
            Console.WriteLine(model.Json_xml);
            Console.Write("\nPremir uma tecla para continuar...");
            Console.ReadKey(true);
            Console.WriteLine("\n\nDados no formato JSON convertidos a partir do formato ISO:");
            Console.WriteLine(model.Json);
            Console.Write("\nPremir uma tecla para sair...");
            Console.ReadKey(true);
        }
        public void MsgErro(int erro, Exception ex)
        {
            Console.WriteLine("Erro:" + erro);
            Console.WriteLine(ex.Message);
            Console.Write("Premir uma tecla para sair...");
            Console.ReadKey(true);
        }
    }
}