# Castle.Windsor.ContainerValidator [![Build status](https://ci.appveyor.com/api/projects/status/km7el3evvelhtl6f/branch/master?svg=true)](https://ci.appveyor.com/project/OshryHorn/Castle.Windsor.Validations/branch/master) [![NuGet Badge](https://buildstats.info/nuget/Castle.Windsor.Validations)](https://www.nuget.org/packages/Castle.Windsor.Validations/)
Plugin for validation of dependencies in given Castle.Windsor.IWindsorContainer and Castle.Windsor.IWindsorInstaller objects.  

## What for?
When heavily using the Castle.Windsor IWindsorContainer/IWindsorInstaller, it's easy to lose track of dependencies and miss one or two.
The Castle.Windsor.Validations provides extension methods for validating that all of the dependencies in a IWindsorContainer/IWindsorInstaller can be resloved with just **one line of code (!!!)**.
> <sub>Before you say "it can't happe to me", these are the cases I came accross so far where we missed dependencies:  
> <sub> 1. While refactoring a class was split, but the new half was not added to the installer.  
> 2. A new guy added another dependency to a constructor and didn't add it to the installer.  
> 3. An interface was added to an existing class which was already in the installer, but the installer regestration was not corrected.  
> 4. We added caching with CacheMeIfYouCan (definitelly recommend) but made verious mistakes in the factories in the installer.  
> 5. More :P 
  
## How to install?
The easiest way would be to just install the NuGet package through the Visual Studio Package Manager, or to run the following in the Package Manager Console: ```Install-Package Castle.Windsor.Validations```

## How to use?
```csharp
[Fact]
public void TestIocContainer()
{
  var container = new WindsorContainer().Install(new MyMainWindsorInstaller());
  container.ValidateAllDependenciesResolvable();
}
```  
  
  
#### Please tell me if there are any issues/problems/bus/requests! :D

### Have fun! 
