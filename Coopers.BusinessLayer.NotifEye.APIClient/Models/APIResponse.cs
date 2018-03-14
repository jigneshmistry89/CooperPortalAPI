namespace Coopers.BusinessLayer.NotifEye.APIClient.Models
{
    internal class APIResponse
    {
        public string Method { get; set; }

        public object Result { get; set; }

    }

    internal class APIResponse<T>
    {
        public string Method { get; set; }

        public T Result { get; set; }

    }
}
