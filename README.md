# ConsoleJS.Net
A JS code runner with NPM packages support.

Warning: using this library can potentially result in code injection vulnerabilities. Be careful with what you pass to Evaluate() and always validate user input.
I take no responsibility for any damage.

The library requires Node and either npm or yarn. If it sees both npm and yarn installed, it will use yarn.

## Usage
```cs
var codeRunner = new JSCodeRunner();
codeRunner.InstallPackages("axios");
var jsCode = @"
  const axios = require('axios');
  axios.get('http://somewebsite.com').then((response) => 
  {
    console.log(response.data);
  });
";
Console.WriteLine(codeRunner.Evaluate(jsCode));
```
This will result in node_modules folder and package files created in your running directory.
To avoid this and store all JS-related files in a separate directory, consider using a specified folder for the environment.
Like this:
```cs
//This code runner will run in a folder 'jsEnvWithAxios'. It will be automatically created and populated if it doesnt exist.
var codeRunner = new JSCodeRunner("jsEnvWithAxios");
codeRunner.InstallPackages("axios");
var jsCode = @"
  const axios = require('axios');
  axios.get('http://somewebsite.com').then((response) => 
  {
    console.log(response.data);
  });
";
Console.WriteLine(codeRunner.Evaluate(jsCode));

//This code runner will run in a folder 'jsEnvWithAxios'. It will be automatically created and populated if it doesnt exist.
var codeRunner = new JSCodeRunner("jsEnvWithSomeOtherPackage");
codeRunner.InstallPackages("someOtherPackage");
var jsCode = @"
  const axios = require('someOtherPackage');
";
Console.WriteLine(codeRunner.Evaluate(jsCode));
```

If you don't need any npm packages, you can run everything entirely in-memory.
```cs
var codeRunner = new JSCodeRunner();
var jsCode = @"
  console.log('Simple JS doesn't need any npm packages :3');
";
Console.WriteLine(codeRunner.Evaluate(jsCode));
```
