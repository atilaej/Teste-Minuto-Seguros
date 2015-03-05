using MinutoSeguros.Lib.Base;

namespace MinutoSeguros.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var feed = new Feed();

            foreach (var item in feed.TopPosts(10))
            {
                System.Console.WriteLine(item.Titulo);
            }
            foreach (var palavra in feed.TopPalavras(10))
            {
                System.Console.WriteLine("=====> Palavra {0}, {1}", palavra.Literal, palavra.Vezes);
            }
            System.Console.ReadKey();
        }
    }
}
