namespace FsharpSwaggerControllers

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Builder
open Microsoft.OpenApi.Models

module Program =
    
    let private configureServices (_: WebHostBuilderContext) (services: IServiceCollection) =
        services
            .AddHealthChecks().Services
            .AddResponseCompression()
            .AddRouting()
            .AddSwaggerGen(fun c ->
                c.SwaggerDoc("v1", OpenApiInfo(Title = "Fsharp Swagger with Controllers", Version = "v1"))
                c.IncludeXmlComments(
                    IO.Path.Combine(
                        AppContext.BaseDirectory,
                        Reflection.Assembly.GetEntryAssembly().GetName().Name + ".xml")))
            .AddControllers()
        |> ignore

    let private configureMiddlewares (_: WebHostBuilderContext) (app: IApplicationBuilder) =
        app
            .UseResponseCompression()
            .UseRouting()
            .UseSwagger()
            .UseEndpoints(fun endpoints ->
                endpoints.MapHealthChecks("/health") |> ignore
                endpoints.MapControllers() |> ignore)
            .UseSwaggerUI(fun c -> c.SwaggerEndpoint("v1/swagger.json", "V1"))
        |> ignore

    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder
                    .ConfigureServices(configureServices)
                    .Configure(configureMiddlewares)
                |> ignore)

    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()
        0
