# auto-nunit-specflow

Testautomation framework using c#, nunit and specflow

## Getting Started

1. Download or clone repository using git
2. Open project in VS
3. In VS run Update-Package command in package manager console
 PM> Update-Package
4. VS top menu > Build > rebuild SpecFlow 

### Prerequisites

1. Visual Studio
2. System level path to chromedriver
3. Chrome, version required is 74

## Running the tests

pretty way
1. Using VS Test Explorer > Run All. Default Browser will be Chrome, version required is 74

not so pretty way
1. open terminal 
2. cd to <project_dir>
3. cd .\packages\NUnit.ConsoleRunner.3.10.0\tools
4. nunit3-console.exe --testparam:BrowserName=Firefox ..\\..\\..\\SpecFlow\bin\Debug\SpecFlow.dll
