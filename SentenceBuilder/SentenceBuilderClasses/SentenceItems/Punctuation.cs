namespace SentenceBuilding;

public class Punctuation(PunctuationType punctuationType) : SentenceItem
{
	public PunctuationType PunctuationType { get; } = punctuationType;
	public override string ToString() => this.GetPunctuation();
}
public enum PunctuationType
{
	Period,
	Comma,
	Apostrophe,
	QuestionMark,
	Colon,
	Semicolon,
	ExclamationMark
}
public static class PunctuationTypeExtensions
{
	public static string GetPunctuation(this Punctuation punctuation) => punctuation.PunctuationType switch
	{
		PunctuationType.Period => ".",
		PunctuationType.Comma => ",",
		PunctuationType.Apostrophe => "'",
		PunctuationType.QuestionMark => "?",
		PunctuationType.Colon => ":",
		PunctuationType.Semicolon => ";",
		PunctuationType.ExclamationMark => "!",
		_ => throw new NotImplementedException()
	};
}