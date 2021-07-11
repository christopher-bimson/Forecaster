module Forecaster.Console.Adapters

open System.Text
open BetterConsoleTables
open Forecaster.Console.AdapterTypes

open Argu
open Forecaster.Core.DomainTypes

let handler percentile quartile output arguments =
    let command = 
        match arguments.Command with
            | Percentile _ -> percentile 
            | Quartile _ -> quartile
    command arguments.Samples arguments.Iterations arguments.Trials |> output arguments.Format

let interpret parser handler argv =
    match parser argv with
        | Ok arguments -> Ok (handler arguments)
        | Error message -> Error message
        
let parse argv =
    let parser = ArgumentParser.Create<ArgumentTemplate>(programName = "forecaster", usageStringCharacterWidth = 80)
    try
        let parsed = parser.Parse argv
        let result = {
            Command = parsed.GetResult(Command, Percentile)
            Samples = parsed.GetResult(Samples) |> Array.ofList
            Iterations = parsed.GetResult(Iterations) |> int
            Trials = parsed.GetResult(Trials, 100000u) |> int
            Format = parsed.GetResult(Format, Pretty)
        }
        Ok result
    with
        :? (ArguParseException) as ex ->
            Error ex.Message 

let output pretty tsv format forecast =
    match format with
        | Pretty _ -> pretty forecast
        | Tsv _ -> tsv forecast
        
let pretty (forecast: LikelihoodOfValue array) =
    let table = Table()
    table.AddColumn("Likelihood", Alignment.Right) |> ignore
    table.AddColumn("Value", Alignment.Right) |> ignore
    for entry in forecast do
        table.AddRow(entry.Likelihood, entry.Value) |> ignore
    TableConfiguration.ConsoleAvailable <- false
    table.Config.ExpandConsole <- false
    table.Config <- TableConfiguration.MySql()
    table.ToString()
    
let tsv (forecast: LikelihoodOfValue array) =
    let output = StringBuilder()
    output.AppendLine("Likelihood\tValue") |> ignore
    for entry in forecast do
        output.AppendLine($"{entry.Likelihood}\t{entry.Value}") |> ignore
    output.ToString()