#region Copyright
// <copyright file="MvxActivityView.cs" company=" PnxSmartWDA">
// (c) Copyright  PnxSmartWDA. http://www. PnxSmartWDA.com
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
// 
// Project Lead - Stuart Lodge,  PnxSmartWDA. http://www. PnxSmartWDA.com
#endregion

using System;
using Android.App;
using Android.Content;
using Android.OS;
using  PnxSmartWDA.MvvmCross.Android.ExtensionMethods;
using  PnxSmartWDA.MvvmCross.Android.Interfaces;
using  PnxSmartWDA.MvvmCross.ExtensionMethods;
using  PnxSmartWDA.MvvmCross.Interfaces.ServiceProvider;
using  PnxSmartWDA.MvvmCross.Interfaces.ViewModels;
using  PnxSmartWDA.MvvmCross.Platform.Diagnostics;

namespace  PnxSmartWDA.MvvmCross.Android.Views
{
    public abstract class MvxActivityView<TViewModel>
        : Activity
        , IMvxAndroidView<TViewModel>
        , IMvxServiceConsumer<IMvxIntentResultSink>
        where TViewModel : class, IMvxViewModel
    {
        protected MvxActivityView()
        {
            IsVisible = true;
        }

        #region Common code across all android views - one case for multiple inheritance?

        private TViewModel _viewModel;

        public Type ViewModelType
        {
            get { return typeof(TViewModel); }
        }

        public bool IsVisible { get; private set; }

        public TViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnViewModelSet();
            }
        }

        public void MvxInternalStartActivityForResult(Intent intent, int requestCode)
        {
            base.StartActivityForResult(intent, requestCode);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.OnViewCreate();
        }

        protected override void OnDestroy()
        {
            this.OnViewDestroy();
            base.OnDestroy();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            this.OnViewNewIntent();
        }

        protected abstract void OnViewModelSet();

        protected override void OnResume()
        {
            base.OnResume();
            IsVisible = true;
            this.OnViewResume();
        }

        protected override void OnPause()
        {
            this.OnViewPause();
            IsVisible = false;
            base.OnPause();
        }

        protected override void OnStart()
        {
            base.OnStart();
            this.OnViewStart();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            this.OnViewRestart();
        }

        protected override void OnStop()
        {
            this.OnViewStop();
            base.OnStop();
        }

        public override void StartActivityForResult(Intent intent, int requestCode)
        {
            switch (requestCode)
            {
                case (int)MvxIntentRequestCode.PickFromFile:
                    MvxTrace.Trace("Warning - activity request code may clash with Mvx code for {0}", (MvxIntentRequestCode)requestCode);
                    break;
                default:
                    // ok...
                    break;
            }
            base.StartActivityForResult(intent, requestCode);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            this.GetService<IMvxIntentResultSink>().OnResult(new MvxIntentResultEventArgs(requestCode, resultCode, data));
            base.OnActivityResult(requestCode, resultCode, data);
        }

        #endregion
    }
}