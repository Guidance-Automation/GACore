using System.Runtime.Versioning;

namespace GACore.DemoApp.ViewModel;

[SupportedOSPlatform("windows")]
public static class ViewModelLocator
{
    public static FooBoolObjViewModel FooBoolObjViewModel { get; } = new FooBoolObjViewModel();
}
