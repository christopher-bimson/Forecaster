# Forecaster
Forecaster is a simple .NET Core console application that can create a forecast of the future value of some variable using
some historic data points and a Monte Carlo simulation.

The original purpose of this application was to forecast the mid-to-long term progress of a software development
team using it's historic throughput, as a less wasteful an more accurate alternative to estimation. See my original 
[blog post](https://christopher-bimson.github.io/2023/07/forecasting/) for more information.
 
[FocusedObjective](https://github.com/FocusedObjective/FocusedObjective.Resources) was very
useful when originally putting this together, and contains a lot of useful resources on this topic that are well worth looking at.

I've subsequently used this to forecast other things, so during the port to .NET Core the language of 
the interface became a bit more general.

As a result of wanting to make some changes to the output (mainly adding a summary by quartiles to give an easier to use worse/median/best case forecast) I rewrote it in F# because:

1. C# just gets more and more incoherent with each release as new features are thrown into the bucket, and as a result the language is starting to annoy me.
2. To practice F# and a more functional approach to programming.

As a result, as well as the new additional features, the CLI has changed in some cosmetic ways as a result of switching to Argu.

## Build
```
dotnet build Forecaster.sln
```

## Test
```
dotnet test Forecaster.sln
```

## Usage
```
USAGE: forecaster [--help] [--command <percentile|quartile>]
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
```
