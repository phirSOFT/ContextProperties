using System;

namespace phirSOFT.ContextProperties
{
    /// <summary>
    /// Implements the <see cref="IMemberContextProperty{TValue}"/>. This implementation does not keep the object alive.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class MemberContextProperty<TValue> : IMemberContextProperty<TValue>
    {
        private readonly IContextProperty<TValue> _contextProperty;
        private readonly WeakReference _instance;

        public MemberContextProperty(IContextProperty<TValue> contextProperty, object instance)
        {
            _contextProperty = contextProperty;
            _instance = new WeakReference(instance);
        }

        public TValue this[IContextProvider<IContextProperty<TValue>, TValue> context] =>
            _contextProperty[_instance, context];

        public TValue Value => _contextProperty[_instance.Target, _contextProperty.DefaultContext];
    }
}