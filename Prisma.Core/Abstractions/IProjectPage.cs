using System;
using Prism.Mvvm;
using System.ComponentModel;

namespace Prisma.Core.Abstractions
{
    public interface IProjectPage: INotifyPropertyChanged
    {
        BindableBase CurrentPage { get; set; } // ???
        String DataBasePath { get; set; }
        String PyScriptPath { get; set; }
    }
}