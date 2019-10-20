using Microsoft.VisualStudio.TestTools.UnitTesting;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	[TestClass]
	public class InvalidTokenDefinitionTests : AbstractTokenDefinitionTests
	{
		[TestMethod]
		public void TokenDefinition_IsInvalid1()
		{
			var input = "fake_int a=32;";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid2()
		{
			var input = "boool a=32;";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid3()
		{
			var input = "returns a=32;";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid4()
		{
			var input = "_gcd()";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid5()
		{
			var input = "123abc";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid6()
		{
			var input = "._.";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid7()
		{
			var input = @"\(^o^)/";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}
	}
}
