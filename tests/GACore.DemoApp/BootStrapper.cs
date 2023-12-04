using System.Runtime.Versioning;

namespace GACore.DemoApp;

[SupportedOSPlatform("windows")]
public static class BootStrapper
{
    public static void Activate()
    {
        ViewModel.ViewModelLocator.FooBoolObjViewModel.Model = new Model.FooBoolObj();
    }
}