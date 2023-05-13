using System;
using System.Net.Mail;
using System.Windows.Controls;
using System.Windows.Input;
using ModelModule.Views;
using Prism.Commands;
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
            string currentPage = GetCurrentPage();
            switch (currentPage)
            {
                case nameof(View.UserControlOpenDB):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, "UserControlOpenScript");
                    CanPrevious = true;
                    break;
                case nameof(View.UserControlOpenScript):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlPrintPyScr));
                    break;
                case nameof(View.UserControlPrintPyScr):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOutputPath));
                    CanPrevious = true;
                    CanNext = false;
                    break;
            }
        }

        public void PreviousPage()
        {
            string currentPage = GetCurrentPage() ;
               
            switch (currentPage)
            {
                case nameof(View.UserControlOpenScript):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenDB));
                    CanPrevious = false;
                    break;
                case nameof(View.UserControlPrintPyScr):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenScript));
                    break;
                case nameof(View.UserControlOutputPath): 
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlPrintPyScr));
                    CanNext = true;
                    break;
            }
        }

        private string GetCurrentPage()
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