using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.ViewModels
{
    public class GameViewModel : BindableBase
    {
        public string MainText { get; set; }

        public GameViewModel()
        {
            MainText = "Connect 4";
        }
    }
}
