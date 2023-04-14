namespace ChildCare.DTOs
{
    public class UserListRequestDto
    {
        public int pagesize { get; set; }
        public int pageNumber { get; set; }
        public int sortColumnIndex { get; set; }
        public string? sortDirection { get; set; }
        public string? title { get; set; }



    }
}

