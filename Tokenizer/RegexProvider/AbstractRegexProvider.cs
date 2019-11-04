namespace TranslatorDesign.Tokenizer.RegexProvider
{
	public class AbstractRegexProvider
    {
        protected string[] Patterns { get; set; }

        public virtual string GetPattern()
        {
            return string.Join("|", Patterns);
        }
    }
}
