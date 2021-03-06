﻿using System;
using Android.App;
using MvvmCross.Droid.Views;
using TestProject.Core.ViewModels;

namespace TestProject.Droid
{
    [Activity(Label = "MainView", MainLauncher = true)]
    public class MainViewActivity : MvxActivity<MainViewModel>
    {
        public MainViewActivity()
        {
            
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.MainView);
        }
    }
}
