using System.Linq;
using System.Text.RegularExpressions;

namespace TranslatorDesign.Tokenizer
{
	public class TokenDefinition
	{
		private readonly TokenType _returnsToken;
		private readonly Regex _regex;

		public TokenDefinition(TokenType returnsToken, string regexPattern)
		{
			_returnsToken = returnsToken;
			_regex = new Regex(regexPattern);
		}

		public TokenMatch Match(string input)
		{
			var match = _regex.Match(input);

			if (match.Success)
			{
				string remainingText = string.Empty;

				if (match.Length != input.Length)
				{
					remainingText = input.Substring(match.Length);
				}

				return new TokenMatch
				{
					IsMatch = true,
					RemainingText = remainingText,
					TokenType = _returnsToken,
					Value = match.Value
				};
			}
			else
			{
				return new TokenMatch { IsMatch = false };
			}
		}
	}
}
