using ModelModule.Model;
using ModelModule.View;
using ModelModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prisma.Core;
using Prisma.Core.Abstractions;

namespace ModelModule
{
    public class ModelModule: IModule
    {
        #region inject
        private readonly IRegionManager _regionManager;
        public ModelModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        #endregion

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IProjectPage, ProjectModel>();
            containerRegistry.RegisterSingleton<INavigation, ProjectNavigation>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager
                .RegisterViewWithRegion(RegionsName.MainRegion, typeof(UserControlPrintPyScr))
                .RegisterViewWithRegion(RegionsName.RightRegion, typeof(Navigation));
        }
    }
}