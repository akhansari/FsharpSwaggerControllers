namespace FsharpSwaggerControllers

open System

type WeatherForecast =
    { Date: DateTime
      TemperatureC: int32
      Summary: string }

    member this.TemperatureF =
        32.0 + (float this.TemperatureC / 0.5556)
