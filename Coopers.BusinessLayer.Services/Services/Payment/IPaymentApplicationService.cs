using Coopers.BusinessLayer.Model.DTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IPaymentApplicationService
    {
        Task<Transaction> GetPaymentDetails(string ProductName = "");

        Task<object> ExecutePayment(Payment Payment);

        Task<byte[]> GenerateInvoice(long PatmentHistoryID);

        Task<List<PaymentHistoryInfo>> GetPaymentHistoryList(int Offset, int PageSize);
    }
}
