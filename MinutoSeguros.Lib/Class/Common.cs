using System.Collections.Generic;

namespace MinutoSeguros.Lib.Class
{
    class Common
    {
        public static bool IsValid(string palavra)
        {
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

            if (invalidos.Contains(palavra))
            {
                return false;
            }

            return true;
        }
    }
}
