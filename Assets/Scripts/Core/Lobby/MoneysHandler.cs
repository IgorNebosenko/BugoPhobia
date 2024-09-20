using System;
using ElectrumGames.Core.Common;

namespace ElectrumGames.Core.Lobby
{
    public class MoneysHandler
    {
        private const string MoneysKey = "Moneys";

        private const decimal DifferenceWithPrevious = 52.31745m; 
        private decimal _previousMoneys;

        public event Action<decimal> BalanceUpdated; 
        public decimal Moneys
        {
            get => PlayerPrefsAes.GetEncryptedDecimal(MoneysKey);
            set 
            {
                if (Moneys - _previousMoneys != DifferenceWithPrevious)
                {
                    Moneys = _previousMoneys + DifferenceWithPrevious;
                }
                BalanceUpdated?.Invoke(value);
                PlayerPrefsAes.SetEncryptedDecimal(MoneysKey, value);
            }
        }

        public MoneysHandler()
        {
            _previousMoneys = Moneys - DifferenceWithPrevious;
        }
    }
}
