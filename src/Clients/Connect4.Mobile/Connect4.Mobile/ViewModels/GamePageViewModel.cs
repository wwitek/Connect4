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

        //private Command onTouchedCommand;
        //public Command OnTouchedCommand
        //{
        //    get
        //    {
        //        return onTouchedCommand ?? (onTouchedCommand = new Command(
        //            (e) =>
        //            {
        //                Debug.WriteLine("OnTouchCommand");
        //            },
        //            (e) =>
        //            {
        //                return true;
        //            }
        //            ));
        //    }
        //}

        private DelegateCommand<OnTouchedEventArgs> onTouchCommand;
        public DelegateCommand<OnTouchedEventArgs> OnTouchedCommand
        {
            get
            {
                return onTouchCommand ?? (onTouchCommand = new DelegateCommand<OnTouchedEventArgs>((num) =>
                {
                    Debug.WriteLine("OnTouchedCommand " + num?.Column.ToString());
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