using ERPtask.models;
using ERPtask.Repositrories;
using ERPtask.Repositrories.Interfaces;
using ERPtask.servcies;
using ERPtask.servcies.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ERPtask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure database connection
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddTransient<ICostEntryRepository>(sp => new CostEntryRepository(connectionString));
            builder.Services.AddTransient<ICostEntryService, CostEntryService>();

            builder.Services.AddTransient<IClientRepository>(sp => new ClientRepository(connectionString));
            builder.Services.AddTransient<IClientService, ClientService>();

            builder.Services.AddTransient<IInvoiceRepository>(sp => new InvoiceRepository(connectionString));
            builder.Services.AddTransient<IInvoiceService, InvoiceService>();

            builder.Services.AddTransient<IInvoiceItemRepository>(sp => new InvoiceItemRepository(connectionString));
            builder.Services.AddTransient<IInvoiceItemService, InvoiceItemService>();

            builder.Services.AddTransient<INotificationRepository>(sp =>
            new NotificationRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
                    builder.Services.AddTransient<INotificationService, NotificationService>();

            builder.Services.AddTransient<ITaxRuleRepository>(sp =>
                new TaxRuleRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
                        builder.Services.AddTransient<ITaxRuleService, TaxRuleService>();

            // Build the application
            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cost Management API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            
            app.Run();
        }     
    }
}
