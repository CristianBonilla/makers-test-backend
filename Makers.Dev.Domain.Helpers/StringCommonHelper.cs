namespace Makers.Dev.Domain.Helpers;

public class StringCommonHelper
{
  public static bool IsEquivalent(string sourceA, string sourceB, StringComparison comparison) => string.Compare(sourceA, sourceB, comparison) == 0;

  public static bool IsEquivalent(string sourceA, string sourceB) => IsEquivalent(sourceA, sourceB, StringComparison.OrdinalIgnoreCase);
}
