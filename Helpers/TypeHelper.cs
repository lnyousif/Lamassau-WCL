using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
using System.Configuration;

namespace Lamassau.Helper
{
    internal class TypeHelper
    {
        public static Object HackType(object val, Type targetType)
        {
            // If target type is generic and Nullble<>
            // Who knows ? Possibly structs will be allowed to inherit 
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {

                // Get argument type for Nullable<T>
                Type baseType = targetType.GetGenericArguments()[0];
                //If original obj was a Nullable-like already - unwrap
                //if (val.GetType().Equals(typeof(Nullable<>)))
                //{
                if (val != null)
                {
                    //if (!val.Equals(null))
                    //{
                    val = ChangeType((object)val, baseType);
                    //}
                }

                return ChangeType((object)val, targetType);
                //}
                // Change type and wrap
                //return Convert.ChangeType(val, targetType);

            }
            else
            {
                return ChangeType(val, targetType);
            }
        }

        static private object ChangeType(object value, Type type)
        {


            if (value == null && type.IsGenericType)
            {
                return Activator.CreateInstance(type);
            }

            if (value == null)
            {
                return null;
            }

            //if ((value is string) && !type.IsGenericType)
            if ((value is string))
            {
                if (value.ToString().Trim().Length == 0)
                {
                    return null;
                }
            }

            if (type == value.GetType())
            {
                return value;
            }

            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }

            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }

            if (value is string && type == typeof(Guid))
            {
                return new Guid(value as string);
            }

            if (value is string && type == typeof(Version))
            {
                return new Version(value as string);
            }

            if (!(value is IConvertible))
            {
                return value;
            }

            return Convert.ChangeType(value, type);
        }


    }
}