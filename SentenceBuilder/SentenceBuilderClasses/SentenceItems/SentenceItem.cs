namespace SentenceBuilding;

public abstract class SentenceItem : Optional { }
public static class SentenceItemExtensions
{
	public static List<SentenceItem> Duplicate(this List<SentenceItem> pieces) =>
		pieces.Select(x => x.Duplicate()).ToList();

	public static SentenceItem Duplicate(this SentenceItem item) => item switch
	{
		Article article => new Article(article.ArticleType)
		{
			IsOptional = article.IsOptional,
			Rarity = article.Rarity
		},
		RegularWord word => new RegularWord(word.Word)
		{
			IsOptional = word.IsOptional,
			Rarity = word.Rarity,
			Plural = word.Plural,
			Possessive = word.Possessive
		},
		Punctuation punctuation => new Punctuation(punctuation.PunctuationType)
		{
			IsOptional = punctuation.IsOptional,
			Rarity = punctuation.Rarity
		},
		_ => new RegularWord($"Attempted duplication of unknown SentenceItem type: {item.GetType().Name}"),
	};
}