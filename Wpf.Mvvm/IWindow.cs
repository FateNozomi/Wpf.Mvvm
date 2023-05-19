using System;
using CommunityToolkit.Mvvm.Input;

namespace Wpf.Mvvm
{
    public interface IWindow
    {
        bool? WindowResult { get; }

        bool CanClose { get; }

        bool IsEnabled { get; set; }

        Action? CloseWnd { get; set; }

        IAsyncRelayCommand? LoadedCommand { get; }

        IAsyncRelayCommand? ClosingCommand { get; }
    }
}
