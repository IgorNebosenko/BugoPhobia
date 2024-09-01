using System;
using System.Collections.Generic;
using System.Reflection;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Player.Journal;
using ElectrumGames.MVP;
using ElectrumGames.MVP.Managers;
using ElectrumGames.MVP.Utils;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Injection
{
    public abstract class BaseSceneInstaller : MonoInstaller<BaseSceneInstaller>
    {
        [SerializeField] private Transform viewContainer;
        [SerializeField] private Transform popupContainer;

        protected abstract Assembly UiAssembly { get; }

        public override void InstallBindings()
        {
            #region UI
            var presenterFactory = new PresenterFactory(Container);
            var allViews = AutoViewInstall();
            var viewManager = new ViewManager(allViews.views, viewContainer, presenterFactory);
            var popupManager = new PopupManager(allViews.popups, popupContainer, presenterFactory);

            Container.Bind<PresenterFactory>().FromInstance(presenterFactory).AsSingle();
            Container.BindInstance(viewManager).AsSingle();
            Container.BindInstance(popupManager).AsSingle();
            #endregion

            Container.Bind<NetIdFactory>().AsSingle();
        }

        protected (List<(Type view, Type presenter)> views, List<(Type view, Type presenter)> popups) AutoViewInstall()
        {
            MethodInfo bindWrapper =
                typeof(BaseSceneInstaller).GetMethod("BindWrapper", BindingFlags.Instance | BindingFlags.NonPublic);

            var viewRegistrationList = new List<(Type, Type)>();
            var popupRegistrationList = new List<(Type, Type)>();

            var bindings = AutoRegisterViewAttribute.GetViews(new[] {UiAssembly});

            foreach (var binding in bindings)
            {
                Type type = binding.view;
                Type presenterType = type.BaseType.GetGenericArguments()[0];

                MethodInfo bindingMethod = bindWrapper.MakeGenericMethod(type, presenterType);
                bindingMethod.Invoke(this, new object[] {binding.path});


                if (!CheckForPopup())
                    viewRegistrationList.Add((type, presenterType));
                else
                    popupRegistrationList.Add((type, presenterType));

                bool CheckForPopup()
                {
                    Type tType = presenterType;

                    do
                    {
                        if (tType.IsGenericType &&
                            tType.GetGenericTypeDefinition() == typeof(PopupPresenterCoroutine<,,>))
                            return true;

                        tType = tType.BaseType;
                    } while (tType != null);

                    return false;
                }
            }

            return (viewRegistrationList, popupRegistrationList);
        }

        void BindWrapper<TView, TPresenter>(string path)
        {
            Container.BindViewPresenter<TView, TPresenter>(path);
        }
    }
}