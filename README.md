# Learn .Net Core Concepts
<h2>What is InProcess and OutProcess in the .csproj file?</h2>
<p>
    When CreateDefaultBuilder() method in the programm.cs calls UseIIS() method and host the app inside the IIS worker process (W3wp.exe or iisexpress.exe)
</p>
<p>
    When we run the .net aplication within Visual studio it uses "iisexpress" or from CLI it uses (using dotnet run) "net" as the process that running and hosting application.
</p>
<p>
    To set OutProcess we can change the "AspNetCoreHostingModel" to OutProcess or simple remove "AspNetCoreHostingModel" node from the node. <br/>
    In outprocess hosting we two types of web server for internal it is Kestrol for external it may IIS, Appachi or Nginx.
</p>

<p>
    We can store any configuration value in appsettings.json and by injecting IConfiguration we can access the configuration file content.
    If you have same key in appsetting.Development.json then this over write appsetting.json key.
    <br/>
    IConfiguration service read values from the following config files in the given order
        <ol>
            <li>File (appsetting.json, appsetting.{environment}.json</li>
            <li>User secrets</li>
            <li>Environment Variables (inside launchSettings.json -> profile -> in any profile)</li>
            <li>Command-line Arguments (dotnet run MyKey="Test")</li>
        </ol>

</p>
<p>
    <h2>Middleware</h2>
        <p>In Asp.net core middleware is pice of software that can handle hhtp request and respose.
        ex. Exception Hanlder, Authenticate users, Serve static files. Middleware are use to setup the Http request pipeline.  Configure method of startup.cs is the place to confiure our middleware.<br/>
       </p>
       <p>
       </p>
</p>
