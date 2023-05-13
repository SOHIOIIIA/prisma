using System;
using ModelModule.Model;
using ModelModule.Components;
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
            containerRegistry.RegisterSingleton<Object, UserControlOpenDB>("UserControlOpenDB");
            containerRegistry.RegisterSingleton<Object, UserControlOpenScript>("UserControlOpenScript");
            containerRegistry.RegisterSingleton<Object, UserControlPrintPyScr>("UserControlPrintPyScr");
            containerRegistry.RegisterSingleton<Object, UserControlOutputPath>("UserControlOutputPath");
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager
                .RegisterViewWithRegion(RegionsName.MainRegion, typeof(UserControlOpenDB))
                .RegisterViewWithRegion(RegionsName.MainRegion, typeof(UserControlOpenScript))
                .RegisterViewWithRegion(RegionsName.MainRegion, typeof(UserControlPrintPyScr))
                .RegisterViewWithRegion(RegionsName.MainRegion, typeof(UserControlOutputPath))
                .RegisterViewWithRegion(RegionsName.RightRegion, typeof(Navigation));
        }
    }
}