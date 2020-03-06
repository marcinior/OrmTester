using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrmTesterDesktop.ViewModels
{
    public class ErrorMsgViewModel : INotifyPropertyChanged
    {
        private string errorMsg;

        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                errorMsg = value;
                this.NotifyPropertyChanged(nameof(ErrorMsg));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
