using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tests
{
	public class AbstractTokenDefinitionTests
	{
		protected readonly ReservedRegexProvider ReservedProvider;
		protected readonly string IntegerRegex;
        protected readonly OperatorRegexProvider OperatorProvider;
        protected readonly SyntaxOperatorRegexProvider SyntaxProvider;

        public AbstractTokenDefinitionTests()
		{
			ReservedProvider = new ReservedRegexProvider();
			IntegerRegex = RegexWrapper.DefaultWrap(@"\d+");
            OperatorProvider = new OperatorRegexProvider();
            SyntaxProvider = new SyntaxOperatorRegexProvider();

        }
	}
}