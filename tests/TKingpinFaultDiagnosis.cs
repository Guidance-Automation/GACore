
using GAAPICommon.Enums;
using GAAPICommon.Messages;
using NUnit.Framework;

namespace GACore.Test;

[TestFixture]
[Description("Kingpin Fault Diagnosis")]
public class TKingpinFaultDiagnosis
{
    [Test]
    public void CheckMotorLostFault()
    {
        KingpinFaultDiagnosis diagnosis = new(GetMotorLostFault());

        Assert.That(diagnosis.IsInFault());

        Assert.That(diagnosis.DynamicLimiterFault, Is.Not.Null);
        Assert.That(diagnosis.ExtendedDataFault, Is.Null);
        Assert.That(diagnosis.NavigationFault, Is.Not.Null);
        Assert.That(diagnosis.PCSFault, Is.Null);

        Assert.That(diagnosis, Is.EqualTo(GetMotorLostFault().Diagnose()));
        Assert.That(diagnosis, Is.Not.EqualTo(GetMotorFault().Diagnose()));
        Assert.That(diagnosis, Is.Not.EqualTo(GetNoFaultState().Diagnose()));
    }

    [Test]
    public void CheckMotorFault()
    {
        KingpinFaultDiagnosis diagnosis = new(GetMotorFault());

        Assert.That(diagnosis.IsInFault());

        Assert.That(diagnosis.DynamicLimiterFault, Is.Not.Null);
        Assert.That(diagnosis.ExtendedDataFault, Is.Null);
        Assert.That(diagnosis.NavigationFault, Is.Null);
        Assert.That(diagnosis.PCSFault, Is.Null);

        Assert.That(diagnosis, Is.EqualTo(GetMotorFault().Diagnose()));
        Assert.That(diagnosis, Is.Not.EqualTo(GetNoFaultState().Diagnose()));
    }

    [Test]
    public void CheckNoFault()
    {
        KingpinFaultDiagnosis diagnosis = new(GetNoFaultState());

        Assert.That(!diagnosis.IsInFault());

        Assert.That(diagnosis.DynamicLimiterFault, Is.Null);
        Assert.That(diagnosis.ExtendedDataFault, Is.Null);
        Assert.That(diagnosis.NavigationFault, Is.Null);
        Assert.That(diagnosis.PCSFault, Is.Null);

        Assert.That(diagnosis, Is.EqualTo(GetNoFaultState().Diagnose()));
    }

    private static KingpinStateDto GetNoFaultState()
    {
        return new KingpinStateDto()
        {
            DynamicLimiterStatus = DynamicLimiterStatus.Ok,
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.NoFault,
            NavigationStatus = NavigationStatus.Oknavigation,
            PositionControlStatus = PositionControlStatus.Okposition
        };
    }

    private static KingpinStateDto GetMotorLostFault()
    {
        return new KingpinStateDto()
        {
            DynamicLimiterStatus = DynamicLimiterStatus.MotorFault,
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.NoFault,
            NavigationStatus = NavigationStatus.Lost,
            PositionControlStatus = PositionControlStatus.Okposition
        };
    }

    private static KingpinStateDto GetMotorFault()
    {
        return new KingpinStateDto()
        {
            DynamicLimiterStatus = DynamicLimiterStatus.MotorFault,
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.NoFault,
            NavigationStatus = NavigationStatus.Oknavigation,
            PositionControlStatus = PositionControlStatus.Okposition
        };
    }
}