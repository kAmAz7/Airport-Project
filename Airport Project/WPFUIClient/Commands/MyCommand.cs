﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUIClient.Commands
{
    class MyCommand : ICommand
    {
        Action _act;

        public MyCommand(Action act)
        {
            _act = act;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void register(EventHandler eventHandler)
        {
            CanExecuteChanged += eventHandler;
        }

        public void Execute(object parameter)
        {
            _act();
        }
    }
}
