using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConsoleApp1C
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("košarica: ");

            var nemaPopusta = new NemaPopusta();
            var popust50 = new Popust50();

            var kosarica1 = new kosarica(popust50);

            kosarica1.Add(10, "daljinski");
            kosarica1.Popust = nemaPopusta;
            kosarica1.Add(12, "baterije");

            kosarica1.print();
        }
    }
    class kosarica
    {
        private IList<double> cijene;
        private IList<string> proizvodi;

        private struct Data
        {
            public Data(double intValue, string strValue)
            {
                IntegerData = intValue;
                StringData = strValue;
                
            }

            public double IntegerData { get; private set; }
            public string StringData { get; private set; }
           
        }

        public ICijenaPopust Popust { get; set; }

        private List<Data> p;

        public kosarica(ICijenaPopust Popust)
        {
            this.proizvodi = new List<string>();
            this.cijene = new List<double>();
            this.Popust = Popust;

            this.p = new List<Data>();
        }
        public void Add(double price, string ime)
        {
            this.cijene.Add(this.Popust.GetPrice(price));
            this.proizvodi.Add(this.Popust.GetProizvod(ime));
            var d1 = new Data(this.Popust.GetPrice(price), this.Popust.GetProizvod(ime));

            this.p.Add(d1);
            
        }
        
        public void print()
        {

            foreach (var x in this.p)
            {
                Console.WriteLine(x.IntegerData + " " + x.StringData );
            }
            
            //this.drinks.Clear();
        }
    }

    interface ICijenaPopust
    {
        double GetPrice(double cijena);
        string GetProizvod(string ime);


    }
    class NemaPopusta : ICijenaPopust
    {
        public double GetPrice(double cijena) => cijena;
        public string GetProizvod(string ime) => ime + " nema popusta";

    }

    // Strategy for Happy hour (50% discount)
    class Popust50 : ICijenaPopust
    {
        public double GetPrice(double cijena) => cijena * 0.5;
        public string GetProizvod(string ime)
        {
            return ime + " popust 50%";
        }
    }
}
