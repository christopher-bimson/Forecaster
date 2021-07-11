module Forecaster.Wasm.ViewModelTypes

    type Goal = {
        name: string
        target: float
    }

    type ChartPoint = {
        value: float
        iteration: int
        label: string
    }
    
    type Samples = float list
    
    type Goals = Goal list

    type ViewModel = {
        samples: Samples
        goals: Goals
        iterations: string
        
        nextSample: string
        nextName: string
        nextTarget: string
        
        chartData: ChartPoint seq
    }

    type Message =
        | SetNextSample of string
        | AddNextSample
        | RemoveSample of float
        | SetIterations of string
        | AddGoal
        | SetName of string
        | SetTarget of string
        | RemoveGoal of string
        | CreateForecast
        | InvalidateForecast

