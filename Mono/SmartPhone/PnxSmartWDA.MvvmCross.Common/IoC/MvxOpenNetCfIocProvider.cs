#region Copyright
// <copyright file="MvxOpenNetCfIocProvider.cs" company=" PnxSmartWDA">
// (c) Copyright  PnxSmartWDA. http://www. PnxSmartWDA.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge,  PnxSmartWDA. http://www. PnxSmartWDA.com
#endregion

using  PnxSmartWDA.MvvmCross.Interfaces.IoC;

namespace  PnxSmartWDA.MvvmCross.IoC
{
    public class MvxOpenNetCfIocProvider : IMvxIoCProvider
    {
        #region IMvxIoCProvider Members

        public bool SupportsService<T>() where T : class
        {
            return MvxOpenNetCfContainer.Current.CanResolve<T>();
        }

        public T GetService<T>() where T : class
        {
            return MvxOpenNetCfContainer.Current.Resolve<T>();
        }

        public bool TryGetService<T>(out T service) where T : class
        {
            return MvxOpenNetCfContainer.Current.TryResolve<T>(out service);
        }

        public void RegisterServiceType<TFrom, TTo>()
        {
            MvxOpenNetCfContainer.Current.RegisterServiceType<TFrom, TTo>();
        }

        public void RegisterServiceInstance<TInterface>(TInterface theObject)
        {
            MvxOpenNetCfContainer.Current.RegisterServiceInstance(theObject);
        }

        #endregion
    }
}