using System;
using System.Net.Mail;
using System.Windows.Controls;
using System.Windows.Input;
using Aspose.Cells.Charts;
using ModelModule.Views;
using Prism.Commands;
using Prism.Events;
using Prisma.Core.Abstractions;
using Prism.Regions;
using Prism.Mvvm;
using Prisma.Core;

namespace ModelModule.Components
{
    public class ProjectNavigation: BindableBase, INavigation
    {
        private enum View
        {
            UserControlOpenDB,
            UserControlOpenScript,
            UserControlOutputPath,
            UserControlPrintPyScr
        }

        public ICommand Next { get; private set; }
        public ICommand Previous { get; private set; }

        public string CurrentPage { get; set; }
        private readonly IRegionManager _regionManager;
        private bool _canNext = true;
        private bool _canPrevious = false;

        public bool CanNext
        {
            get => _canNext;
            set => SetProperty(ref _canNext, value);
        }
        public bool CanPrevious
        {
            get => _canPrevious;
            set => SetProperty(ref _canPrevious, value);
        }

        public void NextPage()
        {
            CurrentPage = GetCurrentPage();
            switch (CurrentPage)
            {
                case nameof(View.UserControlOpenDB):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenScript));
                    CurrentPage = nameof(UserControlOpenScript);
                    CanPrevious = true;
                    break;
                case nameof(View.UserControlOpenScript):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlPrintPyScr));
                    CurrentPage = nameof(UserControlPrintPyScr);
                    break;
                case nameof(View.UserControlPrintPyScr):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOutputPath));
                    CurrentPage = nameof(UserControlOutputPath);
                    CanPrevious = true;
                    CanNext = false;
                    break;
            }
        }

        public void PreviousPage()
        {
            CurrentPage = GetCurrentPage() ;
               
            switch (CurrentPage)
            {
                case nameof(View.UserControlOpenScript):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenDB));
                    CurrentPage = nameof(UserControlOpenDB);
                    CanPrevious = false;
                    break;
                case nameof(View.UserControlPrintPyScr):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenScript));
                    CurrentPage = nameof(UserControlOpenScript);
                    break;
                case nameof(View.UserControlOutputPath): 
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlPrintPyScr));
                    CurrentPage = nameof(UserControlPrintPyScr);
                    CanNext = true;
                    break;
            }
        }

        public string GetCurrentPage()
        {
            string[]? listRegionName = new string[3];
            string viewName;
            var regionCollection = _regionManager.Regions["MainRegion"].ActiveViews;
            foreach (var region in regionCollection)
            {
                listRegionName = region?.ToString()?.Split('.');
            }
            viewName = listRegionName[2];
            return viewName;
        }

        public ProjectNavigation(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Next = new DelegateCommand(NextPage).ObservesCanExecute(() => CanNext);
            Previous = new DelegateCommand(PreviousPage).ObservesCanExecute(() => CanPrevious);
        }
    }
}