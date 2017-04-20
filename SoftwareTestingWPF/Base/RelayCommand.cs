using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoftwareTestingWPF.Base
{
    class RelayCommand : ICommand
    {
        private Func<Boolean> _canExecuteFunc;
        private Action<object> _executeAction;

        public RelayCommand(Action<object> execute, Func<Boolean> canExecute)
        {
            if (null == execute)
            {
                throw new ArgumentException("excute");
            }
            _executeAction = execute;
            _canExecuteFunc = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc == null ? true : _canExecuteFunc();
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
