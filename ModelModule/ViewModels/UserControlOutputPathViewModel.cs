using ModelModule.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Mvvm;
using Prisma.Core.Abstractions;
using Prism.Mvvm;
using Prism.Commands;

namespace ModelModule.ViewModels
{
    class UserControlOutputPathViewModel : BindableBase
    {
        private readonly IProjectPage _projectPage;
        private CancellationTokenSource source;
        private bool _stop = false;
        private bool CanStop
        {
            get => _stop;
            set => SetProperty(ref _stop, value);
        }
        private string _pbVisibility = "Hidden";
        public string PbVisibility
        {
            get => _pbVisibility;
            set => SetProperty(ref _pbVisibility, value);
        }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        private string _outputPath = "";
        public string OutputPath
        {
            get => _outputPath;
            set => SetProperty(ref _outputPath, value);
        }

        public UserControlOutputPathViewModel(IProjectPage projectPage)
        {
            StartCommand = new DelegateCommand(StartExamination);
            StopCommand = new DelegateCommand(StopExamination).ObservesCanExecute(() => CanStop);
        }

        /// <summary>
        /// Сама кнопка есть, надо настроить ее видимость, скорее всего через costum nastr
        /// </summary>
        private async void StartExamination()
        {
            source = new CancellationTokenSource();
            string dbpath = _projectPage.DataBasePath;
            string scriptpath = _projectPage.PyScriptPath;
            if (dbpath != "" && scriptpath != "")
            {
                Process? proc = null;
                try
                {
                    CanStop = true;
                    PbVisibility = "Visible";
                    string processName = $"\"C:\\Windows\\py.exe {scriptpath} {dbpath}\"";
                    proc = Process.Start("cmd", $"/c {processName}");
                    await proc.WaitForExitAsync(source.Token);
                    await Task.Delay(3000);
                    /*var split_path = dbpath.Split("\\");
                    split_path[^1] = "";
                    string model_path = "";
                    foreach (var ind in split_path)
                    {
                        model_path += "\\" + ind;
                    }
                    OutputPath = model_path[1..^1];*/
                    //MessageBox.Show("Complit script!"); // Впринципи это можно убрать (уточнить вопрос про /q echo off)
                }
                catch (System.Threading.Tasks.TaskCanceledException)
                {
                    if (proc != null)
                    {
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                }
            }
            else MessageBox.Show("Отсутствует путь к бд или скрипту!!!");
            CanStop = false;
            PbVisibility = "Hidden";
        }
        private void StopExamination()
        {
            PbVisibility = "Hidden";
            source?.Cancel();
        }
    }
}
