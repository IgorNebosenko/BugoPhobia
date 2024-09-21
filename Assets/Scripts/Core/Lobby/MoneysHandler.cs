using System;
using ElectrumGames.Core.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ElectrumGames.Core.Lobby
{
    public class MoneysHandler
    {
        private const string MoneysKey = "Moneys";

        private readonly decimal _differenceWithPrevious; 
        private decimal _previousMoneys;

        public event Action<decimal> BalanceUpdated;

        private decimal _money;
        
        public decimal Moneys
        {
            get => PlayerPrefsAes.GetEncryptedDecimal(MoneysKey);
            set 
            {
                if (_previousMoneys != Moneys - _differenceWithPrevious)
                {
                    value = _previousMoneys + _differenceWithPrevious;
                }
                
                BalanceUpdated?.Invoke(value);
                PlayerPrefsAes.SetEncryptedDecimal(MoneysKey, value);

                _previousMoneys = Moneys - _differenceWithPrevious;
            }
        }

        public MoneysHandler()
        {
            _differenceWithPrevious = (decimal)Random.Range(-1000f, 1000f);
            _previousMoneys = Moneys - _differenceWithPrevious;
        }
    }
}