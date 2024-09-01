#if !ELECTRUM_CORE
using System.Collections.Generic;

namespace ElectrumGames.MVP.Utils
{
    internal static class EnumeratorUtil
    {
        public static IEnumerator<T> Single<T>(T value)
        {
            while (true)
                yield return value;
        }
    }
}
#endif