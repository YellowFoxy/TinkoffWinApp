﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using TinkoffWinApp.Managers;
using TinkoffWinApp.ViewModels;
using TinkoffWinApp.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace TinkoffWinApp
{
    sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            Initialize();
            InitializeComponent();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();

            container.RegisterWinRTServices();

            container
                .Singleton<IRequestManager, RequestManager>()
                .Singleton<IImageManager, ImageManager>()
                .Singleton<IProductManager, ProductManager>()

                .PerRequest<MainViewModel>()
                .PerRequest<ProductDetailViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            DisplayRootView<MainView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
