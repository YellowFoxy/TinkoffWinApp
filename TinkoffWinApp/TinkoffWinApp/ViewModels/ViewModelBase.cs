using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using TinkoffWinApp.Support;

namespace TinkoffWinApp.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        protected readonly INavigationService NavigationService;

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading)
                    return;
                _isLoading = value;
                NotifyOfPropertyChange();
                if (value)
                    LoadingHelper.GetInstance.StartLoadingIndicator();
                else
                    LoadingHelper.GetInstance.StopLoadingIndicator();
            }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected async Task<T> LoadTask<T>(Func<Task<T>> taskForCancel)
        {
            IsLoading = true;
            
            var result = await taskForCancel.Invoke();
            
            IsLoading = false;
            return result;
        }

        protected async Task LoadTask(Func<Task> taskForCancel)
        {
            await LoadTask(taskForCancel);
        }
    }
}
