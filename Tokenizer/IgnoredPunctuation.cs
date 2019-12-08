namespace TranslatorDesign.Tokenizer
{
	public static class IgnoredPunctuation
	{
		private const string Punctuation = ";(){}";

		public static bool Contains(string text)
		{
			return Punctuation.Contains(text);
		}
	}
}