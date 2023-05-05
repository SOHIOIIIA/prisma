using System.ComponentModel;
using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using ModelModule;

namespace Prism.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModelModule.ModelModule>();
        }
    }
}
