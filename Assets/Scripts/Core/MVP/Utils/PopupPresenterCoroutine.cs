using System.Collections.Generic;

namespace ElectrumGames.MVP.Utils
{
    #region Misc
    
    public interface IPopupPresenterCoroutine<in TArgs, TResult>
        where TArgs : PopupArgs
        where TResult : PopupResult
    {
        IEnumerable<TResult> Init(TArgs args);
    }

    #endregion

    //Need variant for async task...
    
    public abstract class PopupPresenterCoroutine<TView, TArgs, TResult>
        : Presenter<TView>, IPopupPresenterCoroutine<TArgs, TResult>
        where TView : View
        where TArgs : PopupArgs
        where TResult : PopupResult
    {
        protected IEnumerator<TResult> popupEnumerator;

        protected PopupPresenterCoroutine(TView view) : base(view)
        {
            
        }

        public abstract IEnumerable<TResult> Init(TArgs args);
    }
}