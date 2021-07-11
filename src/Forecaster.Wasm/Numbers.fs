module Forecaster.Wasm.Numbers

    let parseFloat (s: string) =
        match System.Double.TryParse(s) with 
        | true, n -> Some n
        | _ -> None
        
    let parseInt (s: string) =
        match System.Int32.TryParse(s) with 
        | true, n -> Some n
        | _ -> None