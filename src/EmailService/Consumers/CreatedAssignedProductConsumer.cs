using DotNetCore.CAP;
using EmailService.Dtos;
using EmailService.Services;

namespace EmailService.Consumers
{
    public class CreatedAssignedProductConsumer : ICapSubscribe
    {
        private readonly IMailService _mailService;

        public CreatedAssignedProductConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }


        [CapSubscribe("product.assigned.transaction")]
        public async void CreatedAssignedProduct(CreatedAssignedProductDto dto)
        {
            string htmlContent = $@"
                <html>
                    <body>
                        <p>Sayın <strong>{dto.FullName}</strong>,</p>
                        <p>Tarafınıza <strong>{dto.Barcode}</strong> numaralı ürün zimmetlendi.</p>
                        </br>
                        <p><u>Ürün bilgileri aşağıdaki gibidir.</u></p>
                        </br>
                        <p>Ürün Id  : <strong>{dto.ProductId}</strong></p>
                        <p>Ürün Adı : <strong>{dto.ProductName}</strong></p>
                        <p>Barkod   : <strong>{dto.Barcode}</strong></p>
                        
                        </br>
                        </br>
                        <p>Sisteme girip onay vermeniz gerekmektedir.</p>
                    </body>
                </html>
            ";
            await _mailService.SendEmailAsync($"{dto.Email}", $"Ürün Zimmeti - {dto.Barcode}", htmlContent);
        }
    }
}
