namespace Microsoft.Streamye.PayServices.Payments
{
    public interface IWebPagePay
    {
        public WebPagePayResult CreatePay(string productName, string orderSn, string totalPrice);
    }
}