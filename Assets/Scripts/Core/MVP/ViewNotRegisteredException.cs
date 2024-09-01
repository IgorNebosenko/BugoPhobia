using System;

namespace ElectrumGames.MVP
{
    public class ViewNotRegisteredException : Exception
    {
        public ViewNotRegisteredException(Type presenterType, AbstractViewManager viewManager) 
            : base($"[ViewNotRegisteredException] No view registered of type {presenterType} in {viewManager}.")
        {
            
        }        
    }
}