using System.Runtime.Versioning;
using System.Windows;
using GACore.Controls.ViewModel;
using GACore.UI.ViewModel;
using NLog;

namespace GACore.DemoApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[SupportedOSPlatform("windows")]
public partial class MainWindow : Window
{
    public MainWindow()
    {
        BootStrapper.Activate();

        InitializeComponent();

        NLog.NLogManager.Instance.LogLevel = LogLevel.Trace;
        HandleViewModels();
    }

    private void HandleViewModels()
    {
        FooKingpin kingpin = (FooKingpin)FindResource("fooKingpin");
        KingpinStateReporterViewModel viewModel = ViewModelFactory.GetKingpinStateReporterViewModel(kingpin);

        kingpinStateView.DataContext = viewModel;
    }

    private void RandomizeButton_Click(object sender, RoutedEventArgs e)
    {
        FooKingpin kingpin = (FooKingpin)FindResource("fooKingpin");
        kingpin.Randomize();

        FooCallButton callButton = (FooCallButton)FindResource("fooCallButton");
        callButton.Randomize();

        FooIPAddressable fooIPAddressable = (FooIPAddressable)FindResource("fooIPAddressable");
        fooIPAddressable.Randomize();

        ViewModelLocator.IPAddressViewModel.Model = fooIPAddressable;
    }

    private void GoodButton_Click(object sender, RoutedEventArgs e)
    {
        FooKingpin kingpin = (FooKingpin)FindResource("fooKingpin");
        kingpin.SetGood();
    }

    private void wizardButton_Click(object sender, RoutedEventArgs e)
    {
        Service.DialogService.CreateFooWizardWindow().ShowDialog();
    }
}