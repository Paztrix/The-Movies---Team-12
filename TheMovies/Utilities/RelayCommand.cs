using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TheMovies.Utilities {
    public class RelayCommand : ICommand {
        //Delegate til metode når Command aktiveres
        private Action<object> _execute;
        //Delegate til metode når der afgøres om Command kan aktiveres
        private Func<object, bool> _canExecute;

        /*
         * Event der udføres når CanExecute skal genevalueres
         * CommandManager.RequerySuggested gør at WPF automatisk genbruger CanExecute
         */
        public event EventHandler? CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Constructor der kræver en execute-metode, men kan også tage en CanExecute-metode
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        //Returnerer True, hvis command kan udføres
        //Hvis _canExecute returnerer null, kan kommandoen altid udføres
        public bool CanExecute(object? parameter) {
            return _canExecute == null || _canExecute(parameter);
        }

        //Udfører handling knyttet til Command
        public void Execute(object? parameter) {
            _execute(parameter);
        }


        //Tvinger UI til at tjekke CanExecute
        public void RaiseCanExecuteChanged() {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
