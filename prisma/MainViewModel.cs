using Prism.Events;
using Prism.Mvvm;
using Prisma.Core.Events;

namespace Prism.Shell;

public class MainViewModel: BindableBase
{
    private string _title = "Проект по C#";
    public string Title { 
        get => _title;
        set => SetProperty(ref _title, value) ; 
    }
    
    public MainViewModel(IEventAggregator ev)
    {
        ev.GetEvent<PageLoad>().Subscribe(x=> Title = x);
    }
}