using System;

namespace Taime.Application.Utils.Attributes
{
    public class InjectionTypeAttribute : Attribute
    {
        public readonly InjectionType InjectionType;

        public InjectionTypeAttribute(InjectionType injectionType)
        {
            InjectionType = injectionType;
        }
    }

    public enum InjectionType
    {
        Singleton,
        Scoped
    }
}
