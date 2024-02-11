using NUnit.Framework;
using System.Runtime.Versioning;

namespace GACore.Test;

[TestFixture]
public class TTools
{
    [Test]
    [SupportedOSPlatform("windows")]
    public void GetInstalledFleetManagementMetadata()
    {
        // This only works if you have populated:
        // HKEY_LOCAL_MACHINE\SOFTWARE\GuidanceAutomation\Fleet Management\ProductName
        // HKEY_LOCAL_MACHINE\SOFTWARE\GuidanceAutomation\Fleet Management\Version            

        _ = Tools.GetInstalledFleetManagementMetadata();
    }
}