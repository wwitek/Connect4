using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Connect4.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase
    {
        public event EventHandler<OnMoveCompletedEventArgs> OnMoveCompleted;

        private Command onCreatedCommand;
        public Command OnCreatedCommand
        {
            get
            {
                return onCreatedCommand ?? (onCreatedCommand = new Command(
                    () =>
                    {
                        Debug.WriteLine("OnCreatedCommand");
                    },
                    () =>
                    {
                        return true;
                    }
                    ));
            }
        }

        private DelegateCommand<object> onTouchCommand;
        public DelegateCommand<object> OnTouchedCommand
        {
            get
            {
                return onTouchCommand ?? (onTouchCommand = new DelegateCommand<object>((num) =>
                {
                    int column = 0;
                    int.TryParse(num.ToString(), out column);
                    Debug.WriteLine("OnTouchedCommand " + column);
                    OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs { MoveId = 1, Column = column, Row = 0 };
                    OnMoveCompleted?.Invoke(this, args);
                }));
            }
        }

        public string MainText { get; set; }

        public GamePageViewModel()
        {
            MainText = "Connect 4";
        }
    }
}