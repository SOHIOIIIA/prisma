using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prisma.Core.Abstractions;
using Prisma.Core.Events;

namespace ModelModule.ViewModels
{
    public class NavigationViewModel: BindableBase
    {
        public ICommand NextPage { get; }
        public ICommand PreviousPage { get; }
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ev;
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


        public NavigationViewModel(IRegionManager regionManager, INavigation navigation, IEventAggregator ev)
        {
            _regionManager = regionManager;
            _ev = ev;
            _navigation = navigation;
            CanNext = _navigation.CanNext;
            CanPrevious = _navigation.CanPrevious;
            NextPage = new DelegateCommand(Next).ObservesCanExecute(()=>CanNext);
            PreviousPage = new DelegateCommand(Previous).ObservesCanExecute(()=>CanPrevious);
        }

        private void Next()
        {
            _navigation.NextPage();
            _ev.GetEvent<PageLoad>().Publish(_navigation.CurrentPage);
            CanNext = _navigation.CanNext;
            CanPrevious = _navigation.CanPrevious;
        }

        private void Previous()
        {
            _navigation.PreviousPage();
            _ev.GetEvent<PageLoad>().Publish(_navigation.CurrentPage);
            CanNext = _navigation.CanNext;
            CanPrevious = _navigation.CanPrevious;
        }
    }
}