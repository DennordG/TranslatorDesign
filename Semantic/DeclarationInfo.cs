namespace TranslatorDesign.Semantic
{
	public class DeclarationInfo
	{
		public string Type { get; }

		public string Id { get; }

		public DeclarationInfo(string type, string id)
		{
			Type = type;
			Id = id;
		}
	}
}