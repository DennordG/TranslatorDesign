using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorDesign.Helpers;
using TranslatorDesign.Semantic;
using TranslatorDesign.Syntax;
using TranslatorDesign.Tokenizer;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tests
{
	[TestClass]
	public class SemanticTests
	{
		public IList<Token> CreateTokens(string[] inputFile)
		{
			var tokenizer = new Tokenizer.Tokenizer(
				new ReservedRegexProvider(),
				new OperatorRegexProvider(),
				new SyntaxOperatorRegexProvider()
			);

			var tokens = tokenizer.Tokenize(inputFile);

			if (tokens.Any(t => t.TokenType == TokenType.Invalid))
			{
				return null;
			}

			return tokens;
		}

		[TestMethod]
		public void Test1()
		{
			string[] inputText = {
				"int main() {",
				"int x;",
				"x = 5;",
				"f();",
				"cout << x;",
				"return 0;",
				"}"
			};

			var tokens = CreateTokens(inputText);
			var parser = new Parser(new Grammar(new GrammarRulesProvider()));
			var couldParse = parser.Parse(tokens, out var syntaxTree);

			Assert.IsTrue(couldParse);

			var semanticAnalyzer = new SemanticAnalyzer();

			Assert.ThrowsException<AggregateException>(() => semanticAnalyzer.ValidateAndThrow(syntaxTree));
		}

		[TestMethod]
		public void Test2()
		{
			string[] inputText = {
				"void f(){",
				"cout << 1;",
				"}",
				"int main() {",
				"int x;",
				"x = 5;",
				"f();",
				"cout << x;",
				"return 0;",
				"}"
			};

			var tokens = CreateTokens(inputText);
			var parser = new Parser(new Grammar(new GrammarRulesProvider()));
			var couldParse = parser.Parse(tokens, out var syntaxTree);

			Assert.IsTrue(couldParse);

			var semanticAnalyzer = new SemanticAnalyzer();

			semanticAnalyzer.ValidateAndThrow(syntaxTree);
		}

		[TestMethod]
		public void Test3()
		{
			string[] inputText = {
				"int main() {",
				"int x;",
				"int x;",
				"x = 5;",
				"cout << x;",
				"return 0;",
				"}"
			};

			var tokens = CreateTokens(inputText);
			var parser = new Parser(new Grammar(new GrammarRulesProvider()));
			var couldParse = parser.Parse(tokens, out var syntaxTree);

			Assert.IsTrue(couldParse);

			var semanticAnalyzer = new SemanticAnalyzer();

			Assert.ThrowsException<AggregateException>(() => semanticAnalyzer.ValidateAndThrow(syntaxTree));
		}
	}
}
