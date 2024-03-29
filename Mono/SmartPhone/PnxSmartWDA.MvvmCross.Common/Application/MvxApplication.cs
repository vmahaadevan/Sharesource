﻿#region Copyright
// <copyright file="MvxApplication.cs" company=" PnxSmartWDA">
// (c) Copyright  PnxSmartWDA. http://www. PnxSmartWDA.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge,  PnxSmartWDA. http://www. PnxSmartWDA.com
#endregion

using System;
using System.Collections.Generic;
using  PnxSmartWDA.MvvmCross.Exceptions;
using  PnxSmartWDA.MvvmCross.ExtensionMethods;
using  PnxSmartWDA.MvvmCross.Interfaces.Application;
using  PnxSmartWDA.MvvmCross.Interfaces.ServiceProvider;
using  PnxSmartWDA.MvvmCross.Interfaces.ViewModels;
using  PnxSmartWDA.MvvmCross.Views;

namespace  PnxSmartWDA.MvvmCross.Application
{
    public abstract class MvxApplication
        :  IMvxViewModelLocatorFinder
          , IMvxViewModelLocatorStore
    {
        private readonly ViewModelLocatorLookup _lookup = new ViewModelLocatorLookup();

        protected MvxApplication()
        {
            UseDefaultViewModelLocator = true;
        }

        protected bool UseDefaultViewModelLocator { get; set; }

        #region IMvxViewModelLocatorFinder Members

        public IMvxViewModelLocator FindLocator(MvxShowViewModelRequest request)
        {
            var candidate = _lookup.Find(request);
            if (candidate != null)
                return candidate;

            if (UseDefaultViewModelLocator)
                return CreateDefaultViewModelLocator();

            throw new MvxException("No ViewModelLocator registered for " + request.ViewModelType);
        }

        protected virtual IMvxViewModelLocator CreateDefaultViewModelLocator()
        {
            return new MvxDefaultViewModelLocator();
        }

        #endregion

        #region IMvxViewModelLocatorStore Members

        public void AddLocators(IEnumerable<IMvxViewModelLocator> locators)
        {
            foreach (var locator in locators)
            {
                AddLocator(locator);
            }
        }

        public void AddLocator(IMvxViewModelLocator locator)
        {
            _lookup.Add(locator);
        }

        #endregion

        #region Nested type: ViewModelLocatorLookup

        private class ViewModelLocatorLookup
            : Dictionary<Type, IMvxViewModelLocator>
            , IMvxServiceConsumer<IMvxViewModelLocatorAnalyser>
        {
            public IMvxViewModelLocator Find(MvxShowViewModelRequest request)
            {
                IMvxViewModelLocator toReturn;
                if (!TryGetValue(request.ViewModelType, out toReturn))
                    return null;
                return toReturn;
            }

            public void Add(IMvxViewModelLocator locator)
            {
                var analyser = this.GetService();
                var methods = analyser.GenerateLocatorMethods(locator.GetType());
                foreach (var method in methods)
                {
                    this[method.ReturnType] = locator;
                }
            }
        }

        #endregion
    }
}