namespace FsharpSwaggerControllers

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

type WeatherForecast =
    { Date: DateTime
      TemperatureC: int32
      Summary: string }
    member this.TemperatureF =
        32.0 + (float this.TemperatureC / 0.5556)

[<ApiController>]
[<Route("[controller]")>]
type WeatherForecastController () =
    inherit ControllerBase ()

    let summaries =
        [ "Freezing"
          "Bracing"
          "Chilly"
          "Cool"
          "Mild"
          "Warm"
          "Balmy"
          "Hot"
          "Sweltering"
          "Scorching" ]

    /// Get the weather forecast
    [<HttpGet>]
    member _.Get () =
        let rng = Random()
        [ for index in 0..4 ->
            { Date = DateTime.Now.AddDays(float index)
              TemperatureC = rng.Next(-20,55)
              Summary = summaries[rng.Next summaries.Length] } ]

module Programe =

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder args
        builder.Services
            .AddSwaggerGen(fun c ->
                c.IncludeXmlComments(
                    IO.Path.Combine(
                        AppContext.BaseDirectory,
                        Reflection.Assembly.GetEntryAssembly().GetName().Name + ".xml")))
            .AddControllers()
        |> ignore
        let app = builder.Build()
        app.UseSwagger().UseSwaggerUI() |> ignore
        app.MapControllers() |> ignore
        app.Run()
        0
