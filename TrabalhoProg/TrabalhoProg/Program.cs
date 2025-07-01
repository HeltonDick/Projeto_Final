using Microsoft.AspNetCore.DataProtection.Repositories;
using TrabalhoProg.Models;
using TrabalhoProg.r


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

FillCustomerData();
FillPropertyData();

app.Run();

static void FillCustomerData() {
    for (int i = 1; i <= 9; i++) {
        Customer customer = new Customer()
        {
            CustomerId = i,
            Name = $"Customer {i}",
            BindingAddress = new Address()
            {
                AddressId = i,
                Street = $"Rua de ca",
                Street1 = $"Rua de la",
                City = $"Essa cidade aqui",
                State = $"O estado liquido",
                PostalCode = $"89560-000",
                Country = $"Brazil"
            }
        };

        CustomerData.Customers.Add(customer);
    }
}

static void FillAddressData()
{
    for (int i = 1; i <= 6; i++)
    {
        Address address = new()
        {
            AddressId = i,
            City = "Videira",
            Country = "Brazil",
            State = "Santa-Catarina",
            PostalCode = "99999-99",
            Street = "Esse daqui",
            Street1 = "Aquela la ó",
        };
        AddressData.Addresses.Add(address);
    }
    ;
}