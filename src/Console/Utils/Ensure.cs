namespace ProjectScaffold.Utils;

public static class Ensure
{
    public static void EqualCount<T, E>(IEnumerable<T> first, IEnumerable<E> second)
    {
        if (first.Count() != second.Count())
        {
            throw new ArgumentException(
                "The two collections must have the same number of elements."
            );
        }
    }
}
