#region Copyright
// <copyright file="MvxBaseConsoleSetup.cs" company="Cirrious">
// (c) Copyright Cirrious. http://www.cirrious.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Author - Stuart Lodge, Cirrious. http://www.cirrious.com
#endregion

using System.Collections.Generic;
using PnxSmartWDA.MvvmCross.Console.Interfaces;
using PnxSmartWDA.MvvmCross.Console.Views;
using PnxSmartWDA.MvvmCross.ExtensionMethods;
using PnxSmartWDA.MvvmCross.Interfaces.ServiceProvider;
using PnxSmartWDA.MvvmCross.Interfaces.Views;
using PnxSmartWDA.MvvmCross.Platform;
using PnxSmartWDA.MvvmCross.Views;

namespace PnxSmartWDA.MvvmCross.Console.Platform
{
    public abstract class MvxBaseConsoleSetup 
        : MvxBaseSetup        
          , IMvxServiceProducer<IMvxConsoleCurrentView>
          , IMvxServiceProducer<IMvxMessagePump>
          , IMvxServiceProducer<IMvxConsoleNavigation>
    {
        public override void Initialize()
        {
            base.Initialize();
            InitializeMessagePump();
        }

        public virtual void InitializeMessagePump()
        {
            var messagePump = new MvxConsoleMessagePump();
            this.RegisterServiceInstance<IMvxMessagePump>(messagePump);
            this.RegisterServiceInstance<IMvxConsoleCurrentView>(messagePump);
        }

        protected override MvxViewsContainer CreateViewsContainer()
        {
            var container = CreateConsoleContainer();
            this.RegisterServiceInstance<IMvxConsoleNavigation>(container);
            return container;
        }

        protected override IMvxViewDispatcherProvider CreateViewDispatcherProvider()
        {
            return new MvxConsoleDispatcherProvider();
        }

        protected virtual MvxBaseConsoleContainer CreateConsoleContainer()
        {
            return new MvxConsoleContainer();
        }

        protected override IDictionary<System.Type, System.Type> GetViewModelViewLookup()
        {
            return GetViewModelViewLookup(GetType().Assembly, typeof(IMvxConsoleView));
        }

    }
}