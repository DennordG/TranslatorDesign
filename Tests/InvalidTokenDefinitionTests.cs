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

		[TestMethod]
		public void TokenDefinition_IsInvalid8()
		{
			var input = @"false;";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid9()
		{
			var input = "\"asdf\\\" ";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid10()
		{
			var input = "\"test\\p\" ";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid11()
		{
			var input = "\"test\\n\\a\" ";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInvalid12()
		{
			var input = "\"test\"\" ";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsFalse(match.IsMatch);
			Assert.IsNull(match.Value);
			Assert.IsNull(match.RemainingText);
			Assert.AreEqual(TokenType.Invalid, match.TokenType);
		}
	}
}
