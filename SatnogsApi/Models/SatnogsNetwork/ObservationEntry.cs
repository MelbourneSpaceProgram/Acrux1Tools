using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Refit;

namespace SatnogsApi.Models.SatnogsNetwork
{
    public class ObservationEntry
    {
        [AliasAs("id")]
        [JsonProperty("id")]
        [Display(Name = "Observation Id")]
        public long Id { get; set; }

        [AliasAs("start")]
        [JsonProperty("start")]
        [Display(Name = "Start Time")]
        public DateTimeOffset StartDateTime { get; set; }

        [AliasAs("end")]
        [JsonProperty("end")]
        [Display(Name = "End Time")]
        public DateTimeOffset EndDateTime { get; set; }

        [AliasAs("ground_station")]
        [JsonProperty("ground_station")]
        [Display(Name = "Ground Station")]
        public int GroundStation { get; set; }

        [AliasAs("transmitter")]
        [JsonProperty("transmitter")]
        [Display(Name = "Transmitter")]
        public string Transmitter { get; set; }

        [AliasAs("norad_cat_id")]
        [JsonProperty("norad_cat_id")]
        [Display(Name = "NORAD Catalog Id")]
        public int NoradCatId { get; set; }

        [AliasAs("payload")]
        [JsonProperty("payload")]
        [Display(Name = "Payload Url")]
        public string PayloadUrl { get; set; }

        [AliasAs("waterfall")]
        [JsonProperty("waterfall")]
        [Display(Name = "Waterfall Url")]
        public string WaterfallUrl { get; set; }

        [AliasAs("demoddata")]
        [JsonProperty("demoddata")]
        [Display(Name = "Demod Data Urls")]
        public List<DemodDataEntry> DemodData { get; set; }

        [AliasAs("station_name")]
        [JsonProperty("station_name")]
        [Display(Name = "Station Name")]
        public string StationName { get; set; }

        [AliasAs("station_lat")]
        [JsonProperty("station_lat")]
        [Display(Name = "Station Latitude")]
        public double StationLat { get; set; }

        [AliasAs("station_lng")]
        [JsonProperty("station_lng")]
        [Display(Name = "Station Longitude")]
        public double StationLng { get; set; }

        [AliasAs("station_alt")]
        [JsonProperty("station_alt")]
        [Display(Name = "Station Altitude")]
        public double StationAlt { get; set; }

        [AliasAs("vetted_status")]
        [JsonProperty("vetted_status")]
        [Display(Name = "Vetted Status")]
        public string VettedStatus { get; set; }

        [AliasAs("archived")]
        [JsonProperty("archived")]
        [Display(Name = "Archived")]
        public bool Archived { get; set; }

        [AliasAs("archive_url")]
        [JsonProperty("archive_url")]
        [Display(Name = "Archive Url")]
        public string ArchiveUrl { get; set; }

        [AliasAs("client_version")]
        [JsonProperty("client_version")]
        [Display(Name = "Client Version")]
        public string ClientVersion { get; set; }

        [AliasAs("client_metadata")]
        [JsonProperty("client_metadata")]
        [Display(Name = "Client Metadata")]
        public string ClientMetadata { get; set; }

        [AliasAs("vetted_user")]
        [JsonProperty("vetted_user")]
        [Display(Name = "Vetted User")]
        public string VettedUser { get; set; }

        [AliasAs("vetted_datetime")]
        [JsonProperty("vetted_datetime")]
        [Display(Name = "Vetted DateTime")]
        public DateTimeOffset VettedDateTime { get; set; }

        [AliasAs("rise_azimuth")]
        [JsonProperty("rise_azimuth")]
        [Display(Name = "Rise Azimuth")]
        public double RiseAzimuth { get; set; }

        [AliasAs("set_azimuth")]
        [JsonProperty("set_azimuth")]
        [Display(Name = "Set Azimuth")]
        public double SetAzimuth { get; set; }

        [AliasAs("max_altitude")]
        [JsonProperty("max_altitude")]
        [Display(Name = "Max Altitude")]
        public double MaxAltitude { get; set; }

        [AliasAs("transmitter_uuid")]
        [JsonProperty("transmitter_uuid")]
        [Display(Name = "Transmitter UUID")]
        public string TransmitterUuid { get; set; }

