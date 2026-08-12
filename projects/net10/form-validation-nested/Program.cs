using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();

builder.Services.AddValidation();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseAntiforgery();

app.MapGet("/", () =>
{
    var html = """
    <html>
    <body>
        <h1>Form Validation - Nested Objects</h1>
        <form method="post" action="/submit">
            <fieldset>
                <legend>Customer Info</legend>
                <input name="Customer.Name" placeholder="Name (min 2 chars)" required /><br>
                <input name="Customer.Email" type="email" placeholder="Email" required />
            </fieldset>
            <fieldset>
                <legend>Order Item</legend>
                <input name="Items[0].ProductName" placeholder="Product Name" required /><br>
                <input name="Items[0].Quantity" type="number" placeholder="Quantity (1-10)" min="1" max="10" />
            </fieldset>
            <button type="submit">Submit Order</button>
        </form>
        <p><a href="/submit?Customer.Name=J&Customer.Email=invalid&Items[0].ProductName=X&Items[0].Quantity=20">Test invalid data</a></p>
    </body>
    </html>
    """;

    return TypedResults.Content(html, "text/html");
});

app.MapPost("/submit", ([FromForm] Order order) =>
{
    return TypedResults.Ok(new
    {
        Message = "Order submitted successfully!",
        Customer = order.Customer,
        Items = order.Items
    });
}).DisableAntiforgery();

app.Run();

public class Order
{
    [Required]
    public CustomerInfo Customer { get; set; } = new();

    [Required, MinLength(1)]
    public List<OrderItem> Items { get; set; } = [];
}

public class CustomerInfo
{
    [Required, MinLength(2)]
    public string Name { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";
}

public class OrderItem
{
    [Required]
    public string ProductName { get; set; } = "";

    [Range(1, 10)]
    public int Quantity { get; set; } = 1;
}
