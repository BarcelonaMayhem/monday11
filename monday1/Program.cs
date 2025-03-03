var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Toy> repo = [];


app.MapGet("/toys", () => repo);
app.MapPost("/toys/add", (Toy dto) => repo.Add(dto));
app.MapPut("/{id}", (int id, UpdateToyDTO dto) =>
{
    Toy buffer = repo.Find(toy => toy.Price == id);
    if (buffer == null)
    {
        return Results.NotFound();
    }
    if (buffer.Name != dto.Name)
        buffer.Name = dto.Name;
    if (buffer.Color != dto.Color)
        buffer.Color = dto.Color;
    if (buffer.Price != dto.Price)
        buffer.Price = dto.Price;

    return Results.Json(buffer);
});

app.MapDelete("/delete/{id}", (int id) =>
{
    Toy buffer = repo.Find(toy => toy.Price == id);
    repo.Remove(buffer);
});

app.Run();

public class Toy
{
    public string Name { get; set; }
    public string Color { get; set; }
    public int Price { get; set; }
    public DateTime NowDate { get; set; }
};

record class UpdateToyDTO (string Name, string Color, int Price);
