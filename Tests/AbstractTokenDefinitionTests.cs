using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	public class AbstractTokenDefinitionTests
	{
		protected readonly ReservedRegexProvider ReservedProvider;
		protected readonly string IntegerRegex;

		public AbstractTokenDefinitionTests()
		{
			ReservedProvider = new ReservedRegexProvider();
			IntegerRegex = RegexWrapper.DefaultWrap(@"\d+");
		}
	}
}