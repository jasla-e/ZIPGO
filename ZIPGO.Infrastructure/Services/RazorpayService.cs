using Microsoft.Extensions.Options;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Application.Settings;

namespace ZIPGO.Infrastructure.Services
{
    public class RazorpayService : IRazorpayService
    {
        private readonly RazorpaySettings _settings;

        public RazorpayService(IOptions<RazorpaySettings> settings)
        {
            _settings = settings.Value;
        }

        public Task<string> CreateOrder(decimal amount, int orderId)
        {
            RazorpayClient client = new RazorpayClient(
                _settings.KeyId,
                _settings.KeySecret);

            var options = new Dictionary<string, object>
            {
                { "amount", (int)(amount * 100) },
                { "currency", "INR" },
                { "receipt", $"ZIPGO_{orderId}" }
            };

            Razorpay.Api.Order order = client.Order.Create(options);

            return Task.FromResult(order["id"].ToString());
        }

        public Task<bool> VerifyPayment(
    string razorpayOrderId,
    string razorpayPaymentId,
    string razorpaySignature)
        {
            string message = razorpayOrderId + "|" + razorpayPaymentId;

            using var hmac = new HMACSHA256(
                Encoding.UTF8.GetBytes(_settings.KeySecret));

            byte[] hash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(message));

            string generatedSignature =
                Convert.ToHexString(hash).ToLower();

            bool isValid = generatedSignature == razorpaySignature;

            return Task.FromResult(isValid);
        }
    }
}