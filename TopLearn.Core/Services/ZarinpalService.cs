using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Payment;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services
{
    public class ZarinpalService : IPaymentService
    {

        private readonly string MerchantId = Environment.GetEnvironmentVariable("GATEWAY_MERCHANT_ID");
        private readonly string CallBackUrl = Environment.GetEnvironmentVariable("GATEWAY_CALLBACK_URL");
        private readonly string GatewayUrlToConnect = Environment.GetEnvironmentVariable("GATEWAY_URL_TO_CONNECT");
        private readonly string GatewayUrlToVerify = Environment.GetEnvironmentVariable("GATEWAY_URL_TO_VEIRIFY");

        public async Task<string> PaymentRequestAsync(int amount, int transactionId, string description, string email = "", string mobile = "")
        {
            HttpClient client = new();

            var data = new
            {
                merchant_id = MerchantId,
                amount = amount,
                description = description,
                callback_url = $"{CallBackUrl}/{transactionId}",
                //metadata = new
                //{
                //    mobile = mobile,
                //    email = email
                //}
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(GatewayUrlToConnect, content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PaymentResponse>(responseString);

            if (result.Data.Code == 100)
                return result.Data.Authority;

            return "";
        }

        public async Task<VerifyPaymentResponse> ValidatePaymentAsync(int amount, string authority)
        {
            HttpClient client = new HttpClient();

            var data = new
            {
                merchant_id = MerchantId,
                amount = amount,
                authority = authority,
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(GatewayUrlToVerify, content);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<VerifyPaymentResponse>(responseString);

            return result;
        }
    }
}
