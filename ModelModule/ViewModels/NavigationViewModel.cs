using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prisma.Core.Abstractions;

namespace ModelModule.ViewModels
{
    public class NavigationViewModel: BindableBase
    {
        public ICommand NextPage { get; }
        public ICommand PreviousPage { get; }
        private string _current_page;
        private readonly IRegionManager _regionManager;
        private readonly INavigation _navigation;

        public NavigationViewModel(IRegionManager regionManager, INavigation navigation)
        {
            _regionManager = regionManager;
            _navigation = navigation;
            _current_page = GetCurrentPage();
            NextPage = new DelegateCommand(Next).ObservesProperty(()=>_navigation.CanNext);
            PreviousPage = new DelegateCommand(Previous).ObservesProperty(()=>_navigation.CanPrevious);
        }

        public void Next()
        {
            _navigation.NextPage();
        }

        public void Previous()
        {
           _navigation.PreviousPage();
        }

        private string GetCurrentPage()
        {
            string[]? listRegionName = new string[3];
            var regionCollection = _regionManager.Regions["MainRegion"].Views;
            foreach (var region in regionCollection)
            {
                listRegionName = region?.ToString()?.Split('.');
            }
            if (listRegionName != null)
            {
                return listRegionName[2];
            }
            return "NULL";
        }

    }
}