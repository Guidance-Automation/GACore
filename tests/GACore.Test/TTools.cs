using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
public class TTools
{
    [Test]
    public void GetInstalledFleetManagementMetadata()
    {
        // This only works if you have populated:
        // HKEY_LOCAL_MACHINE\SOFTWARE\GuidanceAutomation\Fleet Management\ProductName
        // HKEY_LOCAL_MACHINE\SOFTWARE\GuidanceAutomation\Fleet Management\Version            

        FleetManagementMetadata metadata = GACore.Tools.GetInstalledFleetManagementMetadata();
    }
}
