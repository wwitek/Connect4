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

        public Command OnCreateCommand
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

        public string MainText { get; set; }

        public GamePageViewModel()
        {
            MainText = "Connect 4";
        }
    }
}
