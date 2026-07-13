
# QuickLinks
1. [What is Code Coverage](https://github.com/solitontech/CSharp_Starter_Repo/blob/main/docs/Code%20Coverage.md#what-is-code-coverage)
2. [How to implement Code Coverage in C# Xunit](https://github.com/solitontech/CSharp_Starter_Repo/blob/main/docs/Code%20Coverage.md#how-to-implement-code-coverage-in-c-xunit)

# What is Code Coverage
Code coverage is a **metric that can help you understand how much of your source is tested**. 
It's a very useful metric that can help you assess the quality of your test suite, and we will see here how you can get started with your projects. 
    
# How to implement Code Coverage in C# Xunit 
We can able to check the code coverage for the Xunit project with the help of `coverlet.Collector` Package.<br>

    dotnet test --collect:"XPlat Code Coverage"

which generates a `coverage.cobertura.xml` file which consist of the Test case coverage result such as Line-Rate, Lines-Covered by the test project and many other details related to the coverage.<br>
![Step1](https://github.com/solitontech/CSharp_Starter_Repo/blob/a3fe477a372c29eca2aee065527d7e894efdfb15/docs/assets/Images/CodeCoverage/CodeCoverage_path.jpg) 
<br>This is how the `coverage.cobertura.xml` looks like this.<br>
![Step1](https://github.com/solitontech/CSharp_Starter_Repo/blob/a3fe477a372c29eca2aee065527d7e894efdfb15/docs/assets/Images/CodeCoverage/CodeCoverage_samplefile.jpg) 
<br>**Note:** Code coverage feature is available only in Visual Studio Enterprise edition **by default**.<br>

So the Code coverage output can be listed out in many ways 
- Using Console
-  Using Report Generator which generates a HTML Page based on  the XML file (dotnet tool)

## 1) Using Console
   So to print the result in output we have used `coverlet.msbuild` NuGet Package which able to show the results in a tabluar form in the Console.
   This will provide the output result in the console only you have installed the `coverlet.msbuild` NuGet package.

     dotnet test --no-build -p:CollectCoverage=true

   Use this command to generate the result.<br>
![Step1](https://github.com/solitontech/CSharp_Starter_Repo/blob/a3fe477a372c29eca2aee065527d7e894efdfb15/docs/assets/Images/CodeCoverage/Coverlet.Msbuild.jpg) <br>
We can also set the threshold value for the code coverage of a particular project.
For that use this command
    
    dotnet test /p:CollectCoverage=true /p:Threshold=80
    
## 2) Using Report Generator
we can easily generate a report based on the XML file using **Report Generator** dotnet tool.
To install the tool we need to use the command in the command prompt

    dotnet tool install -g dotnet-reportgenerator-globaltool

Once installed use this command to generate a report

    reportgenerator "-reports:TestResults/**/*.xml" "-targetdir:TestReports" -reporttypes:Html

Here we need to mention **our test project's .XML file path** in the report argument.
<br>![Step1](https://github.com/solitontech/CSharp_Starter_Repo/blob/a3fe477a372c29eca2aee065527d7e894efdfb15/docs/assets/Images/CodeCoverage/reportgenerator_cmd.jpg) <br>
Here is the output file which is generated.
<br>![Step1](https://github.com/solitontech/CSharp_Starter_Repo/blob/a3fe477a372c29eca2aee065527d7e894efdfb15/docs/assets/Images/CodeCoverage/reportgenerator_output.jpg) <br>

# Reference 
[Setting up the Threshold](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/MSBuildIntegration.md) <br>
[Coverlet GITHUB](https://github.com/coverlet-coverage/coverlet/tree/master) <br>
[Repoert Generator GITHUB](https://github.com/danielpalme/ReportGenerator) <br>
[Differences between Line and Branch coverage](https://stackoverflow.com/questions/8229236/differences-between-line-and-branch-coverage) 
