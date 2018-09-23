using Newtonsoft.Json;

namespace TinkoffWinApp.Models.ResponceModels
{
    public interface IResponce
    {
        bool Success { get; set; }
        string ErrorReason { get; set; }
    }

    public class EmptyResponceModel: IResponce
    {
        public bool Success { get; set; } = true;

        public string ErrorReason { get; set; }
    }

    public class ResponceModel<T> :EmptyResponceModel
        where T : class
    {        
        [JsonProperty("result")]
        public ResponceModelResult<T> Result { get; set; }
    }

    public class ResponceModelResult<T>
        where T : class
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public T Value { get; set; }
    }
}
