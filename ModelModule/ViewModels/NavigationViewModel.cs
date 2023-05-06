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
        private readonly IRegionManager _regionManager;
        private readonly INavigation _navigation;
        

        public NavigationViewModel(IRegionManager regionManager, INavigation navigation)
        {
            _regionManager = regionManager;
            _navigation = navigation;
            NextPage = new DelegateCommand(Next); //.ObservesProperty(()=>_navigation.CanNext);
            PreviousPage = new DelegateCommand(Previous).ObservesCanExecute(()=>_navigation.CanPrevious);
        }

        public void Next()
        {
            _navigation.NextPage();
        }

        public void Previous()
        {
           _navigation.PreviousPage();
        }
    }
}