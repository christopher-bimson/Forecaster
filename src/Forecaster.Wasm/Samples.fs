module Forecaster.Wasm.Samples

    open Forecaster.Wasm.Numbers
    open Forecaster.Wasm.ViewModelTypes
    
    let setNextSample model value =
        { model with nextSample = value } 
    
    let canAddSample value : bool =
        match parseFloat value with
        | Some d -> if d < 0. then false else true
        | None _ -> false
    
    let addNextSample model =
        match parseFloat model.nextSample with
        | Some d -> if d < 0. then model else { model with samples = (d :: model.samples); nextSample = "" }
        | None -> model
        
    let removeSample model value =
        let updatedSamples = List.remove model.samples value
        { model with samples = updatedSamples }