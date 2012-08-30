#region Copyright
// <copyright file="MvxMainThreadDispatchingObject.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge, Cirrious. http://www.cirrious.com
#endregion

using System;
using  PnxSmartWDA.MvvmCross.ExtensionMethods;
using  PnxSmartWDA.MvvmCross.Interfaces.ServiceProvider;
using  PnxSmartWDA.MvvmCross.Interfaces.Views;

namespace  PnxSmartWDA.MvvmCross.ViewModels
{
    public abstract class MvxMainThreadDispatchingObject 
        : IMvxServiceConsumer<IMvxViewDispatcherProvider>
    {
        protected IMvxViewDispatcher ViewDispatcher
        {
            get { return this.GetService<IMvxViewDispatcherProvider>().Dispatcher; }
        }

        protected void InvokeOnMainThread(Action action)
        {
            if (ViewDispatcher != null)
                ViewDispatcher.RequestMainThreadAction(action);
        }
    }
}