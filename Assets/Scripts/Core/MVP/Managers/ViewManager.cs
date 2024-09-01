using System;
using System.Collections.Generic;
using UnityEngine;

namespace ElectrumGames.MVP.Managers
{
    public class ViewManager : BaseViewManager
    {
        public ViewManager(List<(Type view, Type presenter)> viewPresenterPairs, Transform viewContainer, PresenterFactory factory) :
            base(viewContainer, factory)
        {
            ManualRegisterViews();
            AutoRegisterViews(viewPresenterPairs);
        }

        private void ManualRegisterViews()
        {
        }
    }
}