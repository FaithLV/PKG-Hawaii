namespace NPSHawaii
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class PSNItem
    {
        [JsonProperty("age_limit")]
        public long AgeLimit { get; set; }

        [JsonProperty("bucket")]
        public string Bucket { get; set; }

        [JsonProperty("cloud_only_platform")]
        public List<string> CloudOnlyPlatform { get; set; }

        [JsonProperty("container_type")]
        public string ContainerType { get; set; }

        [JsonProperty("content_descriptors")]
        public List<ContentDescriptor> ContentDescriptors { get; set; }

        [JsonProperty("content_origin")]
        public long ContentOrigin { get; set; }

        [JsonProperty("content_rating")]
        public ContentRating ContentRating { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("default_sku")]
        public DefaultSku DefaultSku { get; set; }

        [JsonProperty("dob_required")]
        public bool DobRequired { get; set; }

        [JsonProperty("gameContentTypesList")]
        public List<GameContentTypesList> GameContentTypesList { get; set; }

        [JsonProperty("game_contentType")]
        public string GameContentType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("links")]
        public List<object> Links { get; set; }

        [JsonProperty("long_desc")]
        public string LongDesc { get; set; }

        [JsonProperty("media_layouts")]
        public List<MediaLayout> MediaLayouts { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pageTypeId")]
        public long PageTypeId { get; set; }

        [JsonProperty("playable_platform")]
        public List<string> PlayablePlatform { get; set; }

        [JsonProperty("promomedia")]
        public List<Promomedia> Promomedia { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("restricted")]
        public bool Restricted { get; set; }

        [JsonProperty("revision")]
        public long Revision { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("sku_links")]
        public List<object> SkuLinks { get; set; }

        [JsonProperty("skus")]
        public List<DefaultSku> Skus { get; set; }

        [JsonProperty("star_rating")]
        public StarRating StarRating { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("title_name")]
        public string TitleName { get; set; }

        [JsonProperty("top_category")]
        public string TopCategory { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class StarRating
    {
        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("count")]
        public List<Count> Count { get; set; }
    }

    public partial class Count
    {
        [JsonProperty("star")]
        public long Star { get; set; }

        [JsonProperty("count")]
        public long PurpleCount { get; set; }
    }

    public partial class Promomedia
    {
        [JsonProperty("anno")]
        public string Anno { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("materials")]
        public List<Material> Materials { get; set; }

        [JsonProperty("multi")]
        public string Multi { get; set; }

        [JsonProperty("rep")]
        public string Rep { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
    }

    public partial class Material
    {
        [JsonProperty("anno")]
        public string Anno { get; set; }

        [JsonProperty("countries")]
        public List<Country> Countries { get; set; }

        [JsonProperty("from")]
        public DateTime From { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("lang")]
        public List<string> Lang { get; set; }

        [JsonProperty("lastm")]
        public DateTime Lastm { get; set; }

        [JsonProperty("until")]
        public DateTime Until { get; set; }

        [JsonProperty("urls")]
        public List<Image> Urls { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("agelimit")]
        public long Agelimit { get; set; }

        [JsonProperty("uagelimit")]
        public long Uagelimit { get; set; }

        [JsonProperty("country")]
        public string PurpleCountry { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("cn_remotePlay")]
        public CloudOnlyPlatform CnRemotePlay { get; set; }

        [JsonProperty("cloud_only_platform")]
        public CloudOnlyPlatform CloudOnlyPlatform { get; set; }

        [JsonProperty("cn_vrEnabled")]
        public CloudOnlyPlatform CnVrEnabled { get; set; }

        [JsonProperty("secondary_classification")]
        public CloudOnlyPlatform SecondaryClassification { get; set; }

        [JsonProperty("game_subgenre")]
        public CloudOnlyPlatform GameSubgenre { get; set; }

        [JsonProperty("cn_vrRequired")]
        public CloudOnlyPlatform CnVrRequired { get; set; }

        [JsonProperty("game_genre")]
        public CloudOnlyPlatform GameGenre { get; set; }

        [JsonProperty("playable_platform")]
        public CloudOnlyPlatform PlayablePlatform { get; set; }

        [JsonProperty("subgenre")]
        public CloudOnlyPlatform Subgenre { get; set; }

        [JsonProperty("cn_dualshockVibration")]
        public CloudOnlyPlatform CnDualshockVibration { get; set; }

        [JsonProperty("tertiary_classification")]
        public CloudOnlyPlatform TertiaryClassification { get; set; }

        [JsonProperty("genre")]
        public CloudOnlyPlatform Genre { get; set; }

        [JsonProperty("cn_crossPlatformPSVita")]
        public CloudOnlyPlatform CnCrossPlatformPsVita { get; set; }

        [JsonProperty("primary_classification")]
        public CloudOnlyPlatform PrimaryClassification { get; set; }
    }

    public partial class CloudOnlyPlatform
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public List<string> Values { get; set; }
    }

    public partial class MediaLayout
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class GameContentTypesList
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public partial class DefaultSku
    {
        [JsonProperty("amortizeFlag")]
        public bool AmortizeFlag { get; set; }

        [JsonProperty("bundleExclusiveFlag")]
        public bool BundleExclusiveFlag { get; set; }

        [JsonProperty("chargeImmediatelyFlag")]
        public bool ChargeImmediatelyFlag { get; set; }

        [JsonProperty("charge_type_id")]
        public long ChargeTypeId { get; set; }

        [JsonProperty("credit_card_required_flag")]
        public long CreditCardRequiredFlag { get; set; }

        [JsonProperty("defaultSku")]
        public bool PurpleDefaultSku { get; set; }

        [JsonProperty("display_price")]
        public string DisplayPrice { get; set; }

        [JsonProperty("eligibilities")]
        public List<object> Eligibilities { get; set; }

        [JsonProperty("entitlements")]
        public List<Entitlement> Entitlements { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_original")]
        public bool IsOriginal { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("platforms")]
        public List<long> Platforms { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("rewards")]
        public List<object> Rewards { get; set; }

        [JsonProperty("seasonPassExclusiveFlag")]
        public bool SeasonPassExclusiveFlag { get; set; }

        [JsonProperty("skuAvailabilityOverrideFlag")]
        public bool SkuAvailabilityOverrideFlag { get; set; }

        [JsonProperty("sku_type")]
        public long SkuType { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Entitlement
    {
        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("drms")]
        public List<Drm> Drms { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("durationOverrideTypeId")]
        public object DurationOverrideTypeId { get; set; }

        [JsonProperty("exp_after_first_use")]
        public long ExpAfterFirstUse { get; set; }

        [JsonProperty("feature_type_id")]
        public long FeatureTypeId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("license_type")]
        public long LicenseType { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("packageType")]
        public object PackageType { get; set; }

        [JsonProperty("packages")]
        public List<object> Packages { get; set; }

        [JsonProperty("preorder_placeholder_flag")]
        public bool PreorderPlaceholderFlag { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("subType")]
        public long SubType { get; set; }

        [JsonProperty("subtitle_language_codes")]
        public object SubtitleLanguageCodes { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("use_count")]
        public long UseCount { get; set; }

        [JsonProperty("voice_language_codes")]
        public object VoiceLanguageCodes { get; set; }
    }

    public partial class Drm
    {
        [JsonProperty("drm_category_type")]
        public long DrmCategoryType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_streamable")]
        public long IsStreamable { get; set; }

        [JsonProperty("media_prop")]
        public MediaProp MediaProp { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
    }

    public partial class MediaProp
    {
    }

    public partial class ContentRating
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rating_system")]
        public string RatingSystem { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class ContentDescriptor
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class PSNItem
    {
        public static PSNItem FromJson(string json) => JsonConvert.DeserializeObject<PSNItem>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PSNItem self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
