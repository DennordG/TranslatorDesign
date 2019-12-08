using System.Collections;
using System.Collections.Generic;

namespace TranslatorDesign.Syntax
{
	public class SyntaxTree
	{
		public SyntaxNode Root { get; }

		public SyntaxTree(SyntaxNode root)
		{
			Root = root;
		}

		public void Print()
		{
			Root.Print();
		}

		public IEnumerable<SyntaxNode> Traverse()
		{
			return TraverseInternal(Root);
		}

		private IEnumerable<SyntaxNode> TraverseInternal(SyntaxNode root)
		{
			yield return root;

			foreach (var child in root.Children)
			{
				foreach (var childNode in TraverseInternal(child))
				{
					yield return childNode;
				}
			}
		}
	}
}
