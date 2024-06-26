﻿using Dto.Payment;
using Dto.Response.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Wallet;
using ZarinPal.Class;

namespace TopLearn.Core.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<string> PaymentRequest(int amount, int transactionId ,string description, string email = "", string mobile = "")
        {
            Payment payment = new Payment();
            var result = await payment.Request(new DtoRequestWithExtra()
            {
                Amount = amount,
                CallbackUrl = $"https://localhost:7168/validate-payment/{transactionId}",
                Description = description,
                Email = email,
                Mobile = mobile,
                MerchantId = Environment.GetEnvironmentVariable("ZARINPAL_MERCHANT"),
            }, Payment.Mode.sandbox);

            if (result.Status == 100)
                return result.Authority;

            return "";
        }

        public async Task<Verification> ValidatePayment(int amount, string authority)
        {
            
            Payment payment = new Payment();
            return await payment.Verification(new DtoVerification()
            {
                MerchantId = Environment.GetEnvironmentVariable("ZARINPAL_MERCHANT"),
                Authority = authority,
                Amount = amount
            }, Payment.Mode.sandbox);
        }
    }
}
