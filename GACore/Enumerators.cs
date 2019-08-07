﻿using System.Runtime.Serialization;

namespace GACore
{
    public enum LightState
    {
        Off = 0,
        Red = 1,
        Amber = 2,
        Green = 3       
    }


    [DataContract]
    public enum DynamicLimiterStatus : ushort
    {
        [EnumMember]
        OK = 0,

        [EnumMember]
        SafetySensor = 1,

        [EnumMember]
        Warning_1 = 2,

        [EnumMember]
        Warning_2 = 3,

        [EnumMember]
        MotorFault = 4,

        [EnumMember]
        FastStop = 5,

        [EnumMember]
        GoSlow = 6,

        [EnumMember]
        Unknown = 65535
    }

    [DataContract]
    public enum NavigationStatus : ushort
    {
        [EnumMember]
        OK = 0,

        [EnumMember]
        Lost = 1,

        [EnumMember]
        AssociationFailure = 2,

        [EnumMember]
        HighUncertainty = 3,

        [EnumMember]
        PoorAssociaton = 4,

        [EnumMember]
        NoResponse = 5,

        [EnumMember]
        Unknown = 65535
    }

    [DataContract]
    public enum PositionControlStatus : ushort
    {
        [EnumMember]
        OK = 0,

        [EnumMember]
        Disabled = 1,

        [EnumMember]
        Disabling = 2,

        [EnumMember]
        NoWaypoints = 3,

        [EnumMember]
        OutOfPosition = 4,

        [EnumMember]
        WaypointDiscontinuity = 5,

        [EnumMember]
        Unknown = 65535
    }
}