using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TranslatorDesign.Syntax
{
	public class SyntaxNode
	{
		public IList<SyntaxNode> Children { get; }

		public GrammarType? GrammarType;
		public readonly string Value;

		public int Depth;

		public SyntaxNode Parent { get; private set; }

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
			node.Parent = this;
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

		public void AddCopyChildrenFrom(SyntaxNode node)
		{
			foreach (var child in node.Children)
			{
				Children.Add(child.ShallowCopy());
				Children.Last().Parent = this;
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
			return StringRepr();
		}

		private string StringRepr()
		{
			return Value ?? GrammarType?.ToString();
		}

		public void AddDepth(int add)
		{
			foreach (var child in Children)
			{
				child.AddDepth(add);
			}

			Depth += add;
		}

		private SyntaxNode ShallowCopy()
		{
			return (SyntaxNode)MemberwiseClone();
		}
		#endregion
	}
}
