using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Connect4.Mobile.EventArguments;

namespace Connect4.Mobile.ViewModels
{
    public class StartPageViewModel : BindableBase
    {
        private bool _canExecute;
        private string _status;
        private ICommand onTestTappedCommand;

        public event EventHandler<OnMoveCompletedEventArgs> OnMoveCompleted;

        public StartPageViewModel()
        {
            Status = "Started";
            _canExecute = true;
        }
        
        public string Status
        {
            get { return _status;  }
            set { SetProperty(ref _status, value); }
        }

        public ICommand OnTestTappedCommand
        {
            get
            {
                return onTestTappedCommand ?? (onTestTappedCommand = 
                        new Command<int>((column) => HandleEvent(column), (column) => CanExecute(column)));
            }
        }

        public async void HandleEvent(int a)
        {
            _canExecute = false;
            Status = "Working...";
            Debug.WriteLine("Test with param=" + a);
            await Task.Delay(10000);
            Status = "Done";
            OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs() { Row = 3 };
            OnMoveCompleted?.Invoke(this, args);
            _canExecute = true;
        }

        public bool CanExecute(int a)
        {
            return _canExecute;
        }
    }
}