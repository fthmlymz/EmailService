namespace EmailService.Dtos
{
    public class CreatedAssignedProductDto
    {
        public string? AssignedUserName { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserPhoto { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Barcode { get; set; }


        public int ProductId { get; set; }
        public string? ProductName { get; set; }



        public int Id { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
        public string? CreatedUserId { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }


        public string? UpdatedBy { get; set; }
        public string? UpdatedUserId { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
    }
}
