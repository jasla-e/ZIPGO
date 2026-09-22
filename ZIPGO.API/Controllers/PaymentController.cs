using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZIPGO.Application.DTOs.Payment;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(
    CreatePaymentDto dto)
        {
            var userId = GetUserId();

            await _paymentService.CreatePayment(
                userId,
                dto.OrderId,
                dto.PaymentMethod);

            return Ok(new
            {
                message = "Payment created successfully"
            });
        }

        [HttpPost("create-razorpay-order")]
        public async Task<IActionResult> CreateRazorpayOrder(int orderId)
        {
            var userId = GetUserId();

            var razorpayOrderId =
                await _paymentService.CreateRazorpayOrder(
                    userId,
                    orderId);

            return Ok(new
            {
                razorpayOrderId
            });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyPayment(
            VerifyPaymentDto dto)
        {
            var userId = GetUserId();

            await _paymentService.VerifyPayment(
                userId,
                dto.OrderId,
                dto.RazorpayOrderId,
                dto.RazorpayPaymentId,
                dto.RazorpaySignature);

            return Ok(new
            {
                message = "Payment verified successfully"
            });
        }
    }
}