namespace TranslatorDesign.Tokenizer
{
	public static class RegexWrapper
	{
		public static string DefaultWrap(string pattern)
		{
			return $@"^{pattern}";
		}

		public static string ReservedWrap(string pattern)
		{
			return $@"^{pattern}\b";
		}
	}
}
