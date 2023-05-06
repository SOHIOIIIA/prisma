using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prisma.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Regions;
using Prisma.Core.Abstractions;
using Prism.Mvvm;
using Prism.Commands;

namespace ModelModule.ViewModels
{
    class UserControlOpenScriptViewModel : BindableBase
    {
        public ICommand _openScriptPy { get; }

        private readonly IProjectPage _projectPage;
        public UserControlOpenScriptViewModel(IProjectPage projectPage)
        {
            _projectPage = projectPage;
            _openScriptPy = new DelegateCommand(PerformOpenScriptPy);
        }

        private string _filePath = "";

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    SetProperty(ref _filePath, value);
                }
            }
        }

        private void PerformOpenScriptPy()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Python Scripts (*.py)|*.py";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                _projectPage.PyScriptPath = FilePath;
            }
        }
    }
}
