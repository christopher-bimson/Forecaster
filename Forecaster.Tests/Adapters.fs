module Forecaster.Tests.Adapters

open Forecaster.DomainTypes
open Forecaster.AdapterTypes
open Forecaster.Adapters

open NUnit.Framework
open FsUnit

    [<TestFixture>]
    type ``adapter - interpret should`` ()=
        
        let argv = [|"some";"args"|]
            
        [<Test>]
        member _.
            ``invoke a parser on command line input and return the error if it is invalid`` ()=
            
            let expectedMessage = "This is a parser error message."
            
            let stub_handler _ =
                Assert.Fail("stub_handler should not be called.")
                Error ""
            
            let stub_parser _ =
                Error expectedMessage
                
            match interpret stub_parser stub_handler argv with
                | Error message ->  message |> should equal expectedMessage
                | _ -> failwith "stub_parser should have returned an error."
            
        [<Test>]
        member _.
            ``invoke a parser on command line input and return the result of the argument handler if it is valid`` ()=
            
            let expectedArguments = {
                Command = Percentile
                Samples = [||]
                Iterations = 10
                Trials = 100
                Format = Pretty
            }
            
            let expectedResults = "Output"
            
            let stub_parser input =
                input |> should equal argv
                Ok expectedArguments
            
            let stub_handler arguments =
                arguments |> should equal expectedArguments
                expectedResults
            
            match interpret stub_parser stub_handler argv with
                | Ok output -> output |> should equal expectedResults
                | _ -> failwith "interpret should not return an error for valid input."
            
    [<TestFixture>]
    type ``adapter - parse should`` ()=
        
        static member ``valid argument examples``()=
                [|
                    (
                        [|"--samples";"2";"3";"4";"5";"--iterations";"4"|],
                        {
                            Command = Percentile
                            Samples = [|2.;3.;4.;5.;|]
                            Iterations = 4
                            Trials = 100000
                            Format = Pretty
                        }
                    );
                    (
                        [|"--command";"quartile";"--samples";"2";"3";"4";"5";"--iterations";"4"|],
                        {
                            Command = Quartile
                            Samples = [|2.;3.;4.;5.;|]
                            Iterations = 4
                            Trials = 100000
                            Format = Pretty
                        }
                    )
                    (
                        [|"--samples";"2";"3";"4";"5";"--iterations";"4";"--trials";"7"|],
                        {
                            Command = Percentile
                            Samples = [|2.;3.;4.;5.;|]
                            Iterations = 4
                            Trials = 7
                            Format = Pretty
                        }
                    )
                    (
                        [|"--samples";"2";"3";"4";"5";"--iterations";"4";"--format";"tsv"|],
                        {
                            Command = Percentile
                            Samples = [|2.;3.;4.;5.;|]
                            Iterations = 4
                            Trials = 100000
                            Format = Tsv
                        }
                    )
                |]
                
        
        [<Test>]
        [<TestCaseSource("valid argument examples")>]
        member _.
            ``valid arguments are parsed correctly`` (testCase: string array * Arguments)=
            let argv, expected = testCase
            
            match parse argv with
                | Ok actual -> actual |> should equal expected
                | _ -> Assert.Fail($"{argv} is valid and should be parsed correctly.")
                
        static member ``invalid argument examples``()=
            [|
                (
                    [||],
                    "ERROR: missing argument '--samples'."
                )
                (
                    [|"--samples";"1";"2"|],
                    "ERROR: missing argument '--iterations'."
                )
                (
                    [|"--command";"wibble"|],
                    "ERROR: parameter '--command' must be followed by <percentile|quartile>"
                )
                (
                    [|"--samples";"2";"--iterations";"4";"--format";"jpeg"|],
                    "ERROR: parameter '--format' must be followed by <pretty|tsv>, but was 'jpeg'." 
                )
                (
                    [|"--samples";"2";"--iterations";"-1"|],
                    "ERROR: parameter '--iterations' must be followed by <uint>, but was '-1'." 
                )
                (
                    [|"--samples";"2";"--iterations";"4";"--trials";"-1"|],
                    "ERROR: parameter '--trials' must be followed by <uint>, but was '-1'." 
                )
            |]
            
        [<Test>]
        [<TestCaseSource("invalid argument examples")>]
        member _.
            ``invalid arguments are rejected with the correct message.`` (testCase: string array * string)=
            
            let argv, expected = testCase
            match parse argv with
                | Error actual -> actual |> should startWith expected
                | Ok _ -> Assert.Fail("Parsing should have failed.")
                
    [<TestFixture>]
    type ``adapter - handler should`` ()=
        
        let stubCommand _ _ _ =
            Assert.Fail("Stub should not be called")
            [||]
        
        [<Test>]
        member _.
            ``return a formatted percentile forecast when arguments.Command = Percentile`` ()=
                
                let arguments = {
                    Command = Percentile
                    Samples = [|2.;3.;4.;5.;|]
                    Iterations = 4
                    Trials = 100000
                    Format = Tsv
                }
                
                let expectedOutput = "This is the output."
                
                let percentileForecast = [|
                    {
                        Likelihood = 50
                        Value = 33.33
                    }
                |]
                
                let mockedPercentile samples iterations trials =
                    samples |> should equal arguments.Samples
                    iterations |> should equal arguments.Iterations
                    trials |> should equal arguments.Trials
                    percentileForecast
                    
                let mockedOutput format forecast =
                    forecast |> should equal percentileForecast
                    format |> should equal arguments.Format
                    expectedOutput
                
                handler mockedPercentile stubCommand mockedOutput arguments |> should equal expectedOutput
                
        
        [<Test>]
        member _.
            ``return a formatted quartile forecast when arguments.Command = Quartile`` ()=
                
                let arguments = {
                    Command = Quartile
                    Samples = [|2.;3.;4.;5.;|]
                    Iterations = 4
                    Trials = 100000
                    Format = Tsv
                }
                
                let expectedOutput = "This is the output."
                
                let quartileForecast = [|
                    {
                        Likelihood = 50
                        Value = 33.33
                    }
                |]
                    
                let mockedQuartile samples iterations trials =
                    samples |> should equal arguments.Samples
                    iterations |> should equal arguments.Iterations
                    trials |> should equal arguments.Trials
                    quartileForecast
                    
                let mockedOutput format forecast =
                    forecast |> should equal quartileForecast
                    format |> should equal arguments.Format
                    expectedOutput
                
                handler stubCommand mockedQuartile mockedOutput arguments |> should equal expectedOutput
                
    [<TestFixture>]
    type ``adapter - output should`` ()=
        
        let stubFormatter _ =
            Assert.Fail("This stub should not be called.")
            ""
        
        [<Test>]
        member _.
            ``pretty format the forecast then the format argument is pretty`` ()=
                let inputForecast = [||]
                let format = Pretty
                
                let expectedOutput = "Wibble"
                
                let mockedPretty forecast =
                    forecast |> should equal forecast
                    expectedOutput
                    
                output mockedPretty stubFormatter format inputForecast   |> should equal expectedOutput
          
    [<TestFixture>]      
    type ``adapter - pretty should`` ()=
        
        [<Test>]
        member _.
            ``render a forecast into a pretty ASCII friendly table`` ()=
                
                let forecast = [|
                    { Likelihood = 100; Value = 17.0 }
                    { Likelihood = 1; Value = 117.0 }
                |]
                
                let expectedOutput = """+------------+-------+
| Likelihood | Value |
+------------+-------+
|        100 |    17 |
+------------+-------+
|          1 |   117 |
+------------+-------+
"""
                let actual = pretty forecast
                actual |> should equal expectedOutput
                
    [<TestFixture>]      
    type ``adapter - tsv should`` ()=
        
        [<Test>]
        member _.
            ``render a forecast into an excel-able tab separated value format`` ()=
                
                let forecast = [|
                    { Likelihood = 100; Value = 17.0 }
                    { Likelihood = 1; Value = 117.0 }
                |]
                
                let expectedOutput = "Likelihood\tValue\n100\t17\n1\t117\n"
                let actual = tsv forecast
                actual |> should equal expectedOutput