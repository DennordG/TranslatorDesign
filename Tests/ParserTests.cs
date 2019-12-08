using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TranslatorDesign.Syntax;
using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tests
{
	[TestClass]
    public class ParserTests
    {
		private readonly Tokenizer.Tokenizer _tokenizer = new Tokenizer.Tokenizer(
                new ReservedRegexProvider(),
                new OperatorRegexProvider(),
                new SyntaxOperatorRegexProvider()
            );

        [TestMethod]
        public void Test1()
        {
            string[] inputText 
                = { "int globalInt;",
                "int globalAfterMainX;",
                "int globalAfterMainY;",
                "bool globalBool;",
                "void f(int integer, bool boolean) {",
                "  cout << " + "\"" + "I AM A FUNCTION!\n" + "\"" + ";",
                "  return;",
                "}",
                "int main() {",
                "int x;",
                "x = 5;",
                "f();",
                "cout << x;",
                "return 0;",
                "}"
            };

            var tokens = _tokenizer.Tokenize(inputText);

            Assert.IsFalse(tokens.Any(t => t.TokenType == TokenType.Invalid));

            var parser = new Parser(new Grammar(new GrammarRulesProvider()));
            var couldParse = parser.Parse(tokens, out var _);

            Assert.IsTrue(couldParse);
        }

        [TestMethod]
        public void Test2()
        {
            //Declarations like : int globalInt, globalAfterMainX are not valid
            string[] inputText
                = { "int globalInt, globalAfterMainX;",
                "int globalAfterMainY;",
                "bool globalBool;",
                "void f(int integer, bool boolean) {",
                "  cout << " + "\"" + "I AM A FUNCTION!\n" + "\"" + ";",
                "  return;",
                "}",
                "int main() {",
                "int x;",
                "x = 5;",
                "f();",
                "cout << x;",
                "return 0;",
                "}"
            };

            var tokens = _tokenizer.Tokenize(inputText);

            Assert.IsFalse(tokens.Any(t => t.TokenType == TokenType.Invalid));

            var parser = new Parser(new Grammar(new GrammarRulesProvider()));
            var couldParse = parser.Parse(tokens, out var _);

            Assert.IsFalse(couldParse);
        }

        [TestMethod]
        public void Test3()
        {
            string[] inputText
                = { "int globalInt;",
                "int globalAfterMainY;",
                "bool globalBool;",
                "int main() {",
                "int x;",
                "cin >> x >> globalAfterMainY >> globalBool;",
                "x = (4 + (5 * 20)) /2 + (3 * (13-3) + 7 * 2);",
                "cout << x;",
                "globalBool = (globalAfterMainY == x);",
                "cout << x << globalAfterMainY << globalBool;",
                "return 0;",
                "}"
            };

            var tokens = _tokenizer.Tokenize(inputText);

            Assert.IsFalse(tokens.Any(t => t.TokenType == TokenType.Invalid));

            var parser = new Parser(new Grammar(new GrammarRulesProvider()));
            var couldParse = parser.Parse(tokens, out var _);

            Assert.IsFalse(couldParse);
        }

        [TestMethod]
        public void Test4()
        {
            string[] inputText
                = { "int globalInt;",
                "int globalAfterMainY;",
                "bool globalBool;",
                "int main() {",
                "int x;",
                "cin >> globalBool;",
                "x = (4 + (5 * 20)) /2 + (3 * (13-3) + 7 * 2);",
                "cout << x;",
                "globalBool = (globalAfterMainY == x);",
                "cout << globalBool;",
                "return 0;",
                "}"
            };

            var tokens = _tokenizer.Tokenize(inputText);

            Assert.IsFalse(tokens.Any(t => t.TokenType == TokenType.Invalid));

            var parser = new Parser(new Grammar(new GrammarRulesProvider()));
            var couldParse = parser.Parse(tokens, out var _);

            Assert.IsTrue(couldParse);
        }
    }
}
