![Build status](https://github.com/Hefaistos68/MoreDateTime/actions/workflows/dotnet.yml/badge.svg)
[![NuGet Version](http://img.shields.io/nuget/v/MoreDateTime.svg?style=flat)](https://www.nuget.org/packages/MoreDateTime/) 
[![GitHub contributors](https://img.shields.io/github/contributors/hefaistos68/moredatetime.svg)](https://github.com/hefaistos68/MoreDateTime)

# :date: MoreDateTime - [Github](https://github.com/Hefaistos68/MoreDateTime)
_(full documentation coming soon)_

This library is built from the ground up to be a simple, easy to use, and intuitive date and time library for .NET, that simplifies common tasks and provides a consistent API for working with dates and times, throughout the DateTime, DateOnly and TimeOnly .NET objects. It adds many operations that one expects to find in a date and time library, but are missing from the .NET standard library.

For example: `NextWeek()` to advance a DateTime or DateOnly to the same weekday in the next week, `NextYear()` to add a year to a DateTime or DateOlnly object (yes, of course you can use the existing `.AddYears(1)` method, but it does not look as clear), `NextWorkday()` to advance the DateTime or DateOnly object to the next working day on the given `Calendar`.

Or things like `IsWeekend()` which of course you can also do with `myDate.DayOfWeek == DayOfWeek.Saturday || myDate.DayOfWeek == DayOfWeek.Sunday`, but it just aint as nice to read. Then we also have `IsWorkday()` which is the opposite of `IsWeekend()` and also `IsHoliday()` which checks if the given date is a holiday on the given calendar.

Not to mention the `IsBetween()` method which is a very useful method to check if a date is between two other dates. It also supports the `DateOnly` and `TimeOnly` objects. 

For more elaborate operations we have the Enumerations, like `EnumerateDaysUntil`, `EnumerateMonthsUntil`, `EnumerateYearsUntil`, `EnumerateWeeksUntil`, `EnumerateWorkdaysUntil`, `EnumerateWeekendsUntil`, `EnumerateHolidaysUntil`.

Finally you can add minutes to a `DateOnly` object (given its more than a day) -> `myDate.AddMinutes(525600); // thats a year in minutes` or even hours or seconds if you wish so.

Want to know the FirstMondayOfTheMonth? -> `myDate.FirstMondayOfTheMonth()` or the LastFridayOfTheMonth? -> `myDate.LastFridayOfTheMonth()`. How often did you need already to know the first or last day of the month? -> `myDate.FirstDayOfTheMonth()` or `myDate.LastDayOfTheMonth()`.

Need to know the number of workdays in a period? -> `myDate.NumberOfDaysUntil()` or the number of holidays in a period? -> `myDate.NumberOfHolidaysUntil()`.

You can also create custom enumerations with `EnumerateInStepsUntil` which allows you to create enumerations from a start date or time until a end date or time in steps specified by a `TimeSpan`. Like in 
```cs
void ListAllDaysNotHolidays(DateOnly startDate)
{
	// you can provide also a Calendar object to the EnumerateInStepsUntil method if the current Culture is not adequate
	var myEnum = startDate.EnumerateInStepsUntil(startDate.AddMonths(6), TimeSpan.FromDays(1), SkipHolidays);

	foreach (var item in myEnum)
	{
		Console.WriteLine(item);
	}

	bool SkipHolidays(DateOnly date) => return !date.IsHoliday();
}

```
Another sample, create 10 minute spans over a week, but skip midnight
```cs
void Get10MinuteSpans(DateTime startDate)
{
	var myEnum = startDate.EnumerateInStepsUntil(startDate.NextWeek(), TimeSpan.FromMinutes(10), SkipMidnight);

	foreach (var item in myEnum)
	{
		Console.WriteLine(item);
	}

	bool SkipMidnight(DateTime date)
	{
		return !date.IsMidnight();
	}
}
```

You got your own `IDateTimeProvider` through which you can inject your own date and time and supports mocking directly. Instead of `DateTime.Utcnow` you can use `DateTimeProvider.Current.UtcNow` and always get the date and time you expect.

## How to use it?

Just install the NuGet package and you are ready to go! It's mostly built upon common sense when using Date and Time objects in .NET.

### Dependencies

For Holiday calculations we use the [Nager.Date](https://github.com/nager/nager.date) library, but you need to bring your own license key. Optionally you can use your own provider through the `IHolidayProvider` interface. 

Call `DateTimeExtensions.SetHolidayProvider()` to set your own provider. If you do not set a provider, the `NagerHolidayProvider` will be used, but you need to provide a valid license key.

### Examples


### NuGet
The NuGet package is available via [NuGet](https://www.nuget.org/packages/MoreDateTime)<br>

```
PM> install-package MoreDateTime
```

<details>
  <summary>Code Examples (click to expand)</summary>
  
## Examples for .NET (NuGet package)

### Do something simple with a date
```cs

 --> code sample here, coming asap
```

</details>

