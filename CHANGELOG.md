# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).


## [0.1.0] - 2023-04-06
 
### Added
- everything so far

--------------------------------------------------------


## [2.0.0] - 2026-10-28
 
### Added
- Additional unit tests and edge case coverage
- GitHub Action for NuGet.org trusted publishing

### Changed
- Updated target framework to .NET 10 (`net10.0`)

### Removed
- Removed `IDateTimeProvider` and `DateTimeProvider` (superseded natively by `System.TimeProvider` in .NET 8+)
