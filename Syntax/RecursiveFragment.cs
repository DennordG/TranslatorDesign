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
					recursiveNode.AddDepth(-1);
					syntaxNode.AddCopyChildrenFrom(recursiveNode);
					syntaxNode.Children.Remove(recursiveNode);
				}

                while (tokens.Count > tokensCopy.Count)
                {
                    tokens.Pop();
                }
            }
			else if (tokens.Count == tokensCopy.Count)
			{
				syntaxNode.RemoveChild(recursiveNode);
			}

			return foundResult;
        }
    }
}
