module Forecaster.Tests.Acceptance

open System
open System.IO
open FsUnit
open NUnit.Framework

[<TestFixture>]
type ``acceptance tests`` ()=
        
        [<Test>]
        member _.
            ``forecaster --help`` ()=
                let argv = [|"--help"|]
                let expectedOutput = """USAGE: forecaster [--help] [--command <percentile|quartile>]
                  [--samples [<double>...]] [--iterations <uint>]
                  [--trials <uint>] [--format <pretty|tsv>]

OPTIONS:

    --command <percentile|quartile>
                          (default: percentile) The type of forecast to
                          generate.
    --samples [<double>...]
                          (required) Sample data points of the value you want
                          to forecast.
    --iterations <uint>   (required) The number of data points 'ahead' to
                          forecast.
    --trials <uint>       (default: 100000) The number of trials to use to
                          build the forecast. Each trial is a 'potential
                          future' that will contribute to the forecast.
    --format <pretty|tsv> (default: pretty) The output format of the forecast.
    --help                display this list of options.
"""

                let stdOut: StringWriter = new StringWriter()
                Console.SetOut(stdOut)
                
                Forecaster.Program.main argv |> should equal -1
                
                stdout.Flush()
                stdOut.ToString() |> should equal expectedOutput
                
                
        [<Test>]
        member _.
            ``forecaster --samples 4 5 6 5 4 --iterations 5`` ()=
                let argv = [|"--samples";"4";"5";"6";"5";"4";"--iterations";"5"|]
                let expectedOutput = """+------------+-------+
| Likelihood | Value |
+------------+-------+
|        100 |    20 |
+------------+-------+
|         90 |    22 |
+------------+-------+
|         80 |    23 |
+------------+-------+
|         70 |    23 |
+------------+-------+
|         60 |    24 |
+------------+-------+
|         50 |    24 |
+------------+-------+
|         40 |    24 |
+------------+-------+
|         30 |    25 |
+------------+-------+
|         20 |    25 |
+------------+-------+
|         10 |    26 |
+------------+-------+
"""

                let stdOut: StringWriter = new StringWriter()
                Console.SetOut(stdOut)
                
                Forecaster.Program.main argv |> should equal 0
                
                stdout.Flush()
                stdOut.ToString() |> should equal expectedOutput
                
        
        [<Test>]
        member _.
            ``forecaster --command quartile --samples 4 5 6 5 4 --iterations 4`` ()=
                let argv = [|"--command";"quartile";"--samples";"3";"5";"6";"5";"4";"7";"5";"--iterations";"4"|]
                let expectedOutput = """+------------+-------+
| Likelihood | Value |
+------------+-------+
|         75 |    18 |
+------------+-------+
|         50 |    20 |
+------------+-------+
|         25 |    22 |
+------------+-------+
"""
                let stdOut: StringWriter = new StringWriter()
                Console.SetOut(stdOut)
                
                Forecaster.Program.main argv |> should equal 0
                
                stdout.Flush()
                stdOut.ToString() |> should equal expectedOutput