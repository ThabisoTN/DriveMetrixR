namespace ProductAuthenticatorApp.Data
{
    public class Product
    {
        public int Id { set; get; }

        public string ProductName { set;get; }
        public string ProductDescription { set;get; }
        public int SerialNumber { set;get; }

        //Foreign Keys
        public string UserId { set; get; }
    }
}
