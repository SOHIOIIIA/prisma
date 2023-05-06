using System.Windows;
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
        private bool _canNext;
        private bool _canPrevious;

        private bool CanNext
        {
            get => _canNext;
            set => SetProperty(ref _canNext, value);
        }
        private bool CanPrevious
        {
            get => _canPrevious;
            set => SetProperty(ref _canPrevious, value);
        }


        public NavigationViewModel(IRegionManager regionManager, INavigation navigation)
        {
            _regionManager = regionManager;
            _navigation = navigation;
            CanNext = _navigation.CanNext;
            CanPrevious = _navigation.CanPrevious;
            NextPage = new DelegateCommand(Next).ObservesCanExecute(()=>CanNext);
            PreviousPage = new DelegateCommand(Previous).ObservesCanExecute(()=>CanPrevious);
        }

        private void Next()
        {
            _navigation.NextPage();
            CanNext = _navigation.CanNext;
            CanPrevious = _navigation.CanPrevious;
        }

        private void Previous()
        {
            _navigation.PreviousPage();
            CanNext = _navigation.CanNext;
            CanPrevious = _navigation.CanPrevious;
        }
    }
}