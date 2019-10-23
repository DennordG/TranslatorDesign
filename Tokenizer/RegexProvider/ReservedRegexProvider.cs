namespace TranslatorDesign.Tokenizer.RegexProvider
{
    public class ReservedRegexProvider : AbstractRegexProvider
	{
		public ReservedRegexProvider()
		{
			Patterns = new[]
			{
				"int", "bool", "void", // data types
				"true", "false", // boolean values
				"if", "else", // conditional
				"while", // loops
				"return", // return
				"cin", "cout" // input/output
			};
		}
	}
}
