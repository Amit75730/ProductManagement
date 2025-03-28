// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.DependencyInjection;
// using System.Threading;
// using System.Threading.Tasks;
// using ProductManagementAPI.Services.Interfaces;

// namespace ProductManagementAPI.BackgroundServices
// {
//     public class ProductUnlockService : BackgroundService
//     {
//         private readonly IServiceProvider _serviceProvider;

//         public ProductUnlockService(IServiceProvider serviceProvider)
//         {
//             _serviceProvider = serviceProvider;
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             // Create a scope to resolve scoped services
//             using (var scope = _serviceProvider.CreateScope())
//             {
//                 var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

//                 // Your logic for checking expired locked products
//                 productService.UnlockExpiredLockedProducts();
//             }

//             // Sleep for the interval, then repeat
//             await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
//         }
//     }
// }
