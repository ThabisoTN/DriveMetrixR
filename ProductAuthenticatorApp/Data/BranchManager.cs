namespace ProductAuthenticatorApp.Data
{
    public class BranchManager
    {
        public int BranchManagerId { get; set; }    
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public string ApplicationuserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
