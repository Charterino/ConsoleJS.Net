# ConsoleJS.Net
A JS code runner with NPM packages support.

Warning: using this library can potentially result in code injection vulnerabilities. Be careful with what you pass to Evaluate() and always validate user input.
I take no responsibility for any damage.

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
