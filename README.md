# Common Libraries for .NET <img src="http://dfbuild.cloudapp.net/app/rest/builds/buildType:CommonLibrariesForNet_DebugCiBuild/statusIcon" />

The Common Libraries for .NET provides functionality used by the [Force.com Toolkit for .NET](https://github.com/developerforce/Force.com-Toolkit-for-NET) and the [Chatter Toolkit for .NET](https://github.com/developerforce/Chatter-Toolkit-for-NET). While you can use the Common Libraries for .NET independently, it is recommended that you use it through one of the toolkits.

These libraries were built using the [Async/Await pattern](http://msdn.microsoft.com/en-us/library/hh191443.aspx) for asynchronous development and .NET [portable class libraries](http://msdn.microsoft.com/en-us/library/gg597391.aspx), making it easy to target multiple Microsoft platforms, including .NET 4/4.5, Windows Phone8, Windows 8/8.1, and Silverlight 5.

## NuGet Packages

### Published Packages

You can try the library immmediately by installing this [NuGet package](http://www.nuget.org/packages/DeveloperForce.Common/).

```
Install-Package DeveloperForce.Common
```

### DevTest Packages

If you want to use the most recent DevTest NuGet packages you can grab the packages built during the CI build. All you need to do is setup Visual Studio to pull from the build servers NuGet feed:

1. In Visual Studios select **Tools** -> **Library Package Manager** -> **Package Manager Settings**. Choose **Package Sources**.
2. Click **+** to add a new source.
3. Change the **Name** to "Salesforce Toolkits for .NET (DevTest)".
4. Change the **Source** to http://dfbuild.cloudapp.net/guestAuth/app/nuget/v1/FeedService.svc/.

Now you can choose to install the latest NuGet from this DevTest feed.
