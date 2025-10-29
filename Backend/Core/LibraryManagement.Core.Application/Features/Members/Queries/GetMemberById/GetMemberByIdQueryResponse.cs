namespace LibraryManagement.Core.Application.Features.Members.Queries.GetMemberById
{
    public class GetMemberByIdQueryResponse
    {
        public int Id { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
