
namespace EBird.BusinessEntity
{
    public class CountryMasterBE
    {
        public int CountryID { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class IndustryMasterBE
    {
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
    }

    public class ThemeMasterBE
    {
        public int ThemeID { get; set; }
        public string ThemeName { get; set; }
        public string ThemeShortName { get; set; }
        public string ThemeCategory { get; set; }
        public string ThumbNail { get; set; }
        public string ThemeStatus { get; set; }
    }

    public class RegistrationFormFieldBE
    {
        public int FieldID { get; set; }
        public string FieldName { get; set; }
        public string FieldDisplay { get; set; }
        public string ContainerID { get; set; }
        public int WebinarID { get; set; }
        public bool isRequired { get; set; }
    }

    public class PackageFeature
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public bool isConfig { get; set; }
        public string Category { get; set; }
        public bool IsPremium { get; set; }
        public string FeatureDesc { get; set; }
    }

    public class TimeZoneBE
    {
        public int ZoneID { get; set; }
        public double TimeZoneDiff { get; set; }
        public string TimeZoneName { get; set; }
        public string ShortTimeZoneName { get; set; }
    }

    public class MetaTagBE
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
    }

}
