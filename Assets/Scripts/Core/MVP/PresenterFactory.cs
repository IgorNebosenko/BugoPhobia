using Zenject;

namespace ElectrumGames.MVP
{
    public class PresenterFactory
    {
        private DiContainer _container;

        public PresenterFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : Presenter
        {
            return _container.Instantiate<T>();
        }
    }
}