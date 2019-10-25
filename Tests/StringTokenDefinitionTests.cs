using Microsoft.VisualStudio.TestTools.UnitTesting;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	[TestClass]
	public class StringTokenDefinitionTests : AbstractTokenDefinitionTests
	{
		[TestMethod]
		public void TokenDefinition_IsString1()
		{
			var input = "\"asdf\\n\" ";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("\"asdf\\n\"", match.Value);
			Assert.AreEqual(string.Empty, match.RemainingText);
			Assert.AreEqual(TokenType.String, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsString2()
		{
			var input = "\"another\\t\\n\\t\" asdf";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("\"another\\t\\n\\t\"", match.Value);
			Assert.AreEqual("asdf", match.RemainingText);
			Assert.AreEqual(TokenType.String, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsString3()
		{
			var input = "\"another\\\\\" asdf";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("\"another\\\\\"", match.Value);
			Assert.AreEqual("asdf", match.RemainingText);
			Assert.AreEqual(TokenType.String, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsString4()
		{
			var input = "\"\\'\\'\" asdf";

			var tokenDefinition = new TokenDefinition(TokenType.String, StringRegex);

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("\"\\'\\'\"", match.Value);
			Assert.AreEqual("asdf", match.RemainingText);
			Assert.AreEqual(TokenType.String, match.TokenType);
		}
	}
}
