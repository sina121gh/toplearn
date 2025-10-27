using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Payment
{
    public class PaymentResponse
    {
        [JsonPropertyName("data")]
        public PaymentResponseData Data { get; set; }

        [JsonPropertyName("error")]
        public List<string> Errors { get; set; }
    }

    public class PaymentResponseData
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("authority")]
        public string Authority { get; set; }

        [JsonPropertyName("fee_type")]
        public string FeeType { get; set; }

        [JsonPropertyName("fee")]
        public int Fee { get; set; }
    }
}
