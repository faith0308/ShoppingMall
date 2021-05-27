using Quanzk.ShoppingMall.PaymentServices.Context;
using Quanzk.ShoppingMall.PaymentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.PaymentServices.Repositories
{
    /// <summary>
    /// 支付仓储实现
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _paymentContext;

        public PaymentRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public bool Create(Payment payment)
        {
            var flag = false;
            if (payment != null)
            {
                _paymentContext.Payments.Add(payment);
                if (_paymentContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public bool Delete(Payment payment)
        {
            var flag = false;
            if (payment != null)
            {
                var entity = _paymentContext.Payments.Find(payment.Id);
                if (entity != null)
                {
                    _paymentContext.Payments.Remove(entity);
                    if (_paymentContext.SaveChanges() > 0)  
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        public Payment GetPaymentById(int id)
        {
            var result = _paymentContext.Payments.Find(id);
            return result ?? new Payment();
        }

        public IEnumerable<Payment> GetPayments()
        {
            var result = _paymentContext.Payments.ToList();
            return result ?? new List<Payment>();
        }

        public bool PaymentExists(int id)
        {
            return _paymentContext.Payments.Any(p => p.Id == id);
        }

        public bool Update(Payment payment)
        {
            var falg = false;
            if (payment != null && payment.Id > 0)
            {
                var entity = _paymentContext.Payments.Find(payment.Id);
                if (entity != null)
                {
                    entity.Id = payment.Id;
                    entity.PaymentPrice = payment.PaymentPrice;
                    entity.PaymentType = payment.PaymentType;
                    entity.PaymentMethod = payment.PaymentMethod;
                    entity.Updatetime = DateTime.Now;
                    entity.PaymentRemark = payment.PaymentRemark;
                    entity.PaymentUrl = payment.PaymentUrl;
                    entity.PaymentCode = payment.PaymentCode;
                    entity.UserId = payment.UserId;
                    entity.PaymentErrorNo = payment.PaymentErrorNo;
                    entity.PaymentErrorInfo = payment.PaymentErrorInfo;
                    _paymentContext.Payments.Update(entity);
                    if (_paymentContext.SaveChanges() > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }
    }
}
