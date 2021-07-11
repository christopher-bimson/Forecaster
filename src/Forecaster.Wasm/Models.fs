module Forecaster.Wasm.Models

open Elmish
open Forecaster.Core
open Forecaster.Core.DomainTypes

type Goal = {
    name: string
    target: double
}

type ForecastPoint = {
    throughput: double
    iteration: int
    label: string
}

type Model = {
    samples: double list
    goals: Goal list
    iterations: string
    
    nextSample: string
    nextName: string
    nextTarget: string
    
    source: ForecastPoint seq
}

type Message =
    | SetNextSample of string
    | AddNextSample
    | RemoveSample of double
    | SetIterations of string
    | AddGoal
    | SetName of string
    | SetTarget of string
    | RemoveGoal of string
    | CreateForecast
    | InvalidateForecast

let init =
    {
        samples = []
        goals = []
        iterations = ""
        
        nextSample = ""
        nextName = ""
        nextTarget = ""
        
        source = []
    }

let parseDouble (s: string) =
    match System.Double.TryParse(s) with 
    | true, n -> Some n
    | _ -> None
    
let parseInt (s: string) =
    match System.Int32.TryParse(s) with 
    | true, n -> Some n
    | _ -> None
    
let setIterations model value =
    { model with iterations = value }
    
let setNextSample model value =
    { model with nextSample = value } 

let canAddSample value : bool =
    match parseDouble value with
    | Some d -> if d < 0. then false else true
    | None _ -> false
  
let addNextSample model =
    match parseDouble model.nextSample with
    | Some d -> if d < 0. then model else { model with samples = (d :: model.samples); nextSample = "" }
    | None -> model
    
let removeSample model value =
    let updatedSamples = List.remove model.samples value
    { model with samples = updatedSamples }
    
let setName model value =
    { model with nextName = value }
    
let setTarget model value =
    { model with nextTarget = value }
 
let goalTargetIsValid target =
    match parseDouble target with
        | Some number -> if number > 0. then true else false
        | None -> false
        
let goalNameIsUnique goals name =
     not (List.exists (fun (g: Goal) -> g.name = name) goals)
   
let canAddGoal proposedGoal =
    let model, name, target = proposedGoal
    goalNameIsUnique model.goals name
        && goalTargetIsValid target
        && List.length model.goals < 4
    
let addGoal model =
    match canAddGoal (model, model.nextName, model.nextTarget) with
    | true -> { model with goals = { name = model.nextName; target = double model.nextTarget } :: model.goals; nextName =""; nextTarget =""}
    | _ -> model
    
let removeGoal model name =
    { model with goals = (List.filter (fun g -> g.name <> name) model.goals) }
    
let canForecast model =
   match parseInt model.iterations with
   | Some n -> if n > 1 && n < 53 && (List.length model.samples) > 0 && (Seq.length model.source = 0) then true else false
   | None _ -> false
   
let interpolate total iterations label =
    let step = total/ (double iterations)
    let interpolatedPoints = seq { for i in 1 .. iterations - 1 ->  { iteration = i; throughput = double i * step; label = label } }
    Seq.append interpolatedPoints [{ iteration = iterations; throughput = total; label = label }]
    
let extrude total iterations label =
    seq { for i in 1 .. iterations -> { iteration = i; throughput = total; label = label } }  
    
let forecast model =
    
    match Seq.length model.source > 0 with
    | true -> {model with source = []}
    | false ->  let iterations = int model.iterations
                let forecast = Ports.createQuartileForecast (Array.ofList model.samples) iterations 100000
                let forecast = Array.sortBy (fun (lov : LikelihoodOfValue) -> lov.Likelihood) forecast
                
                let optimistic = forecast.[0].Value
                let median = forecast.[1].Value   
                let conservative = forecast.[2].Value
                
                let optimisticPoints = interpolate optimistic iterations "Optimistic"
                let medianPoints = interpolate median iterations "Median"
                let conservativePoints = interpolate conservative iterations "Conservative"
                let goalPoints = List.map (fun g -> extrude g.target iterations g.name) model.goals |> Seq.concat
                
                let source = Seq.concat [optimisticPoints; medianPoints; conservativePoints; goalPoints]
                
                { model with source = source }
                
let invalidateForecast model =
    { model with source = [] }
    
let update message model =  
    match message with
    | SetNextSample sample -> setNextSample model sample, Cmd.none
    | AddNextSample -> addNextSample model, Cmd.ofMsg InvalidateForecast 
    | RemoveSample sample -> removeSample model sample,  Cmd.ofMsg InvalidateForecast 
    | SetIterations value -> setIterations model value, Cmd.ofMsg InvalidateForecast 
    | AddGoal -> addGoal model, Cmd.ofMsg InvalidateForecast 
    | SetName name -> setName model name, Cmd.none
    | SetTarget target -> setTarget model target, Cmd.none
    | RemoveGoal name -> removeGoal model name, Cmd.ofMsg InvalidateForecast
    | CreateForecast -> forecast model, Cmd.none
    | InvalidateForecast -> invalidateForecast model, Cmd.none
           