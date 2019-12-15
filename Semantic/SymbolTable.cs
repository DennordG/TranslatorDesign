using System.Collections.Generic;
using System.Linq;

namespace TranslatorDesign.Semantic
{
	public class SymbolTable
	{
		private readonly IDictionary<int, IList<SymbolInfo>> _table;

		public SymbolTable()
		{
			_table = new Dictionary<int, IList<SymbolInfo>>();
		}

		public void AddDecl(int depth, DeclarationInfo declInfo, IList<DeclarationInfo> parameters)
		{
			if (!_table.ContainsKey(depth))
			{
				_table.Add(depth, new List<SymbolInfo>());
			}

			_table[depth].Add(new SymbolInfo(declInfo, parameters));
		}

		public SymbolInfo GetDeclById(int depth, string value)
		{
			var validDepths = _table.Keys.Where(d => d <= depth).ToList();

			return validDepths.Select(d => GetDeclByIdAtDepth(d, value)).FirstOrDefault(declInfo => declInfo != null);
		}

		public SymbolInfo GetDeclByIdAtDepth(int depth, string id)
		{
			return _table.ContainsKey(depth) ? _table[depth].FirstOrDefault(s => s.Declaration.Id == id) : null;
		}

		public void Clear(int depth)
		{
			var removedKeys = _table.Keys.Where(d => d >= depth).ToList();

			removedKeys.ForEach(k => _table.Remove(k));
		}
	}
}