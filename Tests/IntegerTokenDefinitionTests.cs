using Microsoft.VisualStudio.TestTools.UnitTesting;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	[TestClass]
	public class IntegerTokenDefinitionTests : AbstractTokenDefinitionTests
	{
		[TestMethod]
		public void TokenDefinition_IsInteger1()
		{
			var input = "123 + 11";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

            Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("123", match.Value);
			Assert.AreEqual("+ 11", match.RemainingText);
			Assert.AreEqual(TokenType.Integer, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInteger2()
		{
			var input = "0115 -1";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("0115", match.Value);
			Assert.AreEqual("-1", match.RemainingText);
			Assert.AreEqual(TokenType.Integer, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInteger3()
		{
			var input = "619949104146091346013403410946";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("619949104146091346013403410946", match.Value);
			Assert.AreEqual(string.Empty, match.RemainingText);
			Assert.AreEqual(TokenType.Integer, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsInteger4()
		{
			var input = "1\n2\n3\n4";

			var tokenDefinition = new TokenDefinition(TokenType.Integer, IntegerRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("1", match.Value);
			Assert.AreEqual("2\n3\n4", match.RemainingText);
			Assert.AreEqual(TokenType.Integer, match.TokenType);
		}
	}
}
