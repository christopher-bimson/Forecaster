module Forecaster.Wasm.Views

open Forecaster.Wasm.Models
open Bolero
open Bolero.Html
open GGNet

    module Colours =
        let forecast = [|"#e3956c";"#f8c96c";"#93b577"|]
        let goals = [|"#5884b1";"#4c7098";"#486db6";"#4160a2";"#36528b"|] 
         
    type SamplesList = Template<"templates/numberlist.html">
    type GoalsList = Template<"templates/goallist.html">
    type Form = Template<"templates/form.html">
    type Page = Template<"templates/page.html">  

    let enableIf predicate =
        attr.disabled (if predicate then null else "true")

    let renderSamples model dispatch =
        let addButton = button [
            on.click (fun _ -> dispatch AddNextSample)
            enableIf (canAddSample model.nextSample)
            attr.classes ["btn";"btn-primary"]
            attr.``type`` "button"][text "Add"]
        
        let samplesListItem value =
            SamplesList
                .SampleListItem()
                    .Sample(value.ToString())
                    .RemoveSample(fun _ -> dispatch (RemoveSample value))
                    .Elt()
        
        SamplesList()
            .NextSample(model.nextSample, fun value -> dispatch (SetNextSample value))
            .AddSampleButton(addButton)
            .SampleList(forEach model.samples samplesListItem)
            .Elt()
            
    let renderGoals model dispatch =
        let addButton = button [
            on.click (fun _ -> dispatch AddGoal)
            enableIf (canAddGoal (model, model.nextName, model.nextTarget))
            attr.classes ["btn";"btn-primary"]
            attr.``type`` "button"][text "Add"]
            
        let groupListItem (goal: Goal) =
            GoalsList
                .GoalListItem()
                    .Goal($"{goal.name} @ {goal.target}")
                    .RemoveGoal(fun _ -> dispatch (RemoveGoal goal.name))
                    .Elt()
        
        GoalsList()
            .Name(model.nextName, fun value -> dispatch (SetName value))
            .Target(model.nextTarget, fun value -> dispatch (SetTarget value))
            .AddGroupButton(addButton)
            .GoalList(forEach model.goals groupListItem)
            .Elt()

    let renderForm model dispatch =
        let forecastButton = button [
            on.click (fun _ -> dispatch CreateForecast)
            enableIf (canForecast model)
            attr.classes ["btn";"btn-primary"]
            attr.``type`` "button"][text "Forecast"]
        
        Form()
            .Iterations(model.iterations, fun value -> dispatch (SetIterations value))
            .Samples(renderSamples model dispatch)
            .Goals(renderGoals model dispatch)
            .ForecastButton(forecastButton)
            .Elt()
            
    let forecast = [|""|]
    let goals = [|""|]

    let renderChart model =
        
        let projectionFilter =
            let projections = Set.empty.Add("Optimistic").Add("Median").Add("Conservative")
            Seq.filter (fun p -> Set.contains p.label projections)
            
        let goalFilter =
            let projections = Set.empty.Add("Optimistic").Add("Median").Add("Conservative")
            Seq.filter (fun p -> not (Set.contains p.label projections))
        
        match Seq.length model.source = 0 with
        | true -> empty 
        | false -> let data = Plot.New(model.source, x = (fun o -> float o.iteration), y = (fun o -> float o.throughput))
                                .Geom_Area(projectionFilter model.source, alpha = 0.5)
                                .Scale_Fill_Discrete((fun o -> o.label), Colours.forecast)
                                .Geom_Line(goalFilter model.source, width = 3.0)
                                .Scale_Color_Discrete((fun o -> o.label), Colours.goals, guide = true)
                                .Scale_X_Discrete()
                                .Title("Forecast")
                                .YLab("Throughput")
                                .XLab("Iterations")
                                .Theme(dark = false)
                   div[attr.classes ["container";"shadow-sm"]][
                     comp<GGNet.Components.Plot<ForecastPoint,double,double>>["data" => data][]
                     hr[]
                   ]
                   
    let view model dispatch =
        Page()
            .Form(renderForm model dispatch)
            .Chart(renderChart model)
            .Elt()


