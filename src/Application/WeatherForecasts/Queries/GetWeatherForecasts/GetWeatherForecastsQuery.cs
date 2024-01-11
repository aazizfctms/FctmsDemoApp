namespace FctmsDemoApp.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public record GetWeatherForecastsQuery() : IRequest<IEnumerable<WeatherForecast>>
{
    public string Weather { get; set; } = "";
    public int? Temp { get; set; }
}

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecast>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var rng = new Random();

        var result = Enumerable.Range(1, 30).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });

        if (string.IsNullOrWhiteSpace(request.Weather) == false)
        {
            result = result.Where(x => request.Weather.Equals(x.Summary, StringComparison.InvariantCultureIgnoreCase));
        }

        if (request.Temp.HasValue)
        {
            result = result.Where(x => x.TemperatureC == request.Temp);
        }

        return result;
    }
}
