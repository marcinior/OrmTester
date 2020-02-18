using System;
using System.Reflection;

namespace Tests
{
    static class TestHelper
    {
        public static double InvokePrivateMethod<TInstance>(TInstance instance, string methodName, object[] parameters)
        {
            MethodInfo method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (double)method.Invoke(instance, parameters);
        }

        public static TimeSpan AddMicroseconds(this TimeSpan timespan, Int32 value)
        {
            return new TimeSpan(timespan.Ticks + value);
        }
    }
}
