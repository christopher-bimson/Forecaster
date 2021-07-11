module Forecaster.Wasm.Forecasting

   open Forecaster.Core
   open Forecaster.Core.DomainTypes
   open Forecaster.Wasm.Numbers
   open Forecaster.Wasm.ViewModelTypes
   
   let canForecast model =
       match parseInt model.iterations with
       | Some n -> if n > 1 && n < 53 && (List.length model.samples) > 0 && (Seq.length model.chartData = 0) then true else false
       | None _ -> false
       
   let interpolate total iterations label =
        let step = total/ (float iterations)
        let interpolatedPoints = seq { for i in 1 .. iterations - 1 ->  { iteration = i; value = float i * step; label = label } }
        Seq.append interpolatedPoints [{ iteration = iterations; value = total; label = label }]
        
   let extrude total iterations label =
        seq { for i in 1 .. iterations -> { iteration = i; value = total; label = label } }  
        
   let forecast model =
       match Seq.length model.chartData > 0 with
       | true -> {model with chartData = []}
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
                    
                   { model with chartData = source }
                        
   let invalidateForecast model =
       { model with chartData = [] }

