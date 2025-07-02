using Microsoft.AspNetCore.DataProtection.Repositories;
using TrabalhoProg.Models;
using TrabalhoProg.Repository;
using TrabalhoProg.Modelo;


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
FillAddressData();
FillCategoryData();
FillPropertyData();
app.Run();

static void FillCustomerData() {
    for (int i = 1; i <= 9; i++) {
        Customer customer = new Customer()
        {
            CustomerId = i,
            Name = $"Customer {i}",
            Email = $"Customer {i}",
            Phone = $"{i}"
        };

        CustomerData.Customers.Add(customer);
    }
}

static void FillCategoryData()
{
    for (int i = 1; i <= 5; i++)
    {
        Category category = new()
        {
            CategoryId = i,
            Name = $"Category {i}",
            Description = $"Cate {i}"
        };
        CategoryData.Categories.Add(category);
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

static void FillPropertyData()
{
    for (int i = 1; i <= 10; i++)
    {
        Property property = new()
        {
            PropertyId = i,
            Name = $"Casa do elto",
            Description = $"era uma casa muito engracado",
            BedRooms = 3,
            GarageVacancies = 1,
            CurrentPricePerNight = 100,
            Address = AddressData.Addresses[i % AddressData.Addresses.Count],
            Category = CategoryData.Categories[i % CategoryData.Categories.Count]
        };
        PropertyData.RealStates.Add(property);
    }
}