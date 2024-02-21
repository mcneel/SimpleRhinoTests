# SimpleRhinoTests

Example project to setup unit tests in Rhino >=8 using [Rhino.Testing](https://www.nuget.org/packages/Rhino.Testing) library.

## Settin Up Your Project

### Package References

Add these package references to your project (.csproj). These references ensure your tests are discoverable by the test runner:

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="NUnit" Version="3.*" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.*" />
    <PackageReference Include="Rhino.Testing" Version="8.0.6-beta" />
  </ItemGroup>
```

### Rhino.Testing Configuration

Rhino.Testing will use `Rhino.Testing.Configs.xml` file to read `RhinoSystemDirectory` and setup necessary assembly resolvers for the target Rhino.

`Rhino.Testing.Configs.xml` can also contains any other configuration you want for your project. See [Rhino.Testing](https://github.com/mcneel/Rhino.Testing/blob/main/README.md) for more information on how to access the configurations.

Make sure this file is copied onto the build folder (where `Rhino.Testing.dll` exists):

```xml
  <ItemGroup>
    <None Update="Rhino.Testing.Configs.xml" CopyToOutputDirectory="always" />
  </ItemGroup>
```

### Setup Fixture

Implement the `Rhino.Testing.Fixtures.RhinoSetupFixture` abstract class in your test library to setup and teardown your testing fixture:

```csharp
    [SetUpFixture]
    public sealed class SetupFixture : Rhino.Testing.Fixtures.RhinoSetupFixture
    {
        public override void OneTimeSetup()
        {
            base.OneTimeSetup();

            // your custom setup
        }

        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();

            // you custom teardown
        }
    }
```

### Test Fixture

Implement the `Rhino.Testing.Fixtures.RhinoTestFixture` abstract class in your test library, add methods for each of your test and make sure to add the `[Test]` attribute to these methods:

```csharp
    [TestFixture]
    public sealed class PrimitivesFixture : Rhino.Testing.Fixtures.RhinoTestFixture
    {
        [Test]
        public void YourRhinoTest()
        {
            // you rhino test
        }
    }
```

### Referencing RhinoCommon

If you need to use RhinoCommon (other other Rhino assemblies) in your project, reference them based on the target framework, and **make sure they are NOT copied to the build directory** as they are shipped with Rhino:

```xml
  <PropertyGroup>
    <RhinoSystemDirectory>C:\Program Files\Rhino 8\System</RhinoSystemDirectory>
  </PropertyGroup>

  <ItemGroup Condition=" $(TargetFramework.Contains('net7.0')) == 'true'">
    <Reference Include="$(RhinoSystemDirectory)\netcore\RhinoCommon.dll" Private="False" />
  </ItemGroup>

  <ItemGroup Condition=" $(TargetFramework.Contains('net48')) == 'true'">
    <Reference Include="$(RhinoSystemDirectory)\RhinoCommon.dll" Private="False" />
  </ItemGroup>
```

## Local Testing

### From Visual Studio

- Open `SimpleNUnitTests.sln` in Visual Studio
- Open **Test Explorer** panel (From Tools -> Test Explorer menu)
- Build project
- **Test Explorer** panel will populate with tests for all frameworks the project is being built for.
- Right-Click on any branch of the test tree to **Run** or **Debug** tests in that branch.

![](./docs/vs-testing.png)

- You can access the test configurations from the main toolbar as shown below:

![](./docs/test-configs.png)

- If native debugging is needed, activate option shown below:

![](./docs/native-test-debug.png)

### From VSCode

- Open this directory in vscode
- Make sure [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension is installed
- Make sure **Testing** panel is active on the left activity bar
- Testing panel will populate with tests for all frameworks the project is being built for.
- Click **Run** or **Debug**button on any branch of the test tree or Right-Click to run or debug tests in that branch

![](./docs/vscode-testing.png)

![](./docs/vscode-testing-rclick.png)

- You can also click on the test button in the editor gutter to run individual tests from the source. This will run the selected test, in all the target frameworks:

![](./docs/vscode-testing-gutter.png)


### From Rhino (Work in Progress)

- Ensure RhinoNUnit plugin is loaded
- Run `NUnit` command
- Provide the path of compiled assembly that contains the tests. In this example it would be:
  - `SimpleNUnitTests/bin/Debug/net48/SimpleNUnitTests.dll` for Rhino running in dotnet framework (`net48`)
  - `SimpleNUnitTests/bin/Debug/net7.0-windows/SimpleNUnitTests.dll` for Rhino running in dotnet framework (`net7.0-windows`)
- A window like below will pop up. Click **Start** to run the tests:

![](./docs/nunit-in-rhino.png)

### From shell

- Open Powershell
- Change directory to this project (`cd /path/to/SimpleNUnitTests/`)
- Run the command below to run tests for both `net48` and `net7.0-windows`. Use `-c` flag to specify a configuration:

```shell
dotnet test ./SimpleNUnitTests.sln -c Release
```

- Use `-f` flag to specify a framework (`net48` and `net7.0-windows`):

```shell
dotnet test ./SimpleNUnitTests.sln -f net48
```
## Automated Testing

### Using Github Actions (Work in Progress)

An example github workflow is setup under `.github/workflows/main.yml` that triggers running tests on every push to this repository.

## Contribute:

[Rhino.Testing](https://www.nuget.org/packages/Rhino.Testing) source repository is here: https://github.com/mcneel/Rhino.Testing

Feel free to fork, improve, and submit pull requests.