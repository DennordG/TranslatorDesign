using System;
using System.Collections.Generic;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class Parser
	{
		internal static Dictionary<GrammarType, int> CurrentDepth;
		internal const int MaxDepth = 100;

		static Parser()
		{
			CurrentDepth = new Dictionary<GrammarType, int>
			{
				{ GrammarType.Program, 0 },
				{ GrammarType.VarDecl, 0 },
				{ GrammarType.FnDecl, 0 },
				{ GrammarType.Parameters, 0 },
				{ GrammarType.FormalsList, 0 },
				{ GrammarType.FormalDecl, 0 },
				{ GrammarType.Block, 0 },
				{ GrammarType.DeclList, 0 },
				{ GrammarType.StmtList, 0 },
				{ GrammarType.Stmt, 0 },
				{ GrammarType.Exp, 0 },
				{ GrammarType.Atom, 0 },
				{ GrammarType.FnCallExpr, 0 },
				{ GrammarType.FnCallStmt, 0 },
				{ GrammarType.ActualList, 0 },
				{ GrammarType.SubscriptExpr, 0 },
				{ GrammarType.Type, 0 },
				{ GrammarType.Id, 0 },
			};
		}

		private IDictionary<GrammarType, IEnumerable<IGrammarFragment>> _grammarRules;

		public Parser(IDictionary<GrammarType, IEnumerable<IGrammarFragment>> grammarRules)
		{
			_grammarRules = grammarRules;
		}

		public IList<GrammarType> Parse(IEnumerable<Token> tokens)
		{
			var tokenStack = new Stack<Token>(tokens.Reverse());

			var grammarTypes = new List<GrammarType>();

			while (tokenStack.Count > 0)
			{
				var tokenStackCopy = new Stack<Token>(tokenStack.Reverse());
				var matchingRule = _grammarRules.FirstOrDefault(r => r.Value.Any(g => g.IsValid(tokenStackCopy)));

				if (matchingRule.Equals(Activator.CreateInstance(matchingRule.GetType())))
				{
					grammarTypes = null;
					break;
				}
				else
				{
					while (tokenStack.Count > tokenStackCopy.Count)
					{
						tokenStack.Pop();
					}

					grammarTypes.Add(matchingRule.Key);
				}
			}

			return grammarTypes;
		}
	}
}
