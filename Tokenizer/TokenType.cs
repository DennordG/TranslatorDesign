namespace TranslatorDesign.Tokenizer
{
	public enum TokenType
	{
		Invalid,
		Reserved,
		SequenceTerminator,
		Integer,
        String,
        ArithmeticAndLogicOperator,
        SyntaxOperator,
		Identifier,
	}
}
