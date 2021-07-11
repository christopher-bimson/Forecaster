module Forecaster.Core.Ports

let createForecast generator aggregator samples iterations trials = 
    generator samples iterations trials |> aggregator
    
let createPercentileForecast =
    createForecast Domain.generateTrials Domain.summarizeByPercentile
    
let createQuartileForecast =
    createForecast Domain.generateTrials Domain.summarizeByQuartile