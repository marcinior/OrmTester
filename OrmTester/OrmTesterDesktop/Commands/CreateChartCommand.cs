﻿using OrmTesterLib.Enums;
using System;
using System.Windows.Input;

namespace OrmTesterDesktop.Commands
{
    public class CreateChartCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<OperationType> createDiagram;

        public CreateChartCommand(Action<OperationType> createDiagram)
        {
            this.createDiagram = createDiagram;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            createDiagram((OperationType)parameter);
        }
    }
}
