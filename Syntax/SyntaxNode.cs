using System;
using System.Collections.Generic;
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

		public SyntaxNode(GrammarType? grammarType, string value)
			: this()
		{
			GrammarType = grammarType;
			Value = value;
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
            node.Parent = this;

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

        public void CopyChildrenAndRemove(SyntaxNode node)
        {
            foreach (var child in node.Children)
            {
                child.AdjustDepth(Depth + 1);
                child.Parent = this;

                Children.Add(child);
            }

            Children.Remove(node);
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

        private void AdjustDepth(int currentDepth)
        {
            Depth = currentDepth;

            foreach (var child in Children)
            {
                child.AdjustDepth(currentDepth + 1);
            }
        }
        #endregion
    }
}
