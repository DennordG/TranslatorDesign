using System.Linq;

namespace TranslatorDesign.Tokenizer
{
    public class OperatorRegexProvider
    {
        private readonly string[] _patterns;

        public OperatorRegexProvider()
        {
            _patterns = new[]
            {
                @"\<\<", @"\>\>", // bit-wise operations
                @"\<=", @"\>=", "!=", "==", @"\<", @"\>", // comparison
                @"\+", "-", @"\*", "/", // arithemtic
                "!", "&&", @"\|\|", // logical operations
            };
        }

        public string GetPattern()
        {
            return string.Join("|", _patterns.Select(RegexWrapper.DefaultWrap));
        }
    }
}
