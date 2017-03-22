# Forecaster
It's a simple console application that will create a projection of future progress based on your past progress and a simple monte carlo simulation. The big win is that the projection is a number of possible values with a 'likelihood' score for each.

[FocusedObjective.Resources](https://github.com/FocusedObjective/FocusedObjective.Resources) was handy putting this together, and contains a lot of useful resources on this topic that are well worth looking at.

## Build
At the moment it is a straight solution build using Visual Studio 2015.

## Usage
```
Forecaster 1.0.0.0

  -h, --historicThroughput    Required. An array of numeric values that
                              represent throughput in some work unit (e.g.
                              story points, story count, ideal hours -
                              abbreviated WU) / time period (e.g. week,
                              iteration, month).

  -p, --periodsToForecast     Required. The number of time periods to forecast.
                              Must be a positive value.

  -t, --numberOfTrials        (Default: 100000) The number of trials (consider
                              a trial to be a 'potential future') to use when
                              calculating the forecast.
```
## Tests
Um. There aren't any. Yet. Lazy on my part. Mea Culpa.

## Performance
I'm sure this is nowhere near as performant as it could be, but it appears to be adequate for now. The code is optimized for ease of understanding and reasoning about rather than raw performance.



