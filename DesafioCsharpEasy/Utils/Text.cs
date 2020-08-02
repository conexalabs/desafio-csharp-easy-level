using System.Text.RegularExpressions;

namespace DesafioCsharpEasy.Utils
{
    public static class Text
    {
        public static string RemoveAccents(string text)
        {
            text = Regex.Replace(text, "[ÁÀÂÃÄ]", "A");
            text = Regex.Replace(text, "[ÉÈÊË]", "E");
            text = Regex.Replace(text, "[ÍÌÎÏ]", "I");
            text = Regex.Replace(text, "[ÓÒÔÕÖ]", "O");
            text = Regex.Replace(text, "[ÚÙÛÜ]", "U");
            text = Regex.Replace(text, "[Ç]", "C");
            text = Regex.Replace(text, "[áàâãä]", "a");
            text = Regex.Replace(text, "[éèêë]", "e");
            text = Regex.Replace(text, "[íìîï]", "i");
            text = Regex.Replace(text, "[óòôõö]", "o");
            text = Regex.Replace(text, "[úùûü]", "u");
            text = Regex.Replace(text, "[ç]", "c");
            return text;
        }
    }
}
