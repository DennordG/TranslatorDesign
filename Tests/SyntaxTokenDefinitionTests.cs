using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tests
{
    [TestClass]
    public class SyntaxTokenDefinitionTests : AbstractTokenDefinitionTests
    {
        private readonly Tokenizer.Tokenizer _tokenizer = new Tokenizer.Tokenizer(new ReservedRegexProvider(), new OperatorRegexProvider(), new SyntaxOperatorRegexProvider());

        [TestMethod]
        public void TokenDefinition_IsOperator1()
        {
            string[] input =
			{
                "( a + b ) / c"
            };

            var tokensGenerated = _tokenizer.Tokenize(input).ToList();

            var operatorTokens = tokensGenerated.Where(t => t.TokenType == TokenType.SyntaxOperator).ToList();

            Assert.IsTrue(operatorTokens.Any());
            Assert.AreEqual(2, operatorTokens.Count);
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "("));
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == ")"));
        }

        [TestMethod]
        public void TokenDefinition_IsOperator2()
        {
            string[] input =
			{
                "{ 3 }"
            };

            var tokensGenerated = _tokenizer.Tokenize(input).ToList();

            var operatorTokens = tokensGenerated.Where(t => t.TokenType == TokenType.SyntaxOperator).ToList();

            Assert.IsTrue(operatorTokens.Any());
            Assert.AreEqual(2, operatorTokens.Count);
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "{"));
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "}"));
        }

        [TestMethod]
        public void TokenDefinition_IsOperator3()
        {
            string[] input =
			{
                "v [4]",
                "x = 5"
            };

            var tokensGenerated = _tokenizer.Tokenize(input).ToList();

            var operatorTokens = tokensGenerated.Where(t => t.TokenType == TokenType.SyntaxOperator).ToList();

            Assert.IsTrue(operatorTokens.Any());
            Assert.AreEqual(3, operatorTokens.Count);
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "["));
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "]"));
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "="));
        }
    }
}

