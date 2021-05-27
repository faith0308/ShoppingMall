using Quanzk.ShoppingMall.PaymentServices.Models;
using Quanzk.ShoppingMall.PaymentServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.PaymentServices.Services
{
    /// <summary>
    /// 支付服务实现
    /// </summary>
    public class PaymentServiceImpl : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentServiceImpl(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public bool Create(Payment payment)
        {
            return _paymentRepository.Create(payment);
        }

        public bool Delete(Payment payment)
        {
            return _paymentRepository.Delete(payment);
        }

        public Payment GetPaymentById(int id)
        {
            return _paymentRepository.GetPaymentById(id);
        }

        public IEnumerable<Payment> GetPayments()
        {
            return _paymentRepository.GetPayments();
        }

        public bool PaymentExists(int id)
        {
            return _paymentRepository.PaymentExists(id);
        }

        public bool Update(Payment payment)
        {
            return _paymentRepository.Update(payment);
        }
    }
}
