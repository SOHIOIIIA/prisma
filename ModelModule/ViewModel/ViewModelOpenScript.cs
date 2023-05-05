using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prisma.Core.Abstractions;
using Prism.Mvvm;
using Microsoft.Win32;
using ModelModule.Model;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;
using Prisma.Core.Abstractions;
using Prism.Mvvm;

namespace ModelModule.ViewModel
{
    public class ViewModelOpenScript : BindableBase
    {
        private readonly IProjectPage _projectPage;
        public ViewModelOpenScript(IProjectPage projectPage) 
        {
            _projectPage = projectPage;   
        }

        private DelegateCommand openScriptPy;

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
        public ICommand OpenScriptPy => openScriptPy ??= new DelegateCommand(PerformOpenScriptPy);

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
