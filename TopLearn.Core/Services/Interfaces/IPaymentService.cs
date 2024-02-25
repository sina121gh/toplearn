using Dto.Response.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> PaymentRequest(int amount, int transactionId, string description, string email = "", string mobile = "");
        Task<Verification> ValidatePayment(int amount, string authority);
    }
}
