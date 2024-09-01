using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ElectrumGames.MVP
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoRegisterViewAttribute : Attribute
    {
        private readonly string[] _sceneBindings;
        private readonly string _customPath;

        public AutoRegisterViewAttribute(string customPath = null, params string[] sceneBindings)
        {
            _sceneBindings = sceneBindings;
            _customPath = customPath;
        }

        public AutoRegisterViewAttribute()
        {
            _sceneBindings = Array.Empty<string>();
            _customPath = null;
        }

        public static IEnumerable<(Type view, string path)> GetViews(Assembly[] assemblies, string specificScene = null)
        {
            return assemblies.Select(a => a.GetTypes()
                .Where(type =>
                {
                    var attr = type.GetCustomAttributes(typeof(AutoRegisterViewAttribute), true);
                    return attr.Length > 0; // && ((AutoRegisterViewAttribute)attr[0]).target == viewManager;
                })
                .Where(t => t != typeof(View) && typeof(View).IsAssignableFrom(t))
                .Where(t =>
                {
                    string[] bindings = ((AutoRegisterViewAttribute) t
                            .GetCustomAttributes(typeof(AutoRegisterViewAttribute), true)[0])
                        ._sceneBindings;
                    return (bindings.Length == 0 && specificScene == null) || bindings.Contains(specificScene);
                })
                .Select(t => (t, ((AutoRegisterViewAttribute) t
                        .GetCustomAttributes(typeof(AutoRegisterViewAttribute),
                            true)[0])
                    ._customPath ?? string.Format(View.StandardPathFormat, t.Name)))).SelectMany(array => array);
        }
    }
}