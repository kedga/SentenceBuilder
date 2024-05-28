using System.Reflection;

namespace SentenceBuilding;

public abstract class SentencePattern
{
	public List<Func<string>> GetPatterns()
	{
		var type = GetType();

		var methods = type
			.GetMethods(BindingFlags.Public | BindingFlags.Static)
			.Where(m => m.Name.Contains("Pattern") && m.ReturnType == typeof(string))
			.ToArray();

		return methods
			.Select(m => (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), m))
			.ToList();
	}
}
