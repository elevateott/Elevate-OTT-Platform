using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElevateOTT.Domain.Enums;

// TODO group enums in separate files

public enum ContentAccess
{
    Free,
    Premium
}

public enum PublicationStatus
{
    Unpublished,
    Published,
    Scheduled
}

public enum SubscriptionAvailability
{
    Public,
    Private
}

public enum BundleType
{
    FixedPrice,
    Rental,
    Freebie
}

public enum StreamType
{
    Hls,
    Dash,
    SmoothStreaming
}

public enum VideoResolutionType
{
    Sd,
    Hd,
    Fhd
}

public enum DistributionType
{
    Web,
    AndroidMobile,
    iOSMobile,
    Roku,
    FireTV,
    AndroidTV,
    Tizen,
    tvOS,
    LGwebOS,
    WindowsDesktop,
    MacDesktop
}

public enum AssetCreationStatus
{
    None,
    Preparing,
    Ready,
    Errored,
    Deleted
}

public enum DirectUploadStatus
{
    None,
    Waiting,
    UploadCreated,
    UploadAssetCreated,
    Cancelled,
    TimedOut,
    AssetReady,
    Errored
}

public enum AssetType
{
    Video,
    Podcast
}

public enum LiveStreamStatus
{
    Active,
    Idle,
    Disabled
}

public enum LatencyMode
{
    Low,
    Reduced,
    Standard
}

[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum SubscriptionStatus
{
    [EnumMember(Value = "future")] Future, // The subscription is scheduled to start at a future date.
    [EnumMember(Value = "in_trial")] InTrial, // The subscription is in trial.
    [EnumMember(Value = "active")] Active, // The subscription is active and will be charged for automatically based on the items in it.
    [EnumMember(Value = "not_renewing")] NotRenewing, // The subscription will be canceled at the end of the current term.
    [EnumMember(Value = "paused")] Paused, // The subscription is paused. The subscription will not renew while in this state.
    [EnumMember(Value = "cancelled")] Cancelled, // The subscription has been canceled and is no longer in service.
}

[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum ItemStatus
{
    [EnumMember(Value = "active")] Active,
    [EnumMember(Value = "archived")] Archived,
    [EnumMember(Value = "deleted")] Deleted,
}

[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum ProductFamilyStatus
{
    [EnumMember(Value = "active")] Active,
    [EnumMember(Value = "deleted")] Deleted,
}

[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum BillingPeriodUnit
{
    [EnumMember(Value = "day")] Day,
    [EnumMember(Value = "week")] Week,
    [EnumMember(Value = "month")] Month,
    [EnumMember(Value = "year")] Year
}


[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum ItemType
{
    [EnumMember(Value = "plan")] Plan,
    [EnumMember(Value = "addon")] AddOn,
    [EnumMember(Value = "charge")] Charge,
}

[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum PlanStatus
{
    [EnumMember(Value = "active")] Active,
    [EnumMember(Value = "archived")] Archived,
    [EnumMember(Value = "deleted")] Deleted,
}

public enum ProductItemType
{
    Plan,
    AddOn,
    Charge,
}
