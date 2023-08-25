using DotNetCore.CAP;
using EmailService.Dtos;
using EmailService.Services;

namespace EmailService.Consumers
{
    public class ProductTransferConsumer : ICapSubscribe
    {
        private readonly IMailService _mailService;

        public ProductTransferConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }



        [CapSubscribe("product.transfer.transaction")]
        public async void ProductTransfer(ProductTransferDto dto)
        {
            if (dto.Status == "rejected")
            {
                Console.WriteLine($"Product transfer REJECTED : " + System.Text.Json.JsonSerializer.Serialize(dto));
            }
            else if (dto.Status == "transfer")
            {
                Console.WriteLine($"Product transfer TRANSFEr : " + System.Text.Json.JsonSerializer.Serialize(dto));
            }
            else if (dto.Status == "accepted")
            {
                Console.WriteLine($"Product transfer ACCEPTED : " + System.Text.Json.JsonSerializer.Serialize(dto));
            }
        }




    }
}
