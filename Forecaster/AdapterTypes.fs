module Forecaster.AdapterTypes

open Argu

type Command =
    | Percentile
    | Quartile
    
type OutputFormat = 
    | Pretty
    | Tsv
    
type Arguments = {
    Command: Command;
    Samples: float array;
    Iterations: int;
    Trials: int;
    Format: OutputFormat;
}

type ArgumentTemplate =
    | Command of Command
    | Samples of float list
    | Iterations of uint
    | Trials of uint
    | Format of OutputFormat
    
    interface IArgParserTemplate with
        member element.Usage =
            match element with
            | Command _ -> "(default: percentile) The type of forecast to generate."
            | Samples _ -> "(required) Sample data points of the value you want to forecast."
            | Iterations _ -> "(required) The number of data points 'ahead' to forecast."
            | Trials _ -> "(default: 100000) The number of trials to use to build the forecast. Each trial is a 'potential future' that will contribute to the forecast."
            | Format _ -> "(default: pretty) The output format of the forecast."