using System.Threading.Tasks;
using System.Windows;

namespace Wpf.Mvvm
{
    public class WindowConductor
    {
        private IWindow _viewModel;
        private Window _view;
        private bool _actuallyClosing;

        public WindowConductor(IWindow viewModel, Window view)
        {
            _viewModel = viewModel;
            _view = view;
        }

        public void Initialize()
        {
            _view.DataContext = _viewModel;
            _view.Loaded += View_Loaded;
            _view.Closing += View_Closing;

            _viewModel.CloseWnd = () => _view.Close();
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.LoadedCommand != null)
                await _viewModel.LoadedCommand.ExecuteAsync(null);
        }

        private async void View_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_actuallyClosing)
            {
                _actuallyClosing = false;
                return;
            }

            e.Cancel = true;
            await Task.Yield();

            if (_viewModel.ClosingCommand != null)
            {
                await _viewModel.ClosingCommand.ExecuteAsync(null);
            }

            bool canClose = _viewModel.CanClose;
            if (canClose)
            {
                _actuallyClosing = true;
                _view.Close();
            }
        }
    }
}
