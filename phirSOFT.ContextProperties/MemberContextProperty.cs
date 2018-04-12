using System;

namespace phirSOFT.ContextProperties
{
    /// <summary>
    /// Implements the <see cref="IMemberContextProperty{TValue}"/>. This implementation does not keep the object alive.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class MemberContextProperty<TValue> : IMemberContextProperty<TValue>
    {
        private IContextProperty<TValue> contextProperty;
        private readonly WeakReference _instance;

        public TValue this[IContextProvider<IContextProperty<TValue>, TValue> context] =>
            contextProperty[_instance, context];

        public TValue Value => contextProperty[_instance.Target, contextProperty.DefaultContext];
    }
}