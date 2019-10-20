namespace TranslatorDesign.Tokenizer
{
	public static class RegexWrapper
	{
		public static string DefaultWrap(string pattern)
		{
			return $@"^{pattern}\b";
		}
	}
}
