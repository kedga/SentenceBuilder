using System.Text;

namespace SentenceBuilding;

public class RegularWord(string word) : SentenceItem
{
	public RegularWord() : this(string.Empty) { }
	public string Word { get; set; } = word;
	public OptionalOption<bool> Plural { get; set; } = false;
	public OptionalOption<bool> Possessive { get; set; } = false;
	public override string ToString()
	{
		var sb = new StringBuilder();

		if (Plural && Plural.Use()) sb.Append(Word.Pluralize());
		else sb.Append(Word);

		if (Possessive && Possessive.Use()) sb.Append(@"'s");

		return sb.ToString();
	}

	public static implicit operator RegularWord(string word) => new(word);

	public static implicit operator string(RegularWord word) => word.ToString();
}
public static class RegularWordExtensions
{
	public static RegularWord ToRegularWord(this string word) => new(word);
}