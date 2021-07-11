module List

    let rec remove list value =
                match list with
                | head :: tail when head = value -> tail
                | head :: tail -> head :: remove tail value
                | _ -> []

