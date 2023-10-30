﻿using System.Windows.Input;

namespace ShopApp
{
    public class MiComand : ICommand
    {
        Action _action;
        private readonly Func<bool> _canExecute;

        public MiComand(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
