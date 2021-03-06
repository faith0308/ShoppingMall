using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.PaymentServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.PaymentServices.Repositories
{
    /// <summary>
    /// 支付仓储接口
    /// </summary>
    public interface IPaymentRepository:IDependency
    {
        IEnumerable<Payment> GetPayments();
        Payment GetPaymentById(int id);
        bool Create(Payment payment);
        bool Update(Payment payment);
        bool Delete(Payment payment);
        bool PaymentExists(int id);
    }
}
