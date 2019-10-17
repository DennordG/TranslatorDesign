using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	public class AbstractTokenDefinitionTests
	{
		protected readonly ReservedRegexProvider ReservedProvider;

		public AbstractTokenDefinitionTests()
		{
			ReservedProvider = new ReservedRegexProvider();
		}
	}
}