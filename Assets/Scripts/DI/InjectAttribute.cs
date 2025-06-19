using System;

namespace DI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Constructor)]
    public class InjectAttribute : Attribute
    {
        
    }
}