#region Copyright
// <copyright file="MvxGeneralJsonEnumConverter.cs" company=" PnxSmartWDA">
// (c) Copyright  PnxSmartWDA. http://www. PnxSmartWDA.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge,  PnxSmartWDA. http://www. PnxSmartWDA.com
#endregion


using System;
using System.Reflection;
using Newtonsoft.Json;

namespace  PnxSmartWDA.MvvmCross.Platform.Json
{
    public class MvxGeneralJsonEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
#if NETFX_CORE
            return objectType.GetTypeInfo().IsEnum;
#else
            return objectType.IsEnum;
#endif
        }

        public override void WriteJson(JsonWriter writer, object
                                                              value, JsonSerializer serializer)
        {
            writer.WriteValue((int)value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Enum.ToObject(objectType, int.Parse(reader.Value.ToString()));
        }
    }
}

