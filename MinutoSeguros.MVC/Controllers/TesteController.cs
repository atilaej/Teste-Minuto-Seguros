using System.Web.Mvc;
using MinutoSeguros.Lib.Base;

namespace MinutoSeguros.MVC.Controllers
{
    public class TesteController : Controller
    {
        
        // GET: Teste
        public ActionResult Posts()
        {
            Feed feed = new Feed();

            var conteudo = feed.TopPosts(10);

            return View(conteudo);
        }

        public ActionResult Palavras()
        {
            Feed feed = new Feed();

            var conteudo = feed.TopPalavras(10);

            return View(conteudo);
        }
    }
}