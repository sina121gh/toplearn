using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Payment;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> PaymentRequestAsync(int amount, int transactionId, string description, string email = "", string mobile = "");
        Task<VerifyPaymentResponse> ValidatePaymentAsync(int amount, string authority);
    }
}
