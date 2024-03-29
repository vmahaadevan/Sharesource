#region Copyright
// <copyright file="MvxRequestedBy.cs" company=" PnxSmartWDA">
// (c) Copyright  PnxSmartWDA. http://www. PnxSmartWDA.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge,  PnxSmartWDA. http://www. PnxSmartWDA.com
#endregion

using  PnxSmartWDA.MvvmCross.Platform.Json;
using Newtonsoft.Json;

namespace  PnxSmartWDA.MvvmCross.Interfaces.ViewModels
{
    public class MvxRequestedBy
    {
        public static MvxRequestedBy Unknown = new MvxRequestedBy(MvxRequestedByType.Unknown);
        public static MvxRequestedBy Bookmark = new MvxRequestedBy(MvxRequestedByType.Bookmark);
        public static MvxRequestedBy UserAction = new MvxRequestedBy(MvxRequestedByType.UserAction);

        public MvxRequestedBy()
            : this(MvxRequestedByType.Unknown)
        {           
        }

        public MvxRequestedBy(MvxRequestedByType requestedByType)
            : this(requestedByType, null)
        {            
        }

        public MvxRequestedBy(MvxRequestedByType requestedByType, string additionalInfo)
        {
            Type = requestedByType;
            AdditionalInfo = additionalInfo;
        }

        [JsonConverter(typeof(MvxGeneralJsonEnumConverter))]
        public MvxRequestedByType Type { get; set; }
        public string AdditionalInfo { get; set; }
    }
}