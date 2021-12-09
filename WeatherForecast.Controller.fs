namespace FsharpSwaggerControllers

open System
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("[controller]")>]
type WeatherForecastController () =
    inherit ControllerBase ()

    let summaries =
        [
            "Freezing"
            "Bracing"
            "Chilly"
            "Cool"
            "Mild"
            "Warm"
            "Balmy"
            "Hot"
            "Sweltering"
            "Scorching"
        ]

    /// Get the weather forecast
    [<HttpGet>]
    member _.Get () =
        let rng = Random()
        [
            for index in 0..4 ->
                { Date = DateTime.Now.AddDays (float index)
                  TemperatureC = rng.Next (-20,55)
                  Summary = summaries.[rng.Next summaries.Length] }
        ]
