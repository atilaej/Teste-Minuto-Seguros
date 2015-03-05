using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace MinutoSeguros.ConsoleEstruturado
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReader xmlReader = XmlReader.Create("http://www.minutoseguros.com.br/blog/feed/");

            SyndicationFeed feeds = SyndicationFeed.Load(xmlReader);
            xmlReader.Close();

            var listaPalavras = new List<string>();

            foreach (SyndicationItem item in feeds.Items)
            {
                listaPalavras.AddRange(
                    RetiraPalavrasInvalidas(
                        item.ElementExtensions.ReadElementExtensions<string>(
                            "encoded", "http://purl.org/rss/1.0/modules/content/"
                        )[0].ToString().ToLower()
                    )
                );
                listaPalavras.AddRange(
                    RetiraPalavrasInvalidas(item.Summary.Text.ToLower())
                );
                listaPalavras.AddRange(
                    RetiraPalavrasInvalidas(item.Title.Text.ToLower())
                );
            }

            var todasPalavrasList = new List<TodasPalavras>();

            foreach (var item in listaPalavras)
            {
                if (todasPalavrasList.Exists(c => c.Palavra == item))
                {
                    todasPalavrasList.First(c => c.Palavra == item).Vezes++;
                }
                else
                {
                    todasPalavrasList.Add(new TodasPalavras()
                    {
                        Palavra = item,
                        Vezes = 1
                    });
                }
            }

            foreach (var item in todasPalavrasList.OrderByDescending(c => c.Vezes).Take(10))
            {
                Console.WriteLine("Palavra: {0} - Vezes: {1}", item.Palavra, item.Vezes);
            }

            Console.ReadKey();
        }

        private static IEnumerable<string> RetiraPalavrasInvalidas(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return null;
            }

            List<string> invalidos = new List<string>()
            {
                //artigos definidos
                "o", "a", "os", "as",

                //artigos indefinidos
                "um", "uma", "uns", "umas",

                //preposições essenciais
                 "a", "ante", "após", "até", "com", 
                 "contra", "de", "desde", "em", "entre", "para", "per", "perante", "por", "sem", 
                 "sob", "sobre", "trás",

                 //Artigos + preposiçoes
                 "ao", "aos", "à", "às",
                 "de", "do", "dos", "da", 
                 "das", "dum", "duns", "duma", "dumas",
                 "em", "no", "nos", "na", "nas", "num",
                 "nuns", "numa", "numas", "por", "per",
                 "pelo", "pelos", "pela", "pelas"
            };

            //tirando as tag´s html
            var textoTemp = Regex.Replace(texto, @"<(.|\n)*?>", string.Empty);
            textoTemp = Regex.Replace(textoTemp, @"\W|\d", " ");
            //tirando espaços em branco duplos
            while (textoTemp.IndexOf("  ", StringComparison.Ordinal) >= 0)
                textoTemp = textoTemp.Replace("  ", " ");

            var palavras = textoTemp.Split(' ');

            var listaRetorno = new List<string>();

            foreach (var palavra in palavras)
            {
                if (!invalidos.Contains(palavra) && !string.IsNullOrEmpty(palavra))
                {
                    listaRetorno.Add(palavra);
                }
            }

            return listaRetorno;
        }
    }

    internal class TodasPalavras
    {
        public string Palavra { get; set; }
        public int Vezes { get; set; }
    }
}
