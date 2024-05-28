namespace SentenceBuilding;

public class Article(ArticleType articleType) : SentenceItem
{
	public ArticleType ArticleType { get; } = articleType;
	public string ToArticle(string nextWord) => nextWord.GetArticle(ArticleType);
	public override string ToString() => $"[{ArticleType} article]";
}
public enum ArticleType
{
	Definite,
	Indefinite,
	Random
}
