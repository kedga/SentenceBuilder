using System.Text;
using GetRandomFromList;

namespace SentenceBuilding;

public class SentenceBuilder
{
	readonly List<SentenceItem> _words = [];
	readonly Random _random = new();
	public List<SentenceItem> Words => _words.Duplicate();
	public SentenceBuilder() { }
	public SentenceBuilder(params string[] words) =>
		_words.AddRange(words.Select(w => w.ToRegularWord()).ToList());
	public SentenceBuilder(List<SentenceItem> words) => _words = words;
	public SentenceBuilder(SentenceBuilder sb) => _words = sb.Words;
	public override string ToString() => this.Build();
}

public static class SentenceBuilderExtensions
{
	public static SentenceBuilder IsPossesive(this SentenceBuilder sb, bool optional = false, int rarity = 5)
	{
		var words = sb.Words;

		if (words.Last() is RegularWord word)
			word.Possessive = new(true, optional, rarity);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder Pluralize(this SentenceBuilder sb, bool optional = false, int rarity = 5)
	{
		var words = sb.Words;

		if (words.Last() is RegularWord word)
			word.Plural = new(true, optional, rarity);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder IsOptional(this SentenceBuilder sb, int rarity = 5)
	{
		var words = sb.Words;

		words.Last().IsOptional = true;
		words.Last().Rarity = rarity;

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder PickOne(this SentenceBuilder sb, params RegularWord[] candidates)
	{
		if (candidates.Length < 1) return new(sb);

		var words = sb.Words;

		words.Add(candidates.PickOne());

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder PickOne(this SentenceBuilder sb, params SentenceBuilder[] candidates)
	{
		if (candidates.Length < 1) return new(sb);

		var words = sb.Words;

		words.AddRange(candidates.PickOne().Words);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder PickOne(this SentenceBuilder sb, params SentenceItem[] candidates)
	{
		if (candidates.Length < 1) return new(sb);

		var words = sb.Words;

		words.Add(candidates.PickOne());

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder PickOne(this SentenceBuilder sb, params Func<SentenceBuilder>[] candidates)
	{
		if (candidates.Length < 1) return new(sb);

		var words = sb.Words;

		var chosen = candidates.PickOne();

		words.AddRange(chosen.Invoke().Words);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder Add(this SentenceBuilder sb, params SentenceItem[] addedWords)
	{
		var words = sb.Words;

		foreach (var word in addedWords)
			words.Add(word);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder Add(this SentenceBuilder sb, params RegularWord[] addedWords)
	{
		var words = sb.Words;

		foreach (var word in addedWords)
			words.Add(word);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder Add(this SentenceBuilder sb, params SentenceBuilder[] sentences)
	{
		if (sentences.Length < 1) return new(sb);

		var words = sb.Words;

		foreach (var sentence in sentences)
			words.AddRange(sentence.Words);

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder AddArticle(this SentenceBuilder sb, ArticleType articleType)
	{
		var words = sb.Words;

		words.Add(new Article(articleType));

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder AddPunctuation(this SentenceBuilder sb, PunctuationType punctuationType)
	{
		var words = sb.Words;

		words.Add(new Punctuation(punctuationType));

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder AddRandom(this SentenceBuilder sb, params IEnumerable<string>[] wordCollections)
	{
		var words = sb.Words;

		var pickedCollection = wordCollections.PickOne();

		words.Add(pickedCollection.Random().ToRegularWord());

		return new SentenceBuilder(words);
	}
	public static SentenceBuilder AddOne(this SentenceBuilder sb, params string[] options)
	{
		var words = sb.Words;

		words.Add(options.PickOne().ToRegularWord());

		return new SentenceBuilder(words);
	}
	public static string Build(this SentenceBuilder sb)
	{
		var words = sb.Words;

		for (int i = words.Count - 1; i >= 0; i--)
		{
			if (words[i].Use() is false) words.RemoveAt(i);
		}

		var strb = new StringBuilder();

		for (int i = 0; i < words.Count; i++)
		{
			var currentItem = words[i];

			var nextItem = i < words.Count - 1 ? words[i + 1] : null;

			if (currentItem is Article article && nextItem is RegularWord nextWord)
			{
				strb.Append(article.ToArticle(nextWord));
			}

			else strb.Append(currentItem);

			if (i < words.Count - 1 && nextItem is not Punctuation) strb.Append(' ');
		}

		return strb.ToString();
	}
}