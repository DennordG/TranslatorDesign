namespace TranslatorDesign.Tokenizer.RegexProvider
{
    public class SyntaxOperatorRegexProvider : AbstractRegexProvider
    {
        public SyntaxOperatorRegexProvider()
        {
            Patterns = new[]
            {
                @"\{", @"\}",
                @"\(", @"\)",
                @"\[", @"\]",
                @"\,", @"\.", ";", "="
            };
        }
    }
}
