namespace Forecaster.Wasm

open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Microsoft.Extensions.DependencyInjection
open System
open System.Net.Http

    module Program =

        [<EntryPoint>]
        let Main args =
            let builder = WebAssemblyHostBuilder.CreateDefault(args)
            builder.RootComponents.Add<Main.App>("#main")
            builder.Build().RunAsync() |> ignore
            0
