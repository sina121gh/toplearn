using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Payment
{
    public class VerifyPaymentResponse
    {
        [JsonPropertyName("data")]
        public VerifyPaymentData Data { get; set; }

        [JsonPropertyName("error")]
        public List<string> Errors { get; set; }
    }

    public class VerifyPaymentData
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }


        [JsonPropertyName("message")]
        public string Message { get; set; }


        [JsonPropertyName("card_hash")]
        public string CardHash { get; set; }


        [JsonPropertyName("card_pan")]
        public string CardPan { get; set; }


        [JsonPropertyName("ref_id")]
        public int RefId { get; set; }


        [JsonPropertyName("fee_type")]
        public string FeeType { get; set; }


        [JsonPropertyName("fee")]
        public int Fee { get; set; }
    }
}
