using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using MinutoSeguros.Lib.Class;

namespace MinutoSeguros.Lib.Base
{
    public class Feed
    {
        public List<Post> Posts { get; set; }
        private const string Endereco = "http://www.minutoseguros.com.br/blog/feed/";

        public Feed()
        {
            Posts = new List<Post>();
            LerFeed();
        }

        private void LerFeed()
        {

            XmlReader xmlReader = XmlReader.Create(Endereco);

            SyndicationFeed feeds = SyndicationFeed.Load(xmlReader);
            xmlReader.Close();
            foreach (SyndicationItem item in feeds.Items)
            {
                var post = new Post();
                //pegando só o primeiro link
                //afinal só tem um
                for (int i = 0; i < item.Links.Count; i++)
                {
                    post.Link = item.Links[i].Uri.ToString();
                }

                post.Conteudo = item.ElementExtensions.ReadElementExtensions<string>("encoded", "http://purl.org/rss/1.0/modules/content/")[0].ToString();
                post.DataPublicacao = item.PublishDate.UtcDateTime;
                post.Resumo = item.Summary.Text;
                post.Titulo = item.Title.Text;

                Posts.Add(post);
            }
        }

        public List<Palavra> TotalGeralDePalavras()
        {
            List<Palavra> retorno = new List<Palavra>();
            foreach (List<Palavra> palavras in Posts.Select(c => c.TodasAsPalavras()))
            {
                foreach (Palavra item in palavras)
                {
                    if (Common.IsValid(item.Literal) && !string.IsNullOrEmpty(item.Literal))
                    {
                        if (retorno.Any(p => p.Literal == item.Literal))
                        {
                            retorno.First(c => c.Literal == item.Literal).Vezes += item.Vezes;
                        }
                        else
                        {
                            retorno.Add(item);
                        }
                    }
                }
            }

            return retorno;
        }

        public List<Palavra> TopPalavras(int quantidade)
        {
            return TotalGeralDePalavras().OrderByDescending(c => c.Vezes).Take(quantidade).ToList();
        }

        public IEnumerable<Post> TopPosts(int quantidade)
        {
            return Posts.OrderByDescending(p => p.DataPublicacao).Take(quantidade);
        }
    }
}
