﻿using ModelModule.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Regions;
using Prisma.Core.Abstractions;
using Prism.Mvvm;
using Prism.Commands;
using System.Configuration;

namespace ModelModule.ViewModels
{
    public class UserControlPrintPyScrViewModel : BindableBase
    {
        private readonly IProjectPage _projectPage;
        public ICommand PrintCommand { get; }
        private string _pythonScriptText = "";
        public string PythonScriptText
        {
            get => _pythonScriptText;
            set => SetProperty(ref _pythonScriptText, value);
        }

        public UserControlPrintPyScrViewModel(IProjectPage projectPage)
        {   
            _projectPage = projectPage;
            PrintCommand = new DelegateCommand(PrintPyScript);
            
        }
        private void PrintPyScript()
        {
            string filepath = _projectPage.PyScriptPath;
            if (filepath != "") ReadPythonFile(_projectPage.PyScriptPath);
        }
        private async void ReadPythonFile(string filepath)
        {
            using (StreamReader read = new(filepath))
            {
                PythonScriptText = await read.ReadToEndAsync();
            }
        }
    }
}
