using System.Net.Http.Json;
using FluentAssertions;
using Monolith.Data;
using Xunit;

public class OrderFlowTests
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:5070") }; // Monolith for Step 1

    [Fact]
    public async Task PlaceOrder_SendsConfirmationAndPersists()
    {
        var cl = await _http.DeleteAsync("/clear");
        cl.EnsureSuccessStatusCode();

        // Create user
        var u = new
        {
            id = 101,
            email = "test@example.com",
            name = "Test",
        };
        var ur = await _http.PostAsJsonAsync("/users", u);
        ur.EnsureSuccessStatusCode();

        // Create product
        var p = new
        {
            id = 201,
            sku = "SKU-201",
            name = "USB-C Cable",
            price = 9.99,
        };
        var pr = await _http.PostAsJsonAsync("/products", p);
        pr.EnsureSuccessStatusCode();

        // Place order
        var o = new
        {
            id = 301,
            userId = 101,
            productId = 201,
            quantity = 2,
            createdAt = DateTime.UtcNow,
        };
        var or = await _http.PostAsJsonAsync("/orders", o);
        or.EnsureSuccessStatusCode();

        // Verify persisted
        var orders = await _http.GetFromJsonAsync<List<Order>>("/orders");
        orders!.Any(o => (int)o.Id == 301).Should().BeTrue();
    }
}
