using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MinutoSeguros.Lib.Class
{
    public class Palavra
    {
        public string Literal { get; set; }
        public int Vezes { get; set; }

        internal IEnumerable<Palavra> ContaPalavra(string texto)
        {
            //tirando as tag´s html
            var textoTemp = Regex.Replace(texto, @"<(.|\n)*?>", string.Empty);
            textoTemp = Regex.Replace(textoTemp, @"\W|\d", " ");
            while (textoTemp.IndexOf("  ", StringComparison.Ordinal) >= 0)
                textoTemp = textoTemp.Replace("  ", " ");
            var palavras = textoTemp.Split(' ');

            List<Palavra> retorno = palavras.Select(t => new Palavra()
            {
                Literal = t,
                Vezes = 1
            }).ToList();

            return retorno.GroupBy(
                    c => new { c.Literal, c.Vezes }
                    ).Select(i => new Palavra()
                    {
                        Literal = i.Key.Literal.ToLower(),
                        Vezes = i.Sum(s => s.Vezes)
                    }
                    ).OrderByDescending(c => c.Vezes).ToList();
        }
    }
}
