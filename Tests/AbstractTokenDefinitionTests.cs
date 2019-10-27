using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tests
{
	public class AbstractTokenDefinitionTests
	{
		protected readonly ReservedRegexProvider ReservedProvider;
		protected readonly OperatorRegexProvider OperatorProvider;
        protected readonly SyntaxOperatorRegexProvider SyntaxProvider;

		protected readonly string IntegerRegex;
		protected readonly string StringRegex;
		protected readonly string IdentifierRegex;

		public AbstractTokenDefinitionTests()
		{
			ReservedProvider = new ReservedRegexProvider();
            OperatorProvider = new OperatorRegexProvider();
            SyntaxProvider = new SyntaxOperatorRegexProvider();

			IntegerRegex = RegexWrapper.DefaultWrap(@"\d+");
			StringRegex = RegexWrapper.DefaultWrap("\"{1}(?:(?:[^\"\\\\]|(?:\\\\[tn\"'\\\\]))+)\"{1}");
			IdentifierRegex = RegexWrapper.DefaultWrap(@"(?:(?:^_+[a-zA-Z\d]\w*)|(?:^[a-zA-Z]\w*))");
		}
	}
}