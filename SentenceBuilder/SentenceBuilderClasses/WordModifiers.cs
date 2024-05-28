using System.Text.RegularExpressions;
using GetRnd = GetRandomFromList.GetRandomFromList;

namespace SentenceBuilding;

public static partial class WordModifiers
{
	public static string GetArticle(this string word, ArticleType articleType, bool optional = false)
	{
		var result = articleType switch
		{
			ArticleType.Definite => GetDefiniteArticle(),
			ArticleType.Indefinite => GetIndefiniteArticle(word),
			ArticleType.Random => GetRandomArticle(word),
			_ => string.Empty,
		};

		if (optional) return GetRnd.PickOne(result, string.Empty);

		return result;
	}
	public static string GetRandomArticle(string word)
	{
		return GetRnd.PickOne(GetDefiniteArticle(), GetIndefiniteArticle(word));
	}
	public static string GetIndefiniteArticle(string word)
	{
		if (word.Length < 1) return string.Empty;

		char firstLetter = char.ToLower(word[0]);

		string result;

		var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

		if (vowels.Contains(firstLetter))
		{
			result = "an";
		}
		else
		{
			result = "a";
		}

		return result;
	}
	public static string GetDefiniteArticle()
	{
		return "the";
	}
	public static string CapitalizeFirst(this string input)
	{
		if (string.IsNullOrEmpty(input)) return input;

		return char.ToUpper(input[0]) + input.Substring(1);
	}
	public static string CapitalizeAll(this string input)
	{
		if (string.IsNullOrEmpty(input)) return input;

		return input.ToUpper();
	}
	public static string Pluralize(this string input)
	{
		if (string.IsNullOrEmpty(input)) return input;

		List<string> esCases = ["s", "x", "z", "ch", "sh", "ss"];

		if (esCases.Any(input.EndsWith))
		{
			return input + "es";
		}

		Regex iesCases = IesRegExCheck();

		if (iesCases.IsMatch(input))
		{
			return string.Concat(input.AsSpan(0, input.Length - 1), "ies");
		}

		return input + "s";
	}

	[GeneratedRegex("[^aeiou]y$", RegexOptions.IgnoreCase, "en-SE")]
	private static partial Regex IesRegExCheck();
}