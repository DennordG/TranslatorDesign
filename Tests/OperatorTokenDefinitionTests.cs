using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tests
{
    [TestClass]
    public class OperatorTokenDefinitionTests : AbstractTokenDefinitionTests
    {
        private readonly Tokenizer.Tokenizer _tokenizer = new Tokenizer.Tokenizer(new ReservedRegexProvider(), new OperatorRegexProvider(), new SyntaxOperatorRegexProvider());

        [TestMethod]
        public void TokenDefinition_IsOperator_Logic()
        {
            string[] input = 
			{
                "! true",
                "1 << 7"
            };

            var tokensGenerated = _tokenizer.Tokenize(input).ToList();

            var operatorTokens = tokensGenerated.Where(t => t.TokenType == TokenType.ArithmeticAndLogicOperator).ToList();

            Assert.IsTrue(operatorTokens.Any());
            Assert.AreEqual(2, operatorTokens.Count);
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "!"));
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "<<"));
        }

        [TestMethod]
        public void TokenDefinition_IsOperator_Arithmetic()
        {
            string[] input =
			{
                "2 * 5",
                "=="
            };

            var tokensGenerated = _tokenizer.Tokenize(input).ToList();

            var operatorTokens = tokensGenerated.Where(t => t.TokenType == TokenType.ArithmeticAndLogicOperator).ToList();

            Assert.IsTrue(operatorTokens.Any());
            Assert.AreEqual(2, operatorTokens.Count);
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "*"));
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "=="));
        }

        [TestMethod]
        public void TokenDefinition_IsOperator_Comparison()
        {
            string[] input =
			{
                "4 >= 1",
                "<= !"
            };

            var tokensGenerated = _tokenizer.Tokenize(input).ToList();

            var operatorTokens = tokensGenerated.Where(t => t.TokenType == TokenType.ArithmeticAndLogicOperator).ToList();

            Assert.IsTrue(operatorTokens.Any());
            Assert.AreEqual(3, operatorTokens.Count);
            Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == ">="));
			Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "<="));
			Assert.IsNotNull(operatorTokens.FirstOrDefault(t => t.Value == "!"));
        }

    }
}
