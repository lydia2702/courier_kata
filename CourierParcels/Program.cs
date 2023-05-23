using CourierParcels.Services;
using CourierParcels.Data;
using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<Executor>().Execute();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

IHostBuilder CreateHostBuilder(string[] strings)
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<IParcelOrderService, ParcelOrderService>();
            services.AddTransient<IParcelService, ParcelService>();

            services.AddSingleton<Executor>();
        })
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        });
}

public class Executor
{
    private readonly IParcelOrderService _parcelOrderService;
    private readonly IParcelService _parcelService;

    public Executor(IParcelOrderService parcelOrderService, IParcelService parcelService)
    {
        _parcelOrderService = parcelOrderService;
        _parcelService = parcelService;
    }

    public void Execute()
    {
        var parcelOrder = new ParcelOrder()
        {
            Parcels = new List<Parcel>()
                {
                    _parcelService.NewParcel(1,1,1,1),
                    _parcelService.NewParcel(10,10,10,5),
                    _parcelService.NewParcel(50,50,1,10),
                    _parcelService.NewParcel(10,1,1,51),
                    _parcelService.NewParcel(1,1,1,10),
                },
            IsSpeedyShipping = true
        };
        parcelOrder.CalculateOrderDiscount();
        Console.Write(_parcelOrderService.PrintParcelOrderSummary(parcelOrder));
    }
}