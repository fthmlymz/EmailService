namespace EmailService.Dtos
{
    public class ProductTransferDto
    {
        public string? ProductName { get; set; }
        public string? ProductBarcode { get; set; }

        public string? SenderUserName { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderCompanyName { get; set; }



        public string? RecipientUserName { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientCompanyName { get; set; }




        public string? Status { get; set; }
        public string? WorkflowId { get; set; }
        public string? EventData { get; set; }




        #region Relationship - Affiliated with the upper class
        public int ProductId { get; set; }
        public int CompanyId { get; set; }
        #endregion
    }
}
