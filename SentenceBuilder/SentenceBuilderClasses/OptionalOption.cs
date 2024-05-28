namespace SentenceBuilding;

public class OptionalOption<T>(T value) : Optional
{
	public OptionalOption(T value, bool isOptional, int rarity) : this(value)
	{
		IsOptional = isOptional;
		Rarity = rarity;
	}
	public T Value { get; set; } = value;

	public static implicit operator OptionalOption<T>(T value) => new(value);

	public static implicit operator T(OptionalOption<T> optionalOption) => optionalOption.Value;
}