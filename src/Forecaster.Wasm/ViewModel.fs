module Forecaster.Wasm.ViewModel

    open Elmish
    open Forecaster.Wasm.ViewModelTypes
    open Forecaster.Wasm.Samples
    open Forecaster.Wasm.Goals
    open Forecaster.Wasm.Forecasting

    let init =
        {
            samples = []
            goals = []
            iterations = ""
            nextSample = ""
            nextName = ""
            nextTarget = ""
            chartData = []
        }
        
    let setIterations model value =
        { model with iterations = value }
        
    let update message model =  
        match message with
        | SetNextSample sample -> setNextSample model sample, Cmd.none
        | SetName name -> setName model name, Cmd.none
        | SetTarget target -> setTarget model target, Cmd.none
        | SetIterations value -> setIterations model value, Cmd.ofMsg InvalidateForecast 
        | AddNextSample -> addNextSample model, Cmd.ofMsg InvalidateForecast 
        | RemoveSample sample -> removeSample model sample,  Cmd.ofMsg InvalidateForecast 
        | AddGoal -> addGoal model, Cmd.ofMsg InvalidateForecast 
        | RemoveGoal name -> removeGoal model name, Cmd.ofMsg InvalidateForecast
        | CreateForecast -> forecast model, Cmd.none
        | InvalidateForecast -> invalidateForecast model, Cmd.none
           