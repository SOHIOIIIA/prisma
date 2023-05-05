using Prisma.Core.Abstractions;
using Prism.Regions;
using Prism.Mvvm;

namespace ModelModule.Components
{
    public class ProjectNavigation: BindableBase, INavigation
    {
        private enum View
        {
            
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

        public void NextPage(string page)
        {
            
        }

        public void PreviousPage(string page)
        {
            
        }

        public ProjectNavigation(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}