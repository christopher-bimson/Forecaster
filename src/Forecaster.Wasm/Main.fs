module Forecaster.Wasm.Main

    open Forecaster.Wasm.ViewModelTypes
    open Forecaster.Wasm.ViewModel
    open Forecaster.Wasm.Views
    open Bolero
    open Elmish

    type App() =
        inherit ProgramComponent<ViewModel, Message>()
        
        override this.Program =
            Program.mkProgram (fun _ -> init, Cmd.none) update view