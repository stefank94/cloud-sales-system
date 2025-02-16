using CloudSales.Domain.Models;

namespace CloudSales.Infrastructure.Ccp
{
    public class CcpClient : ICcpClient
    {
        public async Task<AvailableSoftware?> GetAvailableSoftwareByIdAsync(Guid id)
        {
            var softwares = await GetAvailableSoftwaresAsync(1);
            return softwares.FirstOrDefault(x => x.Id == id);
        }

        public Task<IEnumerable<AvailableSoftware>> GetAvailableSoftwaresAsync(int page)
        {
            return Task.FromResult<IEnumerable<AvailableSoftware>>(new List<AvailableSoftware>() {
                new AvailableSoftware()
                {
                    Id = Guid.Parse("23f1d53e-0420-4184-9bfc-e3e923e735a2"),
                    Name = "Microsoft Office"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("98034514-a856-492d-97c0-c273d11822b9"),
                    Name = "Product 1"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("544534df-b150-4ff9-a9bc-2bb0f301aade"),
                    Name = "Product 2"
                },
                
                new AvailableSoftware()
                {
                    Id = Guid.Parse("d7d3ad40-0cc1-40f5-85f7-4446c6796f25"),
                    Name = "Product 3"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("d87a218d-9430-4e88-a805-d835f525754c"),
                    Name = "Product 4"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("f540a815-eb46-4c6f-a812-198aee94c335"),
                    Name = "Product 5"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("7b2b0ee7-223f-4fad-b730-2f592fda72f7"),
                    Name = "Product 6"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("7d07fd25-f5e0-4465-ae8d-c810a4956343"),
                    Name = "Product 7"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("ba40d47f-bcf0-40c8-89ca-b8a989dc5191"),
                    Name = "Product 8"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("d6ffc2f0-74b8-482a-a9c2-4f08e1856de0"),
                    Name = "Product 9"
                },

                new AvailableSoftware()
                {
                    Id = Guid.Parse("d00e0ec0-0acd-44c7-b681-64d280e93325"),
                    Name = "Product 10"
                }
            });
        }

        public Task<OrderResult> OrderSoftwareAsync(Order order)
        {
            return Task.FromResult(new OrderResult() { OrderId = Guid.NewGuid(), IsSuccessful = true });
        }
    }
}
