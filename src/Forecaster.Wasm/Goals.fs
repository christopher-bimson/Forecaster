module Forecaster.Wasm.Goals

    open Forecaster.Wasm.Numbers
    open Forecaster.Wasm.ViewModelTypes
    
    let setName model value =
        { model with nextName = value }
        
    let setTarget model value =
        { model with nextTarget = value }
     
    let internal goalTargetIsValid target =
        match parseFloat target with
            | Some number -> if number > 0. then true else false
            | None -> false
            
    let internal goalNameIsUnique goals name =
         not (List.exists (fun (g: Goal) -> g.name = name) goals)
       
    let canAddGoal proposedGoal =
        let model, name, target = proposedGoal
        goalNameIsUnique model.goals name
            && goalTargetIsValid target
            && List.length model.goals < 4
        
    let addGoal model =
        match canAddGoal (model, model.nextName, model.nextTarget) with
        | true -> { model with goals = { name = model.nextName; target = float model.nextTarget } :: model.goals; nextName =""; nextTarget =""}
        | _ -> model
        
    let removeGoal model name =
        { model with goals = (List.filter (fun g -> g.name <> name) model.goals) }