module Forecaster.Wasm.Goals

    open Forecaster.Wasm.Numbers
    open Forecaster.Wasm.ViewModelTypes
    
    let setName model value =
        { model with nextName = value }
        
    let setTarget model value =
        { model with nextTarget = value }
            
    let internal goalIsValid goals name target =
        let parsedTarget =
          match parseFloat target with
            | Some number -> number
            | None -> 0.
        
        not (name = "")
        && parsedTarget > 0.
        && not (List.exists (fun (g: Goal) -> g.name = name || g.target = parsedTarget) goals)
        
    let canAddGoal proposedGoal =
        let model, name, target = proposedGoal
        goalIsValid model.goals name target
        && List.length model.goals < 5
        
    let addGoal model =
        match canAddGoal (model, model.nextName, model.nextTarget) with
        | true -> { model with goals = { name = model.nextName; target = float model.nextTarget } :: model.goals; nextName =""; nextTarget =""}
        | _ -> model
        
    let removeGoal model name =
        { model with goals = (List.filter (fun g -> g.name <> name) model.goals) }