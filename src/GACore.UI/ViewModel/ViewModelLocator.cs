using System.Runtime.Versioning;

namespace GACore.UI.ViewModel;

[SupportedOSPlatform("windows")]
public static class ViewModelLocator
{
    public static IPAddressViewModel IPAddressViewModel { get; } = new IPAddressViewModel();
}