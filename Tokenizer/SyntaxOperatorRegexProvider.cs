using System.Linq;

namespace TranslatorDesign.Tokenizer
{
    public class SyntaxOperatorRegexProvider
    {
        private readonly string[] _patterns;

        public SyntaxOperatorRegexProvider()
        {
            _patterns = new[]
            {
                @"\{", @"\}",
                @"\(", @"\)",
                @"\[", @"\]",
                @"\,", @"\.", ";", "="
            };
        }

        public string GetPattern()
        {
            return string.Join("|", _patterns.Select(RegexWrapper.DefaultWrap));
        }
    }
}
