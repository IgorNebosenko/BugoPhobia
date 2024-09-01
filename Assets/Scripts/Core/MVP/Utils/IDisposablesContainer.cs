#if !ELECTRUM_CORE
using System;

namespace ElectrumGames.MVP.Utils
{
    internal interface IDisposablesContainer
    {
        void RegisterForDispose(IDisposable disposable);
    }
}
#endif