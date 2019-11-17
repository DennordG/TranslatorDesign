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
	}
}
