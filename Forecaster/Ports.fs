module Forecaster.Ports

let forecastCommand generateTrials percentileForecast samples iterations trials = 
    generateTrials samples iterations trials |> percentileForecast