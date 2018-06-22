# Forecaster
Forecaster is  a simple .NET Core console application that can create a forecast of the future value of some variable using
some historic data points and a monte carlo simultion.

The original purpose of this application was to forecast the mid-to-long term progress of a software development
team using it's historic throughput, as a less wasteful an more accurate alternative to estimation. See my original 
[blog post](https://christopher-bimson.github.io/2017-04-19-forecaster/) for more information.
 
[FocusedObjective](https://github.com/FocusedObjective/FocusedObjective.Resources) was very
useful when originally putting this together, and contains a lot of useful resources on this topic that are well worth looking at.

I've subsequently used this to forecast other things, so during the port to .NET Core the language of 
the interface became a bit more general.

## Build
```
dotnet build Forecaster.sln
```

Or use your favourite IDE.

## Usage
```
dotnet Forecaster.dll --help

Forecaster 1.0.0
Copyright (C) 2018 Forecaster

  -s, --samples         Required. Sample data points of the value you want to
                        forecast.

  -f, --forecast        Required. The number of data points 'ahead' to
                        forecast.

  -t, --trials          (Default: 100000) The number of trials to use to build
                        the forecast. Each trial is a 'potential future' that
                        will contribute to the forecast.

  -o, --outputFormat    (Default: Pretty) The output format of the forecast.
                        Valid values are: Pretty, Markdown, JSON and CSV.

  --help                Display this help screen.

  --version             Display version information.
```