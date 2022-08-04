using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

public enum SubscriptionStatus
{
    Future, // The subscription is scheduled to start at a future date.
    InTrial, // The subscription is in trial.
    Active, // The subscription is active and will be charged for automatically based on the items in it.
    NotRenewing, // The subscription will be canceled at the end of the current term.
    Paused, // The subscription is paused. The subscription will not renew while in this state.
    Cancelled, // The subscription has been canceled and is no longer in service.
}

public enum ItemStatus
{
    Active,
    Archived,
    Deleted,
}

public enum ProductFamilyStatus
{
    Active,
    Deleted,
}

public enum BillingPeriodUnit
{
    Day,
    Week,
    Month,
    Year
}

public enum ProductItemType
{
    Plan,
    AddOn,
    Charge,
}
