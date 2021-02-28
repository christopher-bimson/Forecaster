module Forecaster.Tests.Ports

    open Forecaster.DomainTypes
    open Forecaster.Ports
    
    open FsUnit
    open NUnit.Framework

    [<TestFixture>]
    type ``ports - forecastCommand should`` ()=
       
        [<Test>]
        member _.
            ``generate trials then summarize them into a forecast.`` ()=
            
            let generatedTrials = [|7;8|]
            let expectedForecast = [| { Likelihood = 33; Value = 10. } |]
            
            let samples = [|1.|]
            let iterations = 4
            let trials = 100
            
            let mockedTrials s n t =
                s |> should equal samples
                n |> should equal iterations
                t |> should equal trials
                generatedTrials
                
            let mockedForecast ts =
                ts |> should equal generatedTrials
                expectedForecast
                
            forecastCommand mockedTrials mockedForecast samples iterations trials
                |> should equal expectedForecast