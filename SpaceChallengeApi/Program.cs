using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SpaceChallengeApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SpaceNavigator>(provider =>
{
    var map = LoadMapFromFile("SpaceChallengeApi/mapa.txt");
    return new SpaceNavigator(map);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

char[,] LoadMapFromFile(string filePath)
{
    var lines = System.IO.File.ReadAllLines(filePath);
    int rows = lines.Length;
    int cols = lines[0].Length;
    var map = new char[rows, cols];

    for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
            map[i, j] = lines[i][j];

    return map;
}
