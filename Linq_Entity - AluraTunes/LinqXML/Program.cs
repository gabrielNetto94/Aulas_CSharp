using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XElement root = XElement.Load(@"..\..\Data\AluraTunes.xml");

            var queryXML =
                from g in root.Element("Generos").Elements("Genero")//acessa os elementos "Genero"
                select g;

            foreach (var genero in queryXML)
            {
                Console.WriteLine("ID:" + genero.Element("GeneroId").Value + " " + genero.Element("Nome").Value);
            }

            var query = from g in root.Element("Generos").Elements("Genero")
                        join m in root.Element("Musicas").Elements("Musica")
                        on g.Element("GeneroId").Value equals m.Element("GeneroId").Value

                        select new //retorno da consulta
                        {
                            Musica = m.Element("Nome").Value,
                            Genero = g.Element("Nome").Value
                        };

            foreach (var musicaEgenero in query)
            {
                Console.WriteLine(musicaEgenero.Musica + " - " + musicaEgenero.Genero);
            }

            Console.ReadKey();
        }
    }
}
