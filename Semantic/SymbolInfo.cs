using System.Collections.Generic;

namespace TranslatorDesign.Semantic
{
	public class SymbolInfo
	{
		public DeclarationInfo Declaration { get; }

		public IList<DeclarationInfo> Parameters { get; }

		public bool IsFunctionType => Parameters != null;

		public SymbolInfo(DeclarationInfo declInfo, IList<DeclarationInfo> parameters)
		{
			Declaration = declInfo;
			Parameters = parameters;
		}
	}
}