        [AliasAs("transmitter_description")]
        [JsonProperty("transmitter_description")]
        [Display(Name = "Transmitter Description")]
        public string TransmitterDescription { get; set; }

        [AliasAs("transmitter_type")]
        [JsonProperty("transmitter_type")]
        [Display(Name = "Transmitter Type")]
        public string TransmitterType { get; set; }

        [AliasAs("transmitter_uplink_low")]
        [JsonProperty("transmitter_uplink_low")]
        [Display(Name = "Transmitter Uplink Low")]
        public long? TransmitterUplinkLow { get; set; }

        [AliasAs("transmitter_uplink_high")]
        [JsonProperty("transmitter_uplink_high")]
        [Display(Name = "Transmitter Uplink High")]
        public long? TransmitterUplinkHigh { get; set; }

        [AliasAs("transmitter_uplink_drift")]
        [JsonProperty("transmitter_uplink_drift")]
        [Display(Name = "Transmitter Uplink Drift")]
        public long? TransmitterUplinkDrift { get; set; }

        [AliasAs("transmitter_downlink_low")]
        [JsonProperty("transmitter_downlink_low")]
        [Display(Name = "Transmitter Downlink Low")]
        public long? TransmitterDownlinkLow { get; set; }

        [AliasAs("transmitter_downlink_high")]
        [JsonProperty("transmitter_downlink_high")]
        [Display(Name = "Transmitter Downlink High")]
        public long? TransmitterDownlinkHigh { get; set; }

        [AliasAs("transmitter_downlink_drift")]
        [JsonProperty("transmitter_downlink_drift")]
        [Display(Name = "Transmitter Downlink Drift")]
        public long? TransmitterDownlinkDrift { get; set; }

        [AliasAs("transmitter_mode")]
        [JsonProperty("transmitter_mode")]
        [Display(Name = "Transmitter Mode")]
        public string TransmitterMode { get; set; }

        [AliasAs("transmitter_invert")]
        [JsonProperty("transmitter_invert")]
        [Display(Name = "Transmitter Invert")]
        public bool TransmitterInvert { get; set; }

        [AliasAs("transmitter_baud")]
        [JsonProperty("transmitter_baud")]
        [Display(Name = "Transmitter Baud")]
        public double TransmitterBaud { get; set; }

        [AliasAs("transmitter_updated")]
        [JsonProperty("transmitter_updated")]
        [Display(Name = "Transmitter Updated")]
        public DateTimeOffset TransmitterUpdatedDateTime { get; set; }

        [AliasAs("tle")]
        [JsonProperty("tle")]
        [Display(Name = "TLE")]
        public long Tle { get; set; }

        public class DemodDataEntry
        {
            [AliasAs("payload_demod")]
            [JsonProperty("payload_demod")]
            [Display(Name = "Payload Demod Url")]
            public string PayloadDemodUrl { get; set; }

            [JsonIgnore]
            [Display(Name = "Timestamp")]
            public DateTimeOffset? Timestamp {
                get {

                    //
                    // Try to parse the time out of the Url
                    //
                    // An example of what the Url will look like:
                    // "http://network.satnogs.org/media/data_obs/1592320/data_1592320_2020-01-25T07-13-14"

                    if (Uri.TryCreate(this.PayloadDemodUrl, UriKind.RelativeOrAbsolute, out Uri payloadUri))
                    {
                        string fileName = payloadUri.Segments.LastOrDefault();
                        var matches = Regex.Match(fileName, "data_\\d*_(.*)");
                        if (matches.Success && matches.Groups[1].Success)
                        {
                            string dateTimeString = matches.Groups[1].Value;
                            if (DateTimeOffset.TryParseExact(dateTimeString,
                                "yyyy-MM-ddTHH-mm-ss",
                                CultureInfo.InvariantCulture.DateTimeFormat,
                                DateTimeStyles.AssumeUniversal,
                                out DateTimeOffset dateTimeOffsetResult))
                            {
                                return dateTimeOffsetResult;
                            }
                        }
                    }

                    return null;
                }
            }

            [JsonIgnore]
            [Display(Name = "UNIX Timestamp")]
            public long? UnixTimestamp => Timestamp?.ToUnixTimeSeconds();
        }
    }
}
