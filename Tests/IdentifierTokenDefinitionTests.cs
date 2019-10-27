using Microsoft.VisualStudio.TestTools.UnitTesting;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	[TestClass]
	public class IdentifierTokenDefinitionTests : AbstractTokenDefinitionTests
	{
		[TestMethod]
		public void TokenDefinition_IsString1()
		{
			var input = "_asdf123 ";

			var tokenDefinition = new TokenDefinition(TokenType.Identifier, IdentifierRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("_asdf123", match.Value);
			Assert.AreEqual(string.Empty, match.RemainingText);
			Assert.AreEqual(TokenType.Identifier, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsString2()
		{
			var input = "__asdf\n";

			var tokenDefinition = new TokenDefinition(TokenType.Identifier, IdentifierRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("__asdf", match.Value);
			Assert.AreEqual(string.Empty, match.RemainingText);
			Assert.AreEqual(TokenType.Identifier, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsString3()
		{
			var input = "_asdf123_123\t";

			var tokenDefinition = new TokenDefinition(TokenType.Identifier, IdentifierRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("_asdf123_123", match.Value);
			Assert.AreEqual(string.Empty, match.RemainingText);
			Assert.AreEqual(TokenType.Identifier, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsString4()
		{
			var input = "_123_asdf ";

			var tokenDefinition = new TokenDefinition(TokenType.Identifier, IdentifierRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("_123_asdf", match.Value);
			Assert.AreEqual(string.Empty, match.RemainingText);
			Assert.AreEqual(TokenType.Identifier, match.TokenType);
		}
	}
}
