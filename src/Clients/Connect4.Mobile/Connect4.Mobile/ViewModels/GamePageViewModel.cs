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
        private Command onCreatedCommand;
        private Command onTouchedCommand;

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
        public Command OnTouchedCommand
        {
            get
            {
                return onTouchedCommand ?? (onTouchedCommand = new Command(
                    (e) =>
                    {
                        Debug.WriteLine("OnTouchCommand");
                    },
                    (e) =>
                    {
                        return true;
                    }
                    ));
            }
        }

        private DelegateCommand<TouchEventArgs> onTouchDelegateCommand;
        public DelegateCommand<TouchEventArgs> OnTouchDelegateCommand
        {
            get
            {
                return onTouchDelegateCommand ?? (onTouchDelegateCommand = new DelegateCommand<TouchEventArgs>((num) =>
                {
                    Debug.WriteLine("OnTouchDelegateCommand " + num?.Column.ToString());
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