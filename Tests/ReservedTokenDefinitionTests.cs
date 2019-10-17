using Microsoft.VisualStudio.TestTools.UnitTesting;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Tests
{
	[TestClass]
	public class ReservedTokenDefinitionTests : AbstractTokenDefinitionTests
	{
		[TestMethod]
		public void TokenDefinition_IsReserved_Integer()
		{
			var input = "int a=32;";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("int", match.Value);
			Assert.AreEqual(" a=32;", match.RemainingText);
			Assert.AreEqual(TokenType.Reserved, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsReserved_Bool()
		{
			var input = "bool a=true;";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("bool", match.Value);
			Assert.AreEqual(" a=true;", match.RemainingText);
			Assert.AreEqual(TokenType.Reserved, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsReserved_Void()
		{
			var input = "void f(int x)";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("void", match.Value);
			Assert.AreEqual(" f(int x)", match.RemainingText);
			Assert.AreEqual(TokenType.Reserved, match.TokenType);
		}

		[TestMethod]
		public void TokenDefinition_IsReserved_ReturnWithNewline()
		{
			var input = "return\nasdf";

			var tokenDefinition = new TokenDefinition(TokenType.Reserved, ReservedProvider.GetPattern());

			var match = tokenDefinition.Match(input);

			Assert.IsTrue(match.IsMatch);
			Assert.AreEqual("return", match.Value);
			Assert.AreEqual("\nasdf", match.RemainingText);
			Assert.AreEqual(TokenType.Reserved, match.TokenType);
		}
	}
}
