using System;
using System.Collections.Generic;
using System.Linq;

namespace MinutoSeguros.Lib.Class
{
    public class Post
    {
        private string _titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        private string _resumo { get; set; }
        private string _conteudo;
        private readonly List<Palavra> _palavras;

        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                Palavra palavra = new Palavra();
                _palavras.AddRange(palavra.ContaPalavra(value));
            }

        }

        public string Resumo
        {
            get { return _resumo; }
            set
            {
                _resumo = value;
                Palavra palavra = new Palavra();
                _palavras.AddRange(palavra.ContaPalavra(value));
            }

        }

        public string Conteudo
        {
            get { return _conteudo; }
            set
            {
                _conteudo = value;
                Palavra palavra = new Palavra();
                _palavras.AddRange(palavra.ContaPalavra(value));
            }
        }
        public string Link { get; set; }
        

        public Post()
        {
            this._palavras = new List<Palavra>();
        }

        public List<Palavra> TodasAsPalavras()
        {
            return _palavras.GroupBy(
                    c => new { c.Literal, c.Vezes }
                    ).Select(i => new Palavra()
                    {
                        Literal = i.Key.Literal.ToLower(),
                        Vezes = i.Sum(s => s.Vezes)
                    }
                    ).OrderByDescending(c => c.Vezes).ToList(); ;
        }

        public int TotalDePalavras()
        {
            return TodasAsPalavras().Sum(c => Common.IsValid(c.Literal) ? c.Vezes : 0);
        }
    }
}
