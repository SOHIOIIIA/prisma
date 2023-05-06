using System.Net.Mail;
using System.Windows.Controls;
using ModelModule.Views;
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
        private readonly IRegionManager _regionManager;
        private bool _canNext;
        private bool _canPrevious;

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
                case nameof(View.UserControlOutputPath): 
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenScript));
                    break;
                case nameof(View.UserControlOpenScript):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlPrintPyScr));
                    break;
                case nameof(View.UserControlPrintPyScr):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOutputPath));
                    break;
            }
        }

        public void PreviousPage()
        {
            string currentPage = GetCurrentPage();
            switch (currentPage)
            {
                case nameof(View.UserControlOpenScript):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenDB));
                    break;
                case nameof(View.UserControlPrintPyScr):
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlOpenScript));
                    break;
                case nameof(View.UserControlOutputPath): 
                    _regionManager.RequestNavigate(RegionsName.MainRegion, nameof(UserControlPrintPyScr));
                    break;
            }
        }

        private string GetCurrentPage()
        {
            string[]? listRegionName = new string[3];
            string viewName;
            var regionCollection = _regionManager.Regions["MainRegion"].Views;
            foreach (var region in regionCollection)
            {
                listRegionName = region?.ToString()?.Split('.');
            }
            
            if (listRegionName != null)
            {
                viewName = listRegionName[2];
                if (viewName == nameof(View.UserControlOpenDB)) CanPrevious = false;
                if (viewName == nameof(View.UserControlOutputPath)) CanNext = false;
                return viewName;
            }
            return "NULL";
        }

        public ProjectNavigation(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _canNext = true;
            _canPrevious = true;
        }
    }
}