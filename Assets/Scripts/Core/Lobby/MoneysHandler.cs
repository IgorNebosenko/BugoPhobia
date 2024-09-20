using System;
using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Lobby
{
    public class MoneysHandler
    {
        private const string MoneysKey = "Moneys";

        private const decimal DifferenceWithPrevious = 52.31745m; 
        private decimal _previousMoneys;

        public event Action<decimal> BalanceUpdated;

        private decimal _money;
        
        public decimal Moneys
        {
            get => PlayerPrefsAes.GetEncryptedDecimal(MoneysKey);
            set 
            {
                if (_previousMoneys != Moneys - DifferenceWithPrevious)
                {
                    value = _previousMoneys + DifferenceWithPrevious;
                }
                
                BalanceUpdated?.Invoke(value);
                PlayerPrefsAes.SetEncryptedDecimal(MoneysKey, value);

                _previousMoneys = Moneys - DifferenceWithPrevious;
            }
        }

        public MoneysHandler()
        {
            _previousMoneys = Moneys - DifferenceWithPrevious;
        }
    }
}