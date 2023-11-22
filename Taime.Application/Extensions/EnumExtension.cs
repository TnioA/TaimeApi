using System;
using System.ComponentModel;
using System.Reflection;
using Taime.Application.Contracts.Shared;
using Taime.Application.Utils.Services;

namespace Taime.Application.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(Type enumType, object value)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("value");
            }

            var fi = enumType.GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length == 0)
            {
                return value.ToString();
            }

            return attributes[0].Description;
        }

        public static string Description(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var attribute = value.GetCustomAttribute<DescriptionAttribute>();

            if (attribute == null)
                return value.ToString();

            return attribute.Description;
        }

        public static string Description(this Enum value, params object[] parameters)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var attribute = value.GetCustomAttribute<DescriptionAttribute>();

            if (attribute == null)
                return string.Format(value.ToString(), parameters);

            return string.Format(attribute.Description, parameters);
        }

        public static T GetCustomAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var fi = value.GetType().GetField(value.ToString());

            return fi.GetCustomAttribute<T>();
        }

        public static MetaError ErrorDescription(this Enum errorItem, params object[] parameters)
        {
            string description = errorItem.Description();

            string message = string.Empty;

            if (parameters == null)
            {
                message = description;
            }
            else
            {
                message = string.Format(description, parameters);
            }

            int code = (int)Convert.ChangeType(errorItem, errorItem.GetTypeCode());

            var aux = errorItem.ToString().Split("_");

            int? protocolCode = null;

            if (aux.Length >= 3 && !string.IsNullOrWhiteSpace(aux[2]))
            {
                if (int.TryParse(aux[2], out int pc))
                {
                    protocolCode = pc;
                }
            }

            return new MetaError(
                new Error()
                {
                    Code = code.ToString("000"),
                    Message = message
                },
                protocolCode
            );
        }

        private static string ToCamelCaseString(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return char.ToLowerInvariant(str[0]) + str.Substring(1);

            return str;
        }

        public static MetaError ErrorDescription(this object errorItem, params object[] parameters)
        {
            return ErrorDescription((Enum)errorItem, parameters);
        }

        public static bool IsEnum(Type type, object value)
        {
            return Enum.IsDefined(type, value);
        }
    }
}