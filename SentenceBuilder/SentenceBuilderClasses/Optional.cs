namespace SentenceBuilding;

public abstract class Optional()
{
	public bool IsOptional { get; set; } = false;
	public int Rarity { get; set; } = 5;
}
public static class OptionalExtensions
{
	readonly static Random _random = new();
	public static bool Use(this Optional optional)
	{
		if (optional.IsOptional is false) return true;

		return _random.Next(1, 11) > optional.Rarity;
	}
}