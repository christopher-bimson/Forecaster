module Forecaster.Program

let resolveHandler =
    let quartile = Ports.forecastCommand Domain.generateTrials Domain.summarizeByQuartile
    let percentile = Ports.forecastCommand Domain.generateTrials Domain.summarizeByPercentile
    let output = Adapters.output Adapters.pretty Adapters.tsv
    Adapters.handler percentile quartile output

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