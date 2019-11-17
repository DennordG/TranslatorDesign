using System;
using System.Collections.Generic;
using System.Text;
using TranslatorDesign.Helpers;
using TranslatorDesign.Tokenizer;

namespace TranslatorDesign.Syntax
{
	public class SyntaxNode
	{
		public IList<SyntaxNode> Children { get; private set; }

		public GrammarType? GrammarType;
		public TokenType? TokenType;
		public string Value;

		public int Depth = 0;


		#region ctor
		private SyntaxNode()
		{
			Children = new List<SyntaxNode>();
		}

		public SyntaxNode(GrammarType grammarType)
			: this()
		{
			GrammarType = grammarType;
		}

		public SyntaxNode(TokenType tokenType)
			: this()
		{
			TokenType = tokenType;
		}

		public SyntaxNode(string value)
			: this()
		{
			Value = value;
		}
		#endregion


		#region Children operations
		public void AddChild(SyntaxNode node)
		{
			node.Depth = Depth + 1;
			Children.Add(node);
		}

		public void ClearChildren()
		{
			foreach (var child in Children)
			{
				child.ClearChildren();
			}

			Children.Clear();
		}

		public void RemoveChild(SyntaxNode node)
		{
			node.ClearChildren();

			Children.Remove(node);
		}

		public void AddChildrenFrom(SyntaxNode node)
		{
			foreach (var child in node.Children)
			{
				Children.Add(child);
			}
		}
		#endregion


		#region Utils
		public void Print()
		{
			Console.WriteLine(LeftSpace(Depth) + ToString());
			foreach (var child in Children)
			{
				child.Print();
			}
		}

		private string LeftSpace(int depth)
		{
			var sb = new StringBuilder();
			const string spaceAdd = "  ";

			while (depth-- > 0)
			{
				sb.Append(spaceAdd);
			}

			return sb.ToString();
		}

		public override string ToString()
		{
			return $"{StringRepr()}";
		}

		private string StringRepr()
		{
			return Value ?? GrammarType?.ToString() ?? TokenType?.ToString();
		}

		public SyntaxNode ShallowCopy()
		{
			return (SyntaxNode)MemberwiseClone();
		}

		public void SwapNodeValues(SyntaxNode node)
		{
			Utils.Swap(ref Value, ref node.Value);
			Utils.Swap(ref TokenType, ref node.TokenType);
			Utils.Swap(ref GrammarType, ref node.GrammarType);
		}

		public void AddDepth(int add)
		{
			foreach (var child in Children)
			{
				child.AddDepth(add);
			}

			Depth += add;
		}
		#endregion
	}
}
