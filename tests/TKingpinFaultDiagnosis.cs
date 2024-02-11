using GAAPICommon.Architecture;
using NUnit.Framework;
using GAAPICommon.Core.Dtos;

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

    private static IKingpinState GetNoFaultState()
    {
        return new KingpinStateDto()
        {
            DynamicLimiterStatus = DynamicLimiterStatus.OK,
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.OK,
            NavigationStatus = NavigationStatus.OK,
            PositionControlStatus = PositionControlStatus.OK
        };
    }

    private static IKingpinState GetMotorLostFault()
    {
        return new KingpinStateDto()
        {
            DynamicLimiterStatus = DynamicLimiterStatus.MotorFault,
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.OK,
            NavigationStatus = NavigationStatus.Lost,
            PositionControlStatus = PositionControlStatus.OK
        };
    }

    private static IKingpinState GetMotorFault()
    {
        return new KingpinStateDto()
        {
            DynamicLimiterStatus = DynamicLimiterStatus.MotorFault,
            ExtendedDataFaultStatus = ExtendedDataFaultStatus.OK,
            NavigationStatus = NavigationStatus.OK,
            PositionControlStatus = PositionControlStatus.OK
        };
    }
}