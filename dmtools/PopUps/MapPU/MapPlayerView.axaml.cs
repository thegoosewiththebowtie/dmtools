using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace dmtools.PopUps.MapPU;

public partial class MapPlayerView : Window
{
    public MapPlayerView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}