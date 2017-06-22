﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Connect4.Mobile.EventArguments;
using Prism.Navigation;
using Connect4.Domain.Enums;
using Microsoft.AspNet.SignalR.Client;
using Connect4.API;
using Connect4.Domain.EventArguments;
using Connect4.Mobile.Enums;
using Connect4.Mobile.Utilities;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Entities.Players;

namespace Connect4.Mobile.ViewModels
{
    public class OnlinePageViewModel : BindableBase, INavigationAware
    {
        public OnlinePageViewModel(INavigationService navigationService, Dimensions dimensions)
        {
            CancelCommand = new DelegateCommand(OnCancel);

            NavigationService = navigationService;
            Dimensions = dimensions;
        }

        private INavigationService NavigationService { get; }
        public ICommand CancelCommand { get; }
        public Dimensions Dimensions { get; }

        private void OnCancel()
        {
            NavigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
