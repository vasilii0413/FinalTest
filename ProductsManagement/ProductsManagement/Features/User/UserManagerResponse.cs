namespace ProductsManagement.Features.User
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSucces { get; set; }
        public IEnumerable<string>Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
