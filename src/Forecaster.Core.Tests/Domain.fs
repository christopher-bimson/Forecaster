module Forecaster.Core.Tests.Domain

    open Forecaster.Core.Domain
    
    open System
    open Forecaster.Core.DomainTypes
    open NUnit.Framework
    open FsUnit
    open FsCheck
    open FsCheck.NUnit
    
    [<TestFixture>]
    type ``domain - pickWithoutReplacement should``()=
        
        [<Property>]
        member _.
            ``not pick a value that is not in the samples array.`` (samples: NonEmptyArray<NormalFloat>)=
                let sample = pickWithoutReplacement samples.Get
                let wasPickedFromSamples = Array.contains sample samples.Get
                wasPickedFromSamples
                      
    [<TestFixture>]
    type ``domain - generateTrial should``()=
        
        [<Property>]
        member _.
            ``not generate a trial that is less than the minimum sample * the number of iterations.`` (testSamples: NonEmptyArray<NormalFloat>,
                                                                                                        testIterations: int) =
                let samples =  testSamples.Get |> Array.map (fun nf -> nf.Get)
                let iterations = Math.Abs(testIterations)
                
                let minimumSample =  samples |> Array.min
                let minimumTrial = Array.replicate iterations minimumSample |> Array.sum
                
                generateTrial samples iterations >= minimumTrial
        
        [<Property>]
        member _.
            ``not generate a trial that is larger than the maximum sample * the number of iterations.`` (testSamples: NonEmptyArray<NormalFloat>,
                                                                                                         testIterations: int) =
                let samples =  testSamples.Get |> Array.map (fun nf -> nf.Get)
                let iterations = Math.Abs(testIterations)
                
                let maxSample =  samples |> Array.max
                let maxTrial = Array.replicate iterations maxSample |> Array.sum
                
                generateTrial samples iterations <= maxTrial
                 
    [<TestFixture>]
    type ``domain - generateTrials should``()=
        
        [<Property>]
        member _.
            ``generate the correct number of trials.``(generatedSamples: NonEmptyArray<NormalFloat>,
                                                       generatedIterations: int, generatedTrialCount: int) =
                let samples = Array.map (fun (nf: NormalFloat) -> nf.Get) generatedSamples.Get
                let iterations = Math.Abs(generatedIterations)
                let trialCount = Math.Abs(generatedTrialCount)
                
                generateTrials samples iterations trialCount |> Array.length = trialCount
                
    [<TestFixture>]
    type ``domain - likelihood should``()=
        
        [<Test>]
        member _.
            ``invert a percentile.`` ()=
                let percentile = 30
                let expected = 70
                
                likelihood percentile |> should equal expected
                
    [<TestFixture>]
    type ``domain - summarizeByPercentile should``()=
        
        [<Property>]
        member _.
            ``contain 10 entries`` (nonEmptyTrials: NonEmptyArray<float>) =
                let trials = nonEmptyTrials.Get
                summarizeByPercentile trials |> Array.length = 10
                
        [<Property>]
        member _.
            ``a percentile forecast contains likelihoods from 10 to 100 in increments of 10.`` (generatedTrials: NonEmptyArray<NormalFloat>) =
                let trials = Array.map (fun (nf: NormalFloat) -> nf.Get) generatedTrials.Get
                let expectedLikelihoods = [|100;90;80;70;60;50;40;30;20;10|]
                
                let likelihoods = summarizeByPercentile trials |> Array.map (fun fv -> fv.Likelihood)
                likelihoods = expectedLikelihoods
            
            
        [<Test>]
        member _.
            ``correctly represent the percentiles in a set of trials``() =
                let trials = [|1.;2.;3.;4.;5.;6.;7.;8.;9.;10.|]
                let forecast = summarizeByPercentile trials
                
                let sum = Array.map (fun (f: LikelihoodOfValue) -> f.Value) forecast |> Array.sum
                
                sum |> should equal 50.5
                
    [<TestFixture>]
    type ``domain - summarizeByQuartile should``()=
        
        [<Property>]
        member _.
            ``contain w entries`` (nonEmptyTrials: NonEmptyArray<float>) =
                let trials = nonEmptyTrials.Get
                summarizeByQuartile trials |> Array.length = 3
                
        [<Property>]
        member _.
            ``a percentile forecast contains the likelihoods 75,50 and 25.`` (generatedTrials: NonEmptyArray<NormalFloat>) =
                let trials = Array.map (fun (nf: NormalFloat) -> nf.Get) generatedTrials.Get
                let expectedLikelihoods = [|75;50;25|]
                
                let likelihoods = summarizeByQuartile trials |> Array.map (fun fv -> fv.Likelihood)
                likelihoods = expectedLikelihoods
            
            
        [<Test>]
        member _.
            ``correctly represent the quartiles in a set of trials``() =
                let trials = [|1.;2.;3.;4.;5.;6.;7.;8.;9.;10.|]
                let forecast = summarizeByQuartile trials
                
                let sum = Array.map (fun (f: LikelihoodOfValue) -> f.Value) forecast |> Array.sum
                
                sum |> should equal 16.5
                
               
                
                
    