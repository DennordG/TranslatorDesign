using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TranslatorDesign.Tokenizer.RegexProvider;

namespace TranslatorDesign.Tokenizer
{
	public class Tokenizer
	{
		private readonly List<TokenDefinition> _tokenDefinitions;

		private const string StringPattern = "\"{1}(?:(?:[^\"\\\\]|(?:\\\\[tn\"'\\\\]))+)\"{1}";
		private const string IdentifierPattern = @"(?:(?:^_+[a-zA-Z\d]\w*)|(?:^[a-zA-Z]\w*))";
		private const string IntegerPattern = @"\d+";

		public Tokenizer(ReservedRegexProvider reservedProvider, OperatorRegexProvider operatorProvider, SyntaxOperatorRegexProvider syntaxProvider)
		{
			_tokenDefinitions = new List<TokenDefinition>
			{
				new TokenDefinition(TokenType.Reserved, reservedProvider.GetPattern()),
				new TokenDefinition(TokenType.Identifier, IdentifierPattern),
				new TokenDefinition(TokenType.Integer, RegexWrapper.DefaultWrap(IntegerPattern)),
				new TokenDefinition(TokenType.String, RegexWrapper.DefaultWrap(StringPattern)),
				new TokenDefinition(TokenType.ArithmeticAndLogicOperator, operatorProvider.GetPattern()),
				new TokenDefinition(TokenType.SyntaxOperator, syntaxProvider.GetPattern()),
			};
		}

		public IList<Token> Tokenize(string[] text)
		{
            var tokens = new List<Token>();

            foreach (string line in text)
            {
                var remainingText = line;

                while (!string.IsNullOrEmpty(remainingText))
                {
                    var match = FindMatch(remainingText);
                    if (match.IsMatch)
                    {
                        tokens.Add(new Token(match.TokenType, match.Value));
                        remainingText = match.RemainingText;
                    }
                    else if (IsWhiteSpace(remainingText))
                    {
                        remainingText = remainingText.Substring(1);
                    }
                    else if (IsComment(remainingText))
                    {
                        remainingText = "";
                    }
                    else
                    {
                        var invalidTokenMatch = CreateInvalidTokenMatch(remainingText);
                        tokens.Add(new Token(invalidTokenMatch.TokenType, invalidTokenMatch.Value));
                        remainingText = invalidTokenMatch.RemainingText;
                    }
                }

                tokens.Add(new Token(TokenType.SequenceTerminator, string.Empty));
            }

			return tokens;
		}

		private TokenMatch CreateInvalidTokenMatch(string text)
		{
			var match = Regex.Match(text, "(^\\S+\\s)|^\\S+");
			if (match.Success)
			{
				return new TokenMatch()
				{
					IsMatch = true,
					RemainingText = text.Substring(match.Length),
					TokenType = TokenType.Invalid,
					Value = match.Value.Trim()
				};
			}

			throw new Exception("Failed to generate invalid token");
		}

		private bool IsWhiteSpace(string text)
		{
			return Regex.IsMatch(text, "^\\s+");
		}

        private bool IsComment(string text)
        {
            return Regex.IsMatch(text, "^#|^//");
        }

        private TokenMatch FindMatch(string text)
		{
			foreach (var tokenDefinition in _tokenDefinitions)
			{
				var match = tokenDefinition.Match(text);
				if (match.IsMatch)
				{
					return match;
				}
			}

			return new TokenMatch { IsMatch = false };
		}
	}
}
