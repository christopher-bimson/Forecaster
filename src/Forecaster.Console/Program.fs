module Forecaster.Console.Program

open Forecaster.Core.Ports

let resolveHandler =
    let output = Adapters.output Adapters.pretty Adapters.tsv
    Adapters.handler createPercentileForecast createQuartileForecast output

[<EntryPoint>]
let main argv =
    let interpret = Adapters.interpret Adapters.parse resolveHandler
    match interpret argv with
        | Ok output ->
            output |> printf "%s"
            0
        | Error message ->
            message |> printf "%s"
            -1