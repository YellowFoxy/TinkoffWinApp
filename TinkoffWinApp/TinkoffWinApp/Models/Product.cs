using System.Collections.Generic;
using Newtonsoft.Json;

namespace TinkoffWinApp.Models
{
    public class Product : ModelBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("programId")]
        public string ProgramId { get; set; }

        [JsonProperty("product")]
        public string ProductStr { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("slogan")]
        public string Slogan { get; set; }

        [JsonProperty("multiCurrencies")]
        public bool MultiCurrencies { get; set; }

        [JsonProperty("benefits")]
        public List<Benefit> Benefits { get; set; }

        [JsonProperty("hrefTariff")]
        public string hrefTariff { get; set; }

        [JsonProperty("bgColor")]
        public string BgColor { get; set; }

        public string BuildImagePath =>
            "https://static.tcsbank.ru/icons/new-products/windows/" + Type + "/400/" + ProgramId + ".png";
    }

    public class Benefit
    {
        [JsonProperty("icon")]
        public BenefitIcon Icon { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class BenefitIcon
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string SomeText => !string.IsNullOrEmpty(Name) ? Name : Text;
    }
}
