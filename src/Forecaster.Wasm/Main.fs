module Forecaster.Wasm.Main

open Forecaster.Wasm.Models
open Forecaster.Wasm.Views
open Bolero
open Elmish

type App() =
    inherit ProgramComponent<Model, Message>()
    
    override this.Program =
        Program.mkProgram (fun _ -> init, Cmd.none) update view