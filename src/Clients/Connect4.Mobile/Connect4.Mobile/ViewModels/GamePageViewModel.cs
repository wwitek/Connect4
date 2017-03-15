using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase
    {
        public string MainText { get; set; }

        public GamePageViewModel()
        {
            MainText = "Connect 4";
        }
    }
}
