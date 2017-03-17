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
    public class StartPageViewModel : BindableBase
    {
        private Command testCommand;
        private Command<object> unfocusedCommand;

        string _title = "Start Page";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public Command TestCommand
        {
            get
            {
                return this.testCommand ?? (this.testCommand = new Command(
                     () =>
                     {
                         Debug.WriteLine("Test");
                     },
                     () =>
                     {
                         // CanExecute delegate
                         return true;
                     }));
            }
        }
        public Command<object> UnfocusedCommand
        {
            get
            {
                return this.unfocusedCommand ?? (this.unfocusedCommand = new Command<object>(
                     (param) =>
                     {
                         Debug.WriteLine(string.Format("Unfocused raised with param {0}", param));
                     },
                     (param) =>
                     {
                         // CanExecute delegate
                         return true;
                     }));
            }
        }
    }
}