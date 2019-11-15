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
            Every middleware has access to incoming request abd out going response.
            We have different middlewares among that we have two types
                <ul>
                    <li>Terminal middleware (app.Run) <br/>Once process the request, produce the response and pipeline revertes itself from here. </li>
                    <li>Continueing middleware(ap.Use) once process request, its trasnfer the request to next middleware</li>
                </ul>
       </p>
       <p>
        We can inject ILogger to Configure method so that we can log activities.
        <br/>ex: <b>ILogger<Startup> logger</b>
       </p>
       <p>
        For displaying static page like image or html we need to add app.UseStaticFiles middleware and to use default html file(default.html) we need to add app.UseDefaultFiles(). But the order should br app.UseDefaultFiles and then app.UseStaticFiles(). <br>
        In app.UseDefaultfiles we have option(DefaultFilesOptions) to change the default file from default.html file to any other files.
        <br>Likewise instead of useing app.UseDefaultFiles() and app.UseStaticFiles() we can use one middleware called app.UseFileServer() middleware. this also accept file option to change default file(FileServerOption)
       </p>
       <p>
            <h3>Environment Variable</h3><br>
            Environment varibale for various profile will be found in launchSettings.json, we can also set the environment varible by adding new environment variable in Control panal -> System : Environment Variable "ASPNETCORE_ENVIRONMENT = Development"
       </p>
    </p>
</p>
<p>
    <h2>ASP.NET MVC</h2>
    <p>
        A controller method can returns ObjectResult, HttpResponse, JSonResult or ViewResult. Since we create MVC application controller method returns ViewResult. ex return View(Object). <br>
        When we return View from controller, it check wether we have a .cshtml page under the following directories (Let assumt our controller is Home and Mthod is Details)
        <br>
        <ul>
            <li>/Views/Home/Details.cshtml</li>
            <li>/Views/Shared/Details.cshtml</li>
            <li>/Pages/Shared/Details.cshtml</li>
        </ul>
        <br>
        Let say we have Two controller and methods on it and the view directory structure as follows<br>
        <p>
            <ul>
                <li>Employee Controller<ul>
							<li>Details</li>
							<li>Edit</li>
							<li>List</li>
						</ul>
				</li>
				<li>Home Controller
					<ul>
						<li>Details</li>
						<li>Edit</li>
						<li>Index</li>
					</ul>
				</li>
			</ul>
			Then View directory will be <br>
				View -> Employee -> Details.cshtml, Edit.cshtml,List.cshtml <br>
				View -> Home -> Details.cshtml, Edit.cshtml, Index.cshtml
        </p>
        <p>
            By default mvc expect view same as the action method but we can change this conversion.<br>
                <i>return View("ViewName", ObjectModel)</i> <br>
            Also View can also have our view anywhere and specify the absolute path when call the view
            <i>return View("Myviews/Test.cshtml"); </i>
        </p>
        <p>
            <h3>Passing data to View</3>
            We can pass data to view in three ways
            <ul>
                <li>ViewData</li>
                <li>ViewBag</li>
                <li>Strongly Typed View</li>
            </ul>
            <b>View Data : </b> <br>
                      -> ViewData["Employee"] =objEmployee;<br>
                      -> ViewData["PageTitle"] = "Employee Details";<br>
            return View();<br>
            From cshtml side we can read these data <br>
            @{
                var employee = ViewData["Employee] as Asp_Net_MVC_.Model.Employee;
            }
            Then displau it using Name @employee.Name <br>
             <b>View Bag : </b> <br>
              ViewBag is a wrapper around ViewData here we can use dynamic property.<br>
              ViewBag.Employee = objEmployee; <br>
              <i>ViewBag.title = "Employee Details";</i></br>
              Also when we display fields we can use like <br>
              ViewBag.Title <br> and we don't want to type cast objects to its type but we can directly use <br>
              <i>ViewBag.Employee.Name</i>
        </p>
        <p>ViewData and ViewBag are loosely typed view</p>
        <p>
           <b>Strongly Typed View : </b> <br>
           We can directly pass object to the view in the return View statement like <br>
           return View(employee);<br>
           and from view side we need to include @model directive on top of the page like <br>
            @model Mysample.Models.Employee<br>
            Now my model is strongly type and when we say @Model.Name  -> we'll get the intellisence.
        </p>
        <p>
            <h3>ViewModel in ASP.Net Coew MVC</h3><br>
            
        </p>
    </p>
</p>
