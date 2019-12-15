using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
    [DebuggerDisplay("{" + nameof(_grammarType) + "}")]
    public class RecursiveFragment : IGrammarFragment
    {
        private readonly GrammarType _grammarType;
        private readonly Func<IEnumerable<IGrammarFragment>> _grammarFragmentsProvider;
		private readonly bool _addNewNode;

		public RecursiveFragment(GrammarType grammarType, Func<IEnumerable<IGrammarFragment>> action, bool addNewNode = true)
        {
            _grammarType = grammarType;
            _grammarFragmentsProvider = action;
			_addNewNode = addNewNode;
		}

        public bool Validate(Stack<Token> tokens, SyntaxNode syntaxNode)
		{
			var tokensCopy = new Stack<Token>(tokens.Reverse());
            var foundResult = false;

			var recursiveNode = new SyntaxNode(_grammarType);
			syntaxNode.AddChild(recursiveNode);

			foreach (var fragment in _grammarFragmentsProvider())
            {
                if (!fragment.Validate(tokensCopy, recursiveNode))
				{
					recursiveNode.ClearChildren();
					tokensCopy = new Stack<Token>(tokens.Reverse());
                }
                else
                {
                    foundResult = true;
                    break;
                }
            }

            if (foundResult && tokens.Count != tokensCopy.Count)
			{
				if (!_addNewNode)
				{
					syntaxNode.CopyChildrenAndRemove(recursiveNode);
				}

				if (_grammarType == GrammarType.ComparisonOp)
				{
					var prevExp = syntaxNode.Parent.Parent.Children.First();
					if (prevExp.Children.Any(c => c.GrammarType == GrammarType.ComparisonOp))
					{
						syntaxNode.RemoveChild(recursiveNode);
						return false;
					}
				}

				while (tokens.Count > tokensCopy.Count)
                {
                    tokens.Pop();
                }
            }
			else if (!foundResult)
			{
				syntaxNode.RemoveChild(recursiveNode);
			}

			return foundResult;
        }
    }
}
