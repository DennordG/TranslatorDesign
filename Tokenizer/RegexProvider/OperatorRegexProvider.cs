namespace TranslatorDesign.Tokenizer.RegexProvider
{
    public class OperatorRegexProvider : AbstractRegexProvider
    {
        public OperatorRegexProvider()
        {
            Patterns = new[]
            {
                @"\<\<", @"\>\>", // bit-wise operations
                @"\<=", @"\>=", "!=", "==", @"\<", @"\>", // comparison
                @"\+", "-", @"\*", "/", // arithmetic
                "!", "&&", @"\|\|", // logical operations
            };
        }
    }
}
