using System.Collections.Generic;
using System.Linq;

namespace TranslatorDesign.Semantic
{
	public class SymbolTable
	{
		private readonly IDictionary<int, IList<string>> _table;

		public SymbolTable()
		{
			_table = new Dictionary<int, IList<string>>();
		}

		public void AddDecl(int depth, string value)
		{
			if (!_table.ContainsKey(depth))
			{
				_table.Add(depth, new List<string>());
			}

			_table[depth].Add(value);
		}

		public bool Exists(int depth, string value)
		{
			var validDepths = _table.Keys.Where(d => d <= depth);

			return validDepths.Any(d => ExistsAtDepth(d, value));
		}

		public bool ExistsAtDepth(int depth, string value)
		{
			return _table.ContainsKey(depth) && _table[depth].Contains(value);
		}

		public void Clear(int depth)
		{
			var removedKeys = _table.Keys.Where(d => d >= depth).ToList();

			removedKeys.ForEach(k => _table.Remove(k));
		}
	}
}