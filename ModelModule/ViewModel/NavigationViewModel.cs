using Prism.Regions;
using Prisma.Core.Abstractions;
namespace ModelModule.ViewModel
{
    public class NavigationViewModel
    {
        private readonly IRegionManager _regionManager;
        
        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void NextPage(string page)
        {
            
        }

    }
}