
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TinkoffWinApp.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyOfPropertyChange([CallerMemberName] string name = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
