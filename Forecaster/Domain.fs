module Forecaster.Domain

open Forecaster.DomainTypes

open System
open MathNet.Numerics.Statistics

let rng = Random()

let pickWithoutReplacement samples =
     let pick = rng.Next(0, Array.length samples)
     samples.[pick]
     
let generateTrial samples iterations =
    let samplePicker _ : float  =
        pickWithoutReplacement samples
    
    Array.init iterations samplePicker |> Array.sum
          
let generateTrials samples iterations trials =
    Array.init trials (fun _ -> generateTrial samples iterations)
    
let likelihood percentile =
    100 - percentile
    
let summarizeByPercentile (trials: float array) =
    let percentiles = Array.init 10 (fun index -> index * 10)
    
    let likelihoodOfValueFrom percentile =
        {
            Likelihood = likelihood percentile;
            Value = Statistics.Percentile(trials, percentile);
        }
    
    Array.map likelihoodOfValueFrom percentiles
    
let summarizeByQuartile (trials: float array) =
    let percentiles = [|25;50;75|]
    
    let likelihoodOfValueFrom percentile =
        {
            Likelihood = likelihood percentile;
            Value = Statistics.Percentile(trials, percentile);
        }
    
    Array.map likelihoodOfValueFrom percentiles