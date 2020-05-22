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
            <p>
                Some times the view that we pass to the view may not have complete details so we create a separate class to incorparate all the fields we need in the view is call View Model.
            </p>
            <p>
                <b>Layout View</b><br>
                Layout view helps to design a common look-and-feel page design. layout view will be in Shared folder under View folder.<br> Add new file and Search for Razor and select Razor Layout view<br>
                Once we have Layout view, that will have all initial HTML elements so we can remove all the HTML initial element from our exsiting views.<br> Now we want to tell pages to use the layout view as follows
                <pre>
                    @{ 
                        Layout = "~/Views/Shared/_Layout.cshtml";
                        ViewBag.Title = "Employees List";
                    }
                </pre>
                <br>
                If you want to include a javascript file to every pages (views) we can smple create our javascript file under wwwroot/js foler and in Layout file we can specify the link before closing body tag, But if you want to include some javascript links only some specfic pages we need to specify that using <i>@RenderSection("Script")</i> just before closing body tag and in the particular page we need to have "Script" section. Suppose if you mentioned <i>@RenderSection("Script")</i> in Layout page and your view doesn't have "Script" section then we'll get a exception page so we can have that optional as follows <i>@RenderSection("Script", required:false) </i>.<br>
                OR we can check if that section available using if statement and enable it <br>
                <pre><i>
                    @if (IsSectionDefined("Scripts"))
                    {
                        @RenderSection("Scripts", required: true)
                    }
                </i>
                </pre>
                Specifying section in the View pages
                <pre>
                    <i>
                    @section Scripts{ 
                        <script src="~/js/CustomScript.js"></script>
                        }
                    </i>
                </pre>
                <br>
                <b>Setting Layout view in a common place:(ViewStart: _ViewStart.cshtml)</b><br>
                ViewStart is a separate razor page under View directory. ViewStart file may come in any position, in View folder or sub folder of views folder. And we can change the Layout file within a page so we can use different Layout file like,
                Layout = "_Layout2" <br> or if you don't want to use any layout page then we can say <i>Layout = null;</i>
                <br><p><b>ViewImport:</b>
                <br>
                    If we have common namespace for all pages, using ViewImport we can specify all common namespace so that we don't want to indutually specify in all view pages.
                    ex: lets say in a view page we are using a model like below
                    <pre>
                        @model Asp_Net_MVC.ViewModels.HomeDetailsViewModel
                    </pre>
                    and may pages using the same namespace "Asp_Net_MVC.ViewModels", instead of repeating same namespace in all pages we can create ViewImport view and can specify all th namespace and only class will be there in the view @model reference live 
                    <pre>
                        @using Asp_Net_MVC.ViewModels
                    </pre>
                    We can also place ViewImport file in the views subdirectory so inner viewimport override outer viewimport configuration.
                </p>
            </p>
        </p>
        <p>
            <h3>Routing</h3>
            <p>
                We have 
                <ol>
                    <li>Conventional Routing</li>
                    <li>Attribute Routing</li>
                </ol>
                <br>
                In conventional routing we configure the routing in the stratup.cs like <br>
                <pre>
                    app.UseMvcWithDefaultRoute();
                    app.UseMvc(routes => {
                        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    });
                </pre>
                here Home default controller and Index is the default action method on the Home controller.
                <br>
                In Attribute route we configure it in the controller level.<br>
                We can use <i>[Route("Home/Details/{id}")]</i> to define a route parameter. If you want to use a optional parameter route then
                <pre>
                    [Route("Home/UsingStronglyType/{id?}")]
                    public ViewResult UsingStronglyType(int? id)
                    {
                        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
                        {
                            Employee = _employeeRepository.GetEmpoyee(id??1),
                            PageTitle = "EmployeeDetails"
                        };
                        return View(homeDetailsViewModel);
                    }
                </pre>
                Also if you want mvc automatically assign route attributes to the controller/action/parametr we can specify the route as 
                <pre>
                    [Route("[controller]/[action]/{id?}")]
                    public ViewResult UsingStronglyType(int? id)
                </pre>
                We can also specify this in the controller level so that we don't want to repeat it in the method level, as below
                <pre>
                     [Route("[controller]/[action]")]
                     public class HomeController : Controller
                </pre>
            </p>
            <p>
                <h3>
                    Bootstrap
                </h3>
                <br>
                We have many tool to install and use Client-Side packages like,
                <ul>
                    <li>Bower</li>
                    <li>NPM</li>
                    <li>WebPack </li>
                    <li>LibMan>
                </ul>
                Here we are going to use <b>libman</b> for installing client-library<br>
                Step to install Bootstrap from Visual studio is
                <ol>
                <li>Right click project and select "Add -> Client-Side Library"</li>
                <li>leave cdnjs as provider and type Twitter...</li>
                <li>From the filtered list select Twitter bootstarb</li>
                <li>Make sure that Target Location is wwwroot/lib/twiiter-bootstrap/   --- (last directory may be in any name)</li>
                <li>Click Install</li>
                Now all the files are copied into wwwroot -> lib --> bootstrap folder.<br>
                add libman.json file aded to the project. it is a config file. It is very same as package.json file.<br>
                by editing this file we can manually install any files, like JQuery, etc...
                <br>If you manually insert some library, when you same the project all added library will e download and placed it in the relevent directory. We can clean local files and restore then by right click on libman.json. This json file in intelligence enable, itwill display library by partially typing the name.<br>
                Now we can refering any css file in our Layout.cshtml or any other view to style the web pages.
            </p>
            <p>
            <h3>Tag Helpers</3>
                Tag helper are server side component to create and render HTML elements.<br>
                How to add reference to tag helper,
                <pre>@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers</pre>
                We need this Tag helpers all over all the pages so we can add it _ViewImpoert.cshtml.<br> 
                Below are the method without using Tag helper
                <pre>
                    @Html.ActionLink("Text to appear in the link","Action Method","ControllerName", "routeParameters");
                    @Html.ActionLink("View","details","home",new {id = employee.Id })
                </pre>
                but in here we can't use style classes so here we can use Tag Helper like below,
                <pre>
                    &lt;a href="@Url.Action("details", "home", new {id = employee.Id })"  class="btn btn-primary"&gt;View&lt;/a&gt;
                </pre>
                BUT Tag helper bit easy to handler
                <pre>
                     &lt;a asp-controller="home" asp-action="details", asp-route-id="@employee.Id"  class="btn btn-primary"&gt;View&lt;/a&gt;
                </pre>
                here asp-route-&lt;ParameterName=Value&gt; 
                Example if you want to pass Id then <br>
                here asp-route-id = 1;
                <br>
                    We have @HTML.ActionLink or @Url.Link or event direct a tag ways to perform the same operation then why do we specifically go for Tag helper? <br>
                    Let say we have our default route configured in the startup like this<br>
                    <pre>
                        app.UseMvc(route => {
                            route.MapRoute("default","{controller=Home}/{action=Index}/{id?});
                        });
                    </pre>
                    This will works with all the method HTML.ActionLinke or URL.Link ornormal A tag. but let say if we adding our company name before controller like,
                                        <pre>
                        app.UseMvc(route => {
                            route.MapRoute("default","MyCompany/{controller=Home}/{action=Index}/{id?});
                        });
                    </pre>
                    so if we used other than Tag helper it will be fail because other methods doesn't add company name automatically before the controller but if we use Tag helper it will take the url and replace controller and method name along with the company name on it.
                    <br>
                    <h3>Image Helper</h3>
                    when we use an image tag in a web page, if the image changes in the server side the server will display the old image from the cache. To tell the browser each time the page load fetch the image from the server we can disable cache from network menu of developer tool or we can add <i>asp-append-version= "true" </i> in the image tag like below,
                    <pre>
                    <img class="card-img-top" src="~/images/noImage.jpg" asp-append-version="true" />
                    </pre>
                    if you see the aource from the view Source we can notice a unique hash value will be added to the image tag that identify if the image changes from the server then load it from the server or else load it from the cache.<br>
                    <h3>Environment Tag helper</h3>
                    We use CDN for our production environment most of the time, let say if there is a problem in CDN server we need to load our  minified javascript or css from our location for our production environment. This si the situation the Environment Tag helper helps.<br>
                    We can configure conditional reference for client library in MVC
                    <ol>
                        <li>Enviroment -> Include="production,staging,..."</li>
                        <li>Environment -> Exclude="development,...</li>
                    </ol>
                    <pre>
                        &lt;environment include="Development"&gt;
                            &lt;link href="~/lib/bootstrap/css/bootstrap.css" rel="stylesheet" /&gt;
                        &lt;/environment&gt; <br>
                            &lt;environment exclude="Development"&gt;
                            &lt;link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" 
                                integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" 
                                crossorigin="anonymous" asp-fallback-href="~/lib/bootstrap/css/bootstrap.min.css"
                                asp-fallback-test-class="sr-only"
                                asp-fallback-test-property="position"
                                asp-fallback-test-value="absolute"
                                asp-suppress-fallback-integrity="true"&gt;
                        &lt;/environment>
                    </pre>
                    What id <i>integrity param in link tag? It is a hash value that matches with the cdn content.if someone alter the css or hash value it will not process the css js from the cdn link<br>
                    If a cdn server is down we can use <b><i>asp-fallback-href</i></b> param of <i>link</i> so that in case of cdn server down mvc uses the local version of minified file. Along with <i>asp-fallback-href</i> there other attributes are there in <i>link</i> element.
                    <ul>
                        <li>asp-fallback-href -> f cdn server down, takes the local file</li>
                        <li>asp-fallback-test-class="sr-only" -> Test if sucessfull file download from cdn</li>
                        <li>as-fallback-test-property="position"  -> Test if sucessfull file download from cdn</li>
                        <li>asp-fallback-test-value="absolute"  -> Test if sucessfull file download from cdn</li>
                        <li>asp-suppress-fallback-integrity="true" -> if cdn down, when using local file we don't need integrity check</li>
                    </ul>
                    <h3>Bootstrap Navigation</h3>
                        <pre>
                        &lt;div class=&quot;container&quot;&gt; &lt;nav class=&quot;navbar navbar-expand-sm bg-dark navbar-dark&quot;&gt; 
                            &lt;a class=&quot;navbar-brand&quot; asp-action=&quot;index&quot; asp-controller=&quot;home&quot;&gt; 
                                &lt;img src=&quot;~/images/employee.gif&quot; height=&quot;30&quot; width=&quot;30&quot; /&gt; 
                            &lt;/a&gt; 
                            &lt;button type=&quot;button&quot; class=&quot;navbar-toggler&quot; data-toggle=&quot;collapse&quot; data-target=&quot;#collapsidleNavbar&quot;&gt; 
                                &lt;span class=&quot;navbar-toggler-icon&quot;&gt;&lt;/span&gt; 
                            &lt;/button&gt; 
                            &lt;div class=&quot;collapse navbar-collapse&quot; id=&quot;collapsidleNavbar&quot;&gt; 
                                &lt;ul class=&quot;navbar-nav&quot;&gt; 
                                    &lt;li class=&quot;nav-item&quot;&gt; 
                                        &lt;a asp-action=&quot;index&quot; asp-controller=&quot;home&quot; class=&quot;nav-link&quot;&gt;
                                        List
                                        &lt;/a&gt; 
                                    &lt;/li&gt; 
                                    &lt;li class=&quot;nav-item&quot;&gt; 
                                        &lt;a asp-action=&quot;create&quot; asp-controller=&quot;home&quot; class=&quot;nav-link&quot;&gt;
                                        Create
                                        &lt;/a&gt; 
                                    &lt;/li&gt; 
                                &lt;/ul&gt; 
                            &lt;/div&gt; 
                            &lt;/nav&gt; 
                            &lt;div&gt; 
                                @RenderBody() 
                            &lt;/div&gt; 
                            @*@if (IsSectionDefined(&quot;Scripts&quot;)) { @RenderSection(&quot;Scripts&quot;, required: true) }*@ 
                            @RenderSection(&quot;Scripts&quot;, required: false) 
                        &lt;/div&gt;
                        </pre>
                        <br>
                        <h3>Form TagHelper</h3>
                        Available importent tag helpers
                            <ul>
                                <li>Form Tag Helper</li>
                                <li>Input Tag Helper (Input asp-for="Field name of model")</li>
                                <li>Label tag Helper(lable asp-for="Field name of model")</li>
                                <li>Select Tag Helper(asp-items="Html.GetEnumSelectList<Dept>()")</li>
                                <li>TextArea Tag Helper</li>
                                <li>Validation Tag Helper</li>
                            </ul><br>
                            Here we are going to see Form Tag Helper<br>
               </p>
               <p>
                    <h3>Model Validation</h3>
                    Since we create lable like <br>
                    <pre>
                    &lt;label asp-for="Email" class="col-sm-2 col-form-label"$gt;&lt;/label&gt;
                    &lt;input asp-for="Email" class="form-control" placeholder="Email"&gt;
                    </pre>
                    here we said asp-for = "Email" in the lable that means we mapped model data field to the lable so if you configure
                    <pre>
                        [Display(Name ="Office Email")]
                        public string Email { get; set; }
                    </pre>
                    that will change the lable text
               </p>
               <p>
               We can also inject any interface into cshtml file as follows,
               <pre>
               @inject  IEmployeeRepository _empRepository;
               </pre>
               We can inject any interface to the view page using inject key word.
               </p>
        </p>
        <p>
            <h3>Entity Framework</h3>
            <b>EF</b> sites between <b>Domain & DbContext Classes and Database</b>. There are database providers to helps EF to communicate to the database. there are many database providers such as,<br>
                Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Sqlite, Microsoft.EntityFrameworkCore.InMemory, Microsoft.EntityFrameworkCore.Cosmos, etc. <br> Complete list of database provider can be found in the link <br>
                https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli <br>
                Database providers sits between <b>EF</b> and <b>Database</b><br>
                EF has common functionalities and methods to access all databases but database provider is the one which convert the EF functionalities into a particular database functionalities and commnuicate with the database.
                <br>
                <h4>Installation</h4>
                We need Entity FrameworkCore, EntityFrameworkCore.SqlServer and EntityFramework Relational to be install to develop entity framework module. Instead ofinstalling these three packages, if we install
                <b>Microsoft.EntityFrameworkCore.SqlServer</b> that has dependency package <b>Microsoft.EntityFrameworkCore.Relational</b> this has dependency <b> Microsoft.EntityFrameworkCore</b> so by installing Microsoft.EntityFrameworkCore.Sqlserver we get all three pacjages installed.
                <br>
                Creating DbContext instance in the stratup class have two options
                    <ol>
                        <li>services.AddDbContext</li>
                        <li>services.AddDbContextPool</li>
                    </ol>
                    AddDbContextPool provide DbContext pooling and use extsing instance instead of create a new instance. <br>
                    <b>EF Core Migration : </b><br>
                    In Package Manager Console we can run command to get about entity frame work sd follows <br>
                    <i>get-Help about_entityframeworkcore</i><br>
                    This will display list of command and description for that command<br>
                    <ul>
                        <li>Add-Migration       -   Adds a new migration.</li>
                        <li>Drop-Database       -   Drops the database.</li>
                        <li>Get-DbContext       -   Gets information about a DbContext type.</li>
                        <li>Remove-Migration    -   Removes the last migration.</li>
                        <li>Scaffold-DbContext  -   Scaffolds a DbContext and entity types for a database.</li>
                        <li>Script-DbContext    -   Generates a SQL script from the current DbContext.</li>
                        <li>Script-Migration    -   Generates a SQL script from migrations.</li>
                        <li>Update-Database     -   Updates the database to a specified migration.</li>
                    </ul>
                    We can remove migration using "Remove_migration" and it will remove last migration that haven't yet applied to database.If you want to remove already migrated to database migration, use <br>
                    <i>update-database [migration-name]</i> this will revert all the migration upto the specified migration name.<br>
                    This will remove migration from database not in the migration directory for that we need to run <i>remove-migraton</i> command as may times times we need. then if you want to correct it in the entity class.
                    <br>
                    <h4>Seedning Data</h4><br>
                    We can use OnModelCreating override mthod of DBContext class to seed our data.
                    <pre>
                        public static class ModelBuilderExtension
                        {
                            public static void Seed(this ModelBuilder modelBuilder)
                            {
                                modelBuilder.Entity&lt;Employee&gt;().HasData(
                                new Employee { Id = 1, Name = "Newman Croos", Department = Dept.IT, Email = "newmancroos@gmail.com" },
                                new Employee { Id = 2, Name = "John", Department = Dept.HR, Email = "john@gmail.com" }
                                );
                            }
                        }
                    </pre>
                    <br>
        </p>
        <p>
            <h3>404 Not Found</h3><br>
            There are two type of 404 not found
                <ol>
                    <li>Requested record is not found in the database so find method return null</li>
                    <li>Requested Route is not there in the application</li>
                </ol>
                For 1, we can create a Not found exception page related to the record user requested.
                for Route not found we can implement Not found using middleware in the startup class. There are there kinds of StatusCode page rediect there,
                <ol>
                <li>UseStatusCodePages  - This will simply displays a pages with simple 404 - not found erro rmessage</li>
                <li>UseStatusCodePagesWithRediect - This will allows us to create a separate page to display all error messages by creating a controller and view</li>
                <li>UseStatusCodePagesWithReExecute 0 this is also gives the same result as UseStatusCodePagesWithRedirect but intternally has some differences in the execution</li>
                <br>
                <b>What is the diferences between UseStatusCodePagesWithRedirect and UseStatusCodePagesWithReExecute></b><br>
                <li>
                    When we call a non existence route the following stages of executions happended in the request pipeline(Configure method of startup.cs)<br>
                    <ol>
                        <li>Requested url request comes to env.IsDevelopment() since it is not development environment the control goes to else part and to app.UseStatusCodPages[....]() method(s), at this point there is not status of the page so it transfer the control to UseStaticFile but the request is not related to static file, finally it comes to app.UseMvc route part. this is the place it identifys these url in not exist.</li>
                        <li>Status not for will be issued so the middle ware now travel in the backward direction and comes to app.UseStatusCodPages[....]() method(s) here it rediect the call to '/Error/{0}'  that means there is the url is temporarly change to "/Error/{0}' (status 302) and page redirect to "/Eror/{0}"</li>
                        <li>Now the redirect request flow thru same pipeline and then reaches to app.UseMvc  route and then calls "/Error/{0} controller method.</li>
                        <li>There is actual 404 response in Redirect method call we convert it to another controller method call and return error page. <b>We lost actual 404 error when we use app.UseStatusCodPagesRedirect</b></li>
                        <li>But in the app.UseStatusCodPagesReExecute method we achived the same result as end user but we get 404 status along with error page.</li>
                        <li>Process flow of app.UseStatusCodPagesReExecute is same as app.UseStatusCodPagesRedirect but when going back to non development environment condition the request comes to route with 200 status but app.UseStatusCodPagesReExecute will replce the 200 with 404 and render the page.
                        Also importently when we use app.UseStatusCodPagesReExecute the url (wrong url) what we gave is remain in the url bar of the browser, this will not change to "/Error/{0} like app.UseStatusCodPagesRedirect</li>
                    </ol>
                </li>
        </p>
        <p>
            <h3>Global Exception Handler</h3><br>
            <p>
                If you run the project all the execution steps will be output in the output windows if we disable all the entry by uncheck from Tools - Options - Debugs - output window we will not get any entries in the output windows.<br>
                If we change appsettings.json Logging - Microsoft to Information now output windows will display Microsoft information related logging where we can find our sql query too.<br>
                <strong>How these debug logging happen?</strong><br>
                We have something called <b>Logging Providers</b>, these logging providers physically store or display log.<br>
                Built in Logging providers: <br>
                <ul>
                    <li>Console Logging provider  - Display logs on console window</li>
                    <li>Debug Logging Provider - Display logs on Debug window in Visual studio</li>
                    <li>Event Source Provider</li>
                    <li>Event Log Provider</li>
                    <li>TraceSource Provider</li>
                    <li>AzureAppServiceFile Provider</li>
                    <li>AzureAppServiceBlob Provider</li>
                    <li>ApplicationInsights Provider</li>
                </ul>
                <br>
                Third Party Logging Providers<br>:
                <ul>
                    <li>NLog</li>
                    <li>elmah</li>
                    <li>Serilog</li>
                    <li>Sentry</li>
                    <li>Gelf</li>
                    <li>JSNLog</li>
                    <li>KissLog.net</li>
                    <li>Loggr</li>
                </ul>
            </p>
            To use built in logging functionality, we can simply inject ILogger&lt;ClassName&gt; and then can log error, information etc..
            The logg will be display in Output windows if you running from Visual studio and command prompt when you running from command line.
            <p>
            <b>Using NLog to log in files</b><br>
                To use Nlog logging first we need to install Nuget package <b>Nlog.Web.AspNetCore</b>
                Once we installed the nuget package we need to write the config file so create a new text file and rename it to <b>nlog.config</b> and add the following marup
                <pre>
                    &lt;?xml version="1.0" encoding="utf-8" ?&gt;
                    &lt;nlog xmlns="http:/www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"&gt;
                        &lt;targets&gt;
                            &lt;target name="allfile" xsi:type="File" fileName="c:\temp\nlog-all-${shortdate}.log" /&gt;
                        &lt;/targets&gt;
                        &lt;rules&gt;
                            &lt;logger name="*" minlevel="Trace" writeTo="" allfile="" /&gt;
                        &lt;/rules&gt;
                    &lt;/nlog&gt;
                </pre>
                open property of nlog.config file and make <b>Copy to Output Directoty = Copyif Newer</b>
                and then we need to add Nlog config to the program.cs. If we search <b> www.github.com/aspnet/aspnetcore </b> and search file <b>webhost.cs</b> there will be a funtion called <b>CreateDefaultBuilder</b> there we can find the <b>ConfigureLogging</b> extension method for configure <b>Console, Debug and EventSource</b> logging. We need to override this in our <b>programe.cs</b> like below.<br>
                <pre>
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.AddEventSourceLogger();
                        logging.AddNLog();
                    })
                </pre>
                now we can inject ILogger<ClassName> _logger and can log any information, error or warning.
                If you want a class wants only log Warning then we can specify it in the appsetting.json as follows,
                Let say HomeController only wants to log Warning, then in <b> appsetting.json - Logging - Loglevel -</b> add,
                <b>"[ProjectName].Controllers.HomeController" : "Warning"</b>
                Now eventhough if we log Information, Trace or Debug those will not be logged in to the log file.
                <br>
                We can also specify log category by logging provider, that mean we can say for <b>Logging provider "Debug" log only debug category logs ()All inbut provider (output, console, etc)</b> and logging provider Console log only Warning category log as follows,
                <br>
                <pre>
                "Logging": {
                            "Debug": {
                            "LogLevel": {
                                "Default": "Warning",
                                "Asp_Net_MVC.Controllers.HomeController": "Warning",
                                "Microsoft": "Warning"
                                //"Microsoft.Hosting.Lifetime": "Information"
                            }
                            },
                            "LogLevel": {
                            "Default": "Trace",
                            "Asp_Net_MVC.Controllers.HomeController": "Trace",
                            "Microsoft": "Trace"
                            //"Microsoft.Hosting.Lifetime": "Trace"
                            }
                        }
                </pre>
                here all inbuild logging provider will log only <b>Warning and above log category</b> and third part logging provider (NLog) log <b>Trace and above log category</b>
            </p>
    </p>
    <p>
        <h2>Asp.Net Core Identity</h2>
        <div>
            Asp.Net Core Identity gives the following supports
            <ul>
                <li>Create, Read, Update and Delete user accounts</li>
                <li>Account information</li>
                <li>Authentication and Authorization</li>
                <li>Pasword Recovery</li>
                <li>Two-factor authentication with SMS</li>
                <li>Support external login providers like Microsoft, Facebook, Google etc</li>
            </ul>
            As first step for Identity, we need to make our <b>AppDbContext class inherited from IdentityDbContext</b> class instead DbContext class but internally IdentityDbContext class is inherited from DbContext class with additional classes like UserManager, RoleManager ect.<br>
            In startup.cs we need to add <b>services.AddIdentity&lt;IdentityUser, IdentityRole&gt;</b> these IdentityUser and IdentityRole are part of IdentityDbContext.<br>
            If need to additional field to the user table (IdentityUser has almost all fields) we can create our Userclass inherit from IdentityUser class and add additional fields and use that class during <b>services.AddIdentity&lt;IdentityUserNew, IdentityRole&gt;</b><br>
            Now we need to specify the EntityFrameworkStore for the IdentityUser so the steps as follows
            <ol>
                <li>AppDbContext inherits from IdentityDbContext instead of DbContext</li>
                <li>In Startup services.AddIdentity&lt;IdentityUser, IdentityRole&gt;().AddEntityFrameworkStore&lt;AppDbContext&;gt;();</li>
                <li>In Configure method before UseMvc add <b>app.UseAuthentication();</b> </li>
                <li>Add new migration for create Identity related tables in database</li>
            </ol>
            if get error while running add-migration add <b>base..OnModelCreating(modelBuilder);</b> in AppDbContext OnModelCreating method.<br> 
        </div>
        <div>
            When we create user we can use UserManager&lt;IdentityUser&gt; to create user and SignInManager&lt;IdentityUser&gt; to login in the user. SignInManager has two parametrs one for the user and another for spcifing whether the user still login even after closing the brwser True will maintain the session even after closing the browser False signout the user once you close the browser.<br>
        </div>
        <div>
            We can use IServiceCollection parameter of ConfigureServices method to configure the <b>IdentityOptions</b>, Identity Options contains many options that can be configure IdentityObject (user, password, claims etc)
            for example we can change Password Option, Validating passowrd during creation of user has default password length, complexcity etc we can change thse option using
            <pre>
                services.Configure&lt;IdentityOptions&gt; (options => { options.Password.RequiredLength = 10;})
            </pre>
            same configuration we can change using
            <pre>
                services.AddIdentity&lt;IdentityUser, IdentityRole&gt;(options => options.Password.RequiredLength = 10;)
            </pre>
            <p>
            For server side valdation we use data annotation in the viewmodel class but it will increase server round trip and server load so we can use client validation using JQuery. We need
            <ol>
                <li>JQuery.js</li>
                <li>JQuery.Validate.js</li>
                <li>jQuery.Validate.unobtrusive.js</li>
            </ol>
            here unobtrusive validator will take server side validation rule and validate client side, we don't write any custom or extra code here. These js file will take care of the client side validation.
            </p>
            <p>
                Wee need to do some server side validation in this application for example when we create a user we need to validate a email is valid and not exist, for that we create a controllor method to validate email and return Json result, why we use Json result is ASP.Net core Mvc uses <b> Jquery Validate </b> to call server side vlidate function and Jquery Validate method expect a Json response from the server.<br>
                To call this server side function we are going to <b>Remote</b> attribute in the <b>Email property</b> of the <b>RegisterViewModel</b> <b> [Remote(action: "IsEmailInUse", controller:"Account")] </b><br>
                For remote validation we need the folowing client side libraries.
            <ol>
                <li>JQuery.js</li>
                <li>JQuery.Validate.js</li>
                <li>jQuery.Validate.unobtrusive.js</li>
            </ol>
            </p>
            <p>
                <h3>Custom Validation Attributes</h3>
                We can inherit <b>ValidationAttribute</b> to create custome validation attribute.
            </p>
        </div>
        <p>
            To create addtional field in Identity user we can inherit IdnetityUser and add more field and replace all the IdentityUser to Created class (ex. ApplicationUser). The importent thing is we need to tell AppDBContext to inheritr IdentityDbContext,
            <b>AppDbContext : IdentityDbContext&lt;ApplicationUser&gt; instead of <b>AppDbContext : IdentityDbContext</b>. 
        </p>
        <div>
            <h2><u>Role Bases Authorization</u></h2>
            <p>
                Once we logged in then using [Authorize] is basic authorization. If we need a role authorization we need to specify
                [Authorize(Role="Admin")] here Role is the admin and login user should blongs to Admin group.<br>
                For hiding administration menu from the non admin user we can put <br>
                <b>signInManger.IsSignedIn(User) and User.IsInRole("Administrator) </b> check and have our admin menu with this condition.
            </p>
        </div>
        <div>
            <h2><u>Claim Base Authorization</u></h2>
            There are two simple steps to implement Claim based authorization
                <ol>
                    <li>Create Claims Policy</li>
                    <li>Use te policy for authorization checks</li>
                </ol>
                Claims are policy based that mean we create policy and add some claims on it. then need to register the claim policy.
                <pre>
                    services.AddAuthorization(options =&gt;
                    {
                        options.AddPolicy("DeleteRolePolicy",
                            policy =&gt; policy.RequireClaim("Delete Role")
                                            .RequireClaim("Create Role"));
                    });
                </pre>
                now we can use it in controller method like
                <pre>
                    [Authorize(Policy = "DeleteRolePolicy")]
                </pre>
                <pre>
                Role is also type of claims so we can get the claim type role as follows,
                User.Claims.Where(x =&gt; x.Type == ClaimTypes.Role)
                </pre>
                same way we can add roles in a policy like below
                <pre>
                    services.AddAuthorization(options =&gt;
                    {
                        options.AddPolicy("AdminRolePolicy",
                            policy =&gt; policy.RequireRole("Admin, Test Role");
                    });
                </pre>
                when we show/hide admini menu for the user we inject SignInManager&lt;ApplicationUser&gt; like below,
                <pre>
                    @using Microsoft.AspNetCore.Identity
                    @inject SingnInManager&lt;ApplicationUser&gt; SignInManager;
                </pre>
                and then have a code block to check if the user is logged in and has admin role like below,
                <pre>
                    @if(SgnInManger.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        Manage Like here
                    }
                </pre>
                Lets take an example a user <b>A</b> has <b>admin role</b> with <b>only Create User role<b> not <b> Delete User and Edit User Role</b> as per our coding since he has admin role edit and delete user button is enabled. We need to disable Edit and Delete button by checking his Policy,
                <pre>
                    @using Microsoft.AspNetCore.Authorization
                    @inject IAuthorizationService authorizationService
                    @if ((await authorizationService.AuthorizeAsync(User, "EditRolePolicy")).Succeeded)
                    {
                        &lt;a class="btn btn-primary" asp-action="EditRole" asp-controller="Administration" asp-route-id="@role.Id"&gt;Edit&lt;/a&gt;
                    }
                </pre>
                When we browse to a url that we have no access Asp.Net will automatically redireect to <b>/Account/AccessDenied</b> route. If you want to change this default behavior we can change it thru Startup.css --- ConfigureServices method like below,
                <pre>
                    //Change default access denied redirect path, also we have many options to change 
                    // Login route, logour route.....
                    services.ConfigureApplicationCookie(options =&gt; {
                        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/administration/accessdenied");
                    });
                </pre>
                in this case the access denied controller method and access denied view should be there under administration controller and view.
                <p>
                    All the above claim based authorization we have used only <b>Claim Type</b> not claim type with claim value.
                    for example : <br>
                    <pre>
                        services.AddAuthorization(options =&gt;
                        {
                            options.AddPolicy("EditRolePolicy",
                                policy =&gt; policy.RequireClaim("Edit Role"));
                        });
                    </pre>
                    here what we defined a policy such a way that if a user has claimtype "Edit Role" we defined a policy.We are not using the claim value.<br>
                    So we are going to change the saving part of the claim. If you select a claim for a user we are going to save true for that claim else false.
                    we can change the start up class as below
                    <pre>
                        services.AddAuthorization(options =&gt;
                        {
                            options.AddPolicy("EditRolePolicy",
                                policy => policy.RequireClaim("Edit Role", "true"));
                        });
                    </pre>
                    and manage user claim as below
                    <pre>
                        if (existingUserClaims.Any(x => x.Type == claim.Type && x.Value == "true")) 
                        {
                            userClaim.IsSelected = true;
                        }
                    </pre>
                    we can also use any value for claim value not neccssaryly use "true" or "false";
                    <pre>
                        services.AddAuthorization(options =>
                        {
                            options.AddPolicy("AllowedCountryPolicy",
                                policy => policy.RequireClaim("Countries", "India", "USA", "UK"));
                        });
                    </pre>
                    here Countries is policy type and "India", "USA" and "UK" are values.
                </p>
        </div>
        <div>
            <h3>Custom Authorization</h3>
            <p>
                <u>Why do we need custom authorization?<br>
                Lets consider a situation, We have List role and edit role, for now for editing a role a user need
                Admin role and EditPolicy, suppose if need the condition like this, To edit a role a user need <b>(Admin Rol + EditPoly)</b> or <b>superadmin role</b>
                <pre>
                    services.AddAuthorization("EditRolePolicy", policy =&gt; 
                    policy.RequireRole("Admin")
                          .RequireClaim("Edit Role", "true", "Yes") // Edit Role value should be treu or yes
                          .RequireRole("Super Admin"));
                </pre>
                <b>BUT THIS WILL NOT WORK, BECAUSE THESE STATING THE A USER MUST REQUIRE "Ediy Role", EditRolePolicy AND Super admin role</b>
                This is the situation we go for custome authorization.
            </p>
            <p>
                <b>Custom authorization using Func(RequireAssertion)</b>
                so for being adminitrator or super admin we can't use RequireRole or RequireClaim directly so we use <b>RequireAssertion</b>
                <pre>
                    services.AddAuthorization(options =&gt;
                    {
                        options.AddPolicy("AdminRolePolicy",
                                policy =&gt; policy.RequireRole("Administrator"));
                        options.AddPolicy("EditRolePolicy",
                            policy =&gt; policy.RequireAssertion(context =&gt;
                            context.User.IsInRole("Administrator") && 
                            context.User.HasClaim(claim => claim.Type ==  "Edit Role" && claim.Value == "true") ||
                            context.User.IsInRole("Super Admin")
                            ));
                    });
                </pre>
                here, with role (administrator and claim Edit Role) OR role (Super Admin) a user can edit User role
            </p>
            <p>
                <b>Custom Authorization Requirement and Handler </b>
                <p>
                    To build custom authorization we need to create Requirement by inheriting <b>IAuthorizationRequirement </b> inteface, it is an empty interface doesn't have any methods.Then need Authorization handler which inherite Authorization requirement and have authorization logic in the handler.</p>
                    <p>For custome authorization attribute we need to create <b>custom requirement and handler</b></p>
                    <p>
                        To create cutome authorization attribut we create a requirement class by inheriting <b>IAuthorizationRequirement</b> inter face and to create handler we create a class by inheriting <b>AuthorizationHandler<T></b> class.
                        where T is type of requirement and then we need to register these custome authorization requirement and Handler in strtup.cs
                        <pre>
                                options.AddPolicy("EditRolePolicy",
                                policy =&gt; policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
                        </pre>
                        AND
                        <pre>
                            services.AddSingleton&lt;IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler&gt;();      
                        </pre>
                        Here we have a requirement with one handler but a Requirement may have more than one handler also,
                    </p>
                    <p>
                    <b>Why do we need multiple handler for a requirement?</b>
                        Lets say We have OR condition, in our example a admin with Edit claim can edit the role OR a super admin can edit the role. so to implemnent these condition we can have two handlers one for (Admin and Edit Claim) another handler for (Super Admin). Once we create a second handler we need to register it in the startup.cs and we are good to go.
                    </p>
                    <pre>
                        services.AddSingleton&lt;IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler&gt;();   
                        services.AddSingleton&lt;IAuthorizationHandler, SuperAdminHandler&gt;();   
                    </pre>
                    A handler can return Success, Failure or nothing. Failure take over the success that means if we have two handler and one f them is failed then even though other handler return success the final result is failure.<br> In this situation if you have second handler, it will be called and what ever the result if the first one failed the final result wil be failure. If you want to stop execution of second handler if the first handler failed then you can explicitly call InvokeHandlerAfterFailure = false the default value is true. this setup should be there in the ConfigureServices method of startup class.
                    <pre>
                        services.AdAuthorization(options =&gt;
                        {
                            options.AddPolicy("EditRolePolicy", policy =&gt;
                            {
                                policy.AdRequirement(new ManageAdminRoleClaimPolicyRequirement());
                                options.InvokeHandlersAfterFailure =false;
                            })
                        })
                    </pre>
            </p>
        </div>
    </p>
    <p>
        <h1>Entity Framework Scaffolding from existing database</h1>
        <ul>
            <li>
                Install "Microsoft.EntityFrameworkCore.Design" and "Microsoft.EntityFrameworkCore.SqlServer"
            </li>
            <li>
                For scaffolding we need to install a tool called "dotnet-ef" <br/>  <b>dotnet tool install -global dotnet-ef</b> 
            </li>
            <li>
                Now we are ready to scaffolding the database using below command line execution. Now if you run <b>dotnet ef</b> you'll get a ef tool introduction. to scaffolding,(Ned to run this command within project folder) <br/>
                <b>dotnet ef dbcontext scaffold "|ConnectionString|" Microsoft.EntityFrameworkCore.SqlServer -d -c |DbContextName| --context-dir EfStructures -o Entities</b>
            </li>
            <li>
                We can run the above command as many times we need every time the scaffolding will replace entire classes if you get some error we can run the same comand with <b>--force</b> in the last
            </li>
        </ul>
        <p>
        <h2>Tracking</h2>
            We can select rows from DbContext as tracking so that when we pull data with notracking.
            <pre>
                <i>
                var person = _context.Person.Where(x =&gt; x.BusinessEntityId ==5);
                person.LastName ="Modified";
                var changeRow = _context.Changetracker.Entries().First();
                </i>
            </pre>
            This will return changed row.
        </p>
        <p>
            We can select rows from DbContext as no tracking so that when we pull large amount of data with notracking we can have good performance.
            <pre>
                <i>
                    var person = _context.Person.Where(x =&gt; x.BusinessEntityId ==5).AsNoTracking();
                    var changeRow = _context.Changetracker.Entries().First();
                </i>
            </pre>
            Here it will not return anything becase we inist No tracking
        </p>
        <p>
            <h2>Querying Data</h2>
            When we writing a query it will not execute all situations
            <pre>
                //Nothing Happens
                IQueryable&lt;Person&gt; query = _context.Person.AsQueryable();
                //Now Query Executes
                var List&lt;Person&gt; = query.ToList();
                //Also here
                foreach(var person in query)
                {}
                //Also here
                var person = query.FirstOrDefault();
                //Also here
                var person = query.SingleOrDefault(x => x.BusinessEntity == 1);
                //And Here
                var person = _query.Find(1);
            </pre>
            Same way if you only <b>Where</b> condition without using <b>ToList</b> or <b>FirstOrDefault and ...</b> query will not execute so you can use multiple queries as below
            <pre>
                //All in one statement
                var query1 = _context.Person.Where(x => x.PersonType == "em" && x.EmailPromotion == 1);
                //Chained statement
                var query2 = _context.Person.Where(x => x.PersonType == "em").Where( x => x.EmailPromotion == 1);
                //Build up over disparate calls
                var query3 - _context.Person.Where(x => x.PersonType == "em");
                query3 = query3.Where(x => x.EmailPromotion ==1);
                //Or's can't be chained ********  so we need to put "OR" like below
                var query4 = _context.Person.Where (x => x.PersonType || x.EmailPromotion ==1)
                //Added to this we can use funtions in the EF query; where list is List&lt;int&gt;
                var query1 = _context.Person.Where(x => list.Contains(x.BusinessEntityId));
                var query1 = _context.Person.Where(x => x.LastName.Contains("UF));
                var query1 = _context.Person.Where(x => EF.Functions.Like(x.LastName, "UF%"));
                var query1 = _context.Person.Where(x => EF.Functions.IsDate(x.DOB.ToString()));
                //And Sum/Count/Average/Max/Min/Any/All
                <br>
                _context.Person.Find(1,234); //takes multiple keys
                <br>
                //FirstOrDefault will take first record of matching criteria
                //SingleOrDefault will return single record if you have more record for the criteria will throw exception
                //Skip(12).Take  //Always use OrderBy before Skip and Take
            </pre>
        </p>
        <p>
            <h2>Get Related Data</h2>
            We can use Include, ThenInclude for getting related records
            <pre>
                //Implicily loading
                //These queries are not yet run, we neet to include ToList or FirstOrDefault ....
                //In Implicit loading all the related tables will be loaded
                var person = _context.Person.Include(x =&gt; x.EmailAddress).Where(w =&gt; w.Id==1);
                //Here Employee within Person and SalesPerson in side Employee
                var person = _context.Person.Include(x =&gt; x.Employee).TheInclude(x =&gt; x.SalesPerson).Where(w =&gt; w.Id==1);
                //This will execute the query
                person.ToList();
                //Explicitly Loading
                //In Explicit loading we load the related table when really need it
                var person = _context.Person.FirstOrDefault(x.BusinesEntityId == 1);
                _context.Entry(p).Reference( p =&gt; p.Employee).Load();  // 1 => 1
                _context.Entry(p).Collection(c =&gt; c.EmployeeAddress).Load(); // 1 => Many
            </pre>
            <br>
                When we write projections, we can select data as Anonyamouse like
                <pre>
                    var Anno = _contect.Person.Select( x => {
                        x.Firstname,
                        x.LastName
                    }).ToList()
                </pre>
                OR we can use strongly type POC object, Like
                <pre>
                    var emp = _context.Person.Select(  x => new PersonPoc{
                        FirstName = x.FirstName,
                        LastName = x.LastName
                        }).ToList();
                </pre>
                The main difference is Anonymouse object cannot pass outside the method as return type but Strongly type object can be pass as return type.
                <br>
                Difference between <b>Select</b> and <b>SelectMany </b> is Select return IQueryable&lt;Collection&lt;Person&gt;&gt; but SelectMany return IQuerabl&lt;Person&gt;. SelectMany is easy to use in our query.
                <pre>
                    IQueryable&lt;ICollection&lt;Person&gt;&gt; persons = _context.Select(x =&gt; x.Id ==1);
                    IQuerable&lt;Person&gt; persons = _context.Person.SelectMany(x =&gt; x.Id ==1);
                </pre>
        </p>
        <p>
            <h2>Persists Data into Database</h2>
            DbContext itself maintaning a transaction. We can run all the update, add delete and then lastly we can call SaveChanges() method of DbContext. But for Integration test or some other purpose we can create a transaction and then run method under transaction like bellow,
            <pre>
                Public void ShouldExecuteInTransaction(Action actionToExecute)
                {
                    using(var transaction = _context.Database.BeginTraction())
                    {
                        actionToexecute();
                        transaction.Commit();  //in the integration test we can make transaction.Rollback() 
                                                //so that data will be rollback once method execute.
                    }
                }
                public void AddItem()
                {
                    ShouldExecuteInTransaction(AddNewPerson);
                    void AddNewPerson()
                    {
                        var person = new Person{
                            AdditionalContactInfo ="Home",
                            FirstName="Barney",
                            LastName = "Rubble",
                            Title= "Neighbor"
                        }
                        _context.Person.Add(person);
                        _context.SaveChanges();
                    }
                }
            </pre>
            There are many whys to Edit ot Delete a entity
            <pre>
                internal EntityEntry DeleteEntry()
                {
                    var person = _context.Person.Find(1);
                    //This is remove in momory data
                    _context.Person.Remove(person);
                    _context.SaveChanges();
                    //OR
                    //This remove database data
                    //But in web world we dont need to remove in momory because each requets dbcontext loads from db.
                    _context.Entry(person).State = EntityState.Deleted; // same way if the person is edit object 
                                                                        //then we can say EntityState.Edited
                    _context.SaveChanges();
                    return _context.ChangeTracker.Entries.First();
                }
                internal EntityEntry DeleteEntry()
                {
                    var person = _context.Person.Find(1);
                    person.LastName = "UpdatedName";
                    _context.Person.Update(person);
                    _context.SaveChanges();
                    return _context.ChangeTracker.Entries.First();
                }
            </pre>
        </p>
    </p>
	<p>
		Normally when we write ASP.Net application, Load test is good for testing the the application scalability. The tool called
		<b>WebSurge</b> is good for testing the load test. We can download it from <b>https://websurge.west-wind.com/</b>. <br>
		Once we install it, open it and add our enpoint url and specify how many thread you want to use , ie, How many simultenious request has to be made and how many second we need to prolong th erequest. As a result we get how many request has been made for a second. We can compare this result with Synchronus calls.(If we written our request as async). 
	</p>
	<p>
		We have Pipe lines and Middleware. Every request goes thru set of middlewares while it passes we can perform some check or operation and decide wether we can pass it to next middleware or break the chinge and return the response.
		On of the middle ware that run within MVC context it <b>MVC Action Filters pipeline</b> There are many action filter and order<br>
        <ol>
            <li>Authorization Filters</li>
            <li>Resource Filter</li>
            <li>&lt;Model Binding&gt;</li>
            <li>Action Filter</li>
            <li>&lt;Action Execute&gt;</li>
            <li>Exception Filters</li>
            <li>Result Filter</li>
            <li>(Result filter execute before and after each indivitual <b>Action method</b>)</li>
            <li>Once Result filter execute the backward direction start and ignore all the filter and comes to Resource filter so If you want to Map anything to DTO object you can handle it in the resutlt filter.</li>
        </ol> <br>
		We can customize <b>Result filter</b> inheriting <b>IResultFiler / IAsyncResultFilter</b> anf <b> ResultFiterAttribute</b>.
        <p>
            <pre>
                public class UsersFilterAttribute : ResultFilterAttribute
                {
                    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
                    {
                        var result = context.Result as ObjectResult;
                        if (result?.Value == null || result.StatusCode < 200 || result.StatusCode >= 300)
                        {
                            await next();
                            return;
                        }
                        //This code identifies the result if Ienumerable or List// We can create a separate filter for List type result
                        if (typeof(IEnumerable).IsAssignableFrom(result.Value.GetType()))
                        {
                            //Enumerable result
                        }
                        result.Value = new List&lt;User&gt; {
                            new User{ Id=1, FName="Newman", LName="Croos"},
                            new User{ Id=2, FName="Nithin.V", LName="Croos"}
                        };
                        await next();
                        //return base.OnResultExecutionAsync(context, next);
                    }
                }
            </pre>
            <p>
                <h2>TransactionScope in Entity Framework Core</h2>
                <p>
                    Using TransactionScope is easy, warapping the database call with TransactionScope will provide easy way of maintening transaction
                    <pre>
                        using(var scope = new TransactionScoope())
                        {
                            var groups -myDbContext.ProductGroup.ToList();
                            scope.Complelete();
                        }
                    </pre>
                    but when we use <b>TransactionScope</b> in Async call bit problem. if you use
                    <pre>
                        using(var scope = new TransactionScoope())
                        {
                            var groups -myDbContext.ProductGroup.ToListAsync().ConfigureAwait(false);
                            scope.Complelete();
                        }
                    </pre>
                    will get <b>System.InvalidOperationException : TransactionScope must be dispose on the same thread that it was created</b> why??????????????/<br>
                    Because <b>TransactionScope doesn't  flow from one thread to another by dfault</b> to fix this we need to add
                    <i>TransactionScopeAsyncFlowOption.Enabled</i> option in the TransactionSctionScope like below,
                    <pre>
                        using(var scope = new TransactionScoope(TransactionScopeAsyncFlowOption.Enabled))
                        {
                            var groups -myDbContext.ProductGroup.ToListAsync().ConfigureAwait(false);
                            scope.Complelete();
                        }
                    </pre>
                    <br>
                    If you a <b>BginTransaction</b> within TransactionScope block like below
                    <pre>
                        using(var scope = new TransactionScope())
                        {
                            Do():
                        }
                        public void Do()
                        {
                            using(var tx = Context.Database.BeginTransaction())
                            {
                                var groups = Context.ProductGroups.ToList();
                            }
                        }
                    </pre>
                    we will get  <i>Syste.InvalidOpterationException : An Ambien transaction has been detected. the ambien transaction needs to be completed be for beginning transaction on this connection</i> because <b>Do()</b> is 3rd party lib or a framework then method has be moved out of outer Transactionscope.
                    <p>
                        <h2>Using Multiple instances of DbContext</h2>
                        Let say we have two different DbContext for two different dataases and using TransactionScope,
                        <pre>
                            using(var scope = new TransactionScope())
                            {
                                var groups = dbContext1.ProductGroups.ToList();
                                var others = dbContext2.SomeEntity.ToList();
                            }
                        </pre>
                        This use case is not supported in Asp.Net Core because here we need <b>Distributed Transaction Coordinator (MSDTC)</b> but .Net Core is multiplatform and other OS doesn't support DTS, .Net team dropped the support to <b>TransactionScope</b> for multi server databases.
                    </p>
                </p>
                <p>
                    <h2>Calling external Api from api</h2>
                    Normally we can use <b>HttpClient</b> to call external service from our service but from .Net core 2.1 MS introduced
                    <b>HttpClientFactory</b>, this is some kind of wrapper for Httpclient and we can register it in the startup.css so that we can easily inject it, so in Statrtup.cs
                    <pre>
                        services.AddHttpClient();
                    </pre>
                    and in some other classes ex
                    <pre>
                        public class Callingclass
                        {
                            private readonly IHttpClientFactory _clientFactory;
                            public Claiingclass(IHttpClientFactory clientFactory)
                            {
                                _clientFactory = clientFactory;
                            }
                            public async Task<Book> GetBooks(string coverId)
                            {
                                var httpClient = _clientFactory.CreateClient();
                                var response = await httpClient.GetAsync($"http://localhost/api/getbooks/{coverId}");
                                if(response.IsSuccessStatusCode)
                                {
                                    return JsonConvert.DeserializeObject&lt;Book&gt;(await response.Content.ReadStringAsync());
                                }
                                return null;
                            }
                        }
                    </pre>
                    <b>
                        Note: Normally we can return multiple values using <b>Tuple</b> in c#
                        <pre>
                            var propertyBag = Tuple&lt;Book,IEnumerable&lt;BookCover&gt;&gt;(book, bookCover);
                            var _book = propertyBag.Item1;
                            var _bookCover = propertyBag.Item2;
                        </pre>
                        <br>
                        It is little dificut to use Item1, Item2 format. Fortunatly from C#7 MS introduce Named Tuple parameter
                        <pre>
                            (Book book, &lt;IEnumerable&lt;BookCover&gt;&gt; bookCover> propertyBag =(book, bookCover);
                            var book = propertyBag.book;
                            var bookCover = propertBag.bookCover;
                        </pre>
                        we also can pass Tuple as response to a web api
                        <pre>
                            return Ok((book:book, bookCover:bookCover));
                        </pre>
                        Now we can captuer these two in <b>ResultFilter</b> and form a single result and then pass as response. in ResultFilter
                        <pre>
                            var result = context.Result as ObjectResult;
                            var (book, bookCover) = ((Book, IEnumberable&lt;BookCover&gt;)) result.Value;
                            //Merge twi result as one and pass as response to the web api.
                        </pre>
                    </b>
                </p>
                <p>
                    <h3>Asyn Concepts</h3><br>
                    There are two types of work can happend in async world
                    <ol>
                        <li>
                            I/O Bound: <br>
                            If you ask your self a question
                            <i>Will my code be waiting for a task to be complete before continuing</i> if yes then, it should be <b>I/O bound</b>. <br>
                            Example : File syatem, database, network call etc.
                        </li>
                        <li>
                            Computational Bound: <br>
                            If you ask your self a question <i>Will my code be performing an expensive computation?</i> if yes then it should be <b>Computational bound</b> <br>
                            Example : Running a big business logic.
                        </li>
                    </ol>
                    Using async for Computational bound is not good idea.
                    <p>
                        <b>Note: <br>
                            * When we select a record from a table we are doing I/O bound operation so we need to call it using Async but
                            consider <i>dbContext.Add(book)</i>, here we are not making any database call instead we just add the book object to inmemory context so using Asyn is not good idea. When you create Repository don't write Add method as Async but create a separate method for <b>SaveChanges</b> ad make it as async.
                            <br>
                            * Using <b>Task.Run()</b> run a synchronus method as Asynchronus method.<br>
                            <pre>
                                var a = Task.Run(() => {
                                    return _synCall.SomthMethod(); // this is some sync method retun some type 
                                                                    //that will be return by Task.Run
                                });
                            </pre>
                            But Using Task.Run() is not recommendaed because 
                                    - Asp.Net Core is not optimized for Task.Run()
                                    - It is intened to use in Client like Zamarin or some UI not on the Server.
                        </b>
                        In .Net we had SynchronizationContext to help managing Tread context but in asp.net core when we completely using async/wait so no need for Synchronizatio context, and we used ConfigureWait(false) to avoid dead lock but in asp.net core isn't neccessary any more due to not being a synchronizationContext.
                    </p>
                </p>
                <p>
                    <h3>CancellationToken</h3>
                    When we pass Cancellation token to mulple call and if one of then failt we can set <b>cancellationToke.Cancel</b> this will notify all other async calls and ends all the calls and release the thread to the pool.
                    Normally we can set cancellationToken.Canel in the Catch portion of Try .. Catch block
                </p>
                <p>
                    <h3>Exception in Async Calls</h3>
                    When we cancel the task using cancellationToken the the main function that initiate the async call will get the exception ( OperationCancelledException)
                    <p>
                        var downloadBookCoverTasks = &lt;set of tasks added to execute&gt;
                        //All task under this downloadBookCoverTasks has CancellationToken passed
                        //If one of the method fails that method cancel the token so we'll get the exception
                        private CancelationTokenSource cancellationTkenSource;
                        CancelationTokenSource = new CancelationTokenSource();
                        //Need to pass <b>CancelationTokenSource.Token</b> to each method that expect cancellation Token.
                        try{
                            return await Task.WhenAll(downloadBookCoverTasks);
                        }
                        catch(OperationCenceledException exception)
                        {
                            _logger.LogInformation($"{exception.Message}");
                            foreach(var task in downloadBookCoverTasks)
                            {
                                _logger.LogInformation($"Task {task.Id} has status {task.Status});
                            }
                            return List&lt;BookCover&gt;();
                        }
                        but here we can get the particular exception wich was thrown by a method because we use Async. for getting that particular exception we have to use AggagateException
                    </p>
                </p>
                <p>
                    Note :
                        <ul>
                            <li>We can run a long running synchronus job as Async using Task.Run(() =&gt;)</li>
                            <li>We can call Async Mthod with Sync method using &lt;AsyncMethod()&gt;.Result, here we immediatly get the result as sync from Async method. this will block the Thread untile it complete it work and return result in Sync manner.<br> We canalso use &lt;AsyncMethod()&gt;.wait() to achive the same.</li>
                        </ul>
                </p>
                <p>
                    <h3>Range operatior in c#</h3><br>
                    <pre>
                    void Main()
                            {
                                var stuff =new int[]{1,2,3,4,5,6,7};
                                Console.WriteLine(String.Join(",", stuff));
                                Console.WriteLine(String.Join(",", stuff[0..2])); // Starting 1 and length 2
                                Console.WriteLine(String.Join(",", stuff[2..])); // Starting 3 end of array
                                Console.WriteLine(String.Join(",", stuff[2..^1])); //Start at 3 end last but one
                                //Creating sub array
                                var b =new ArraySegment&lt;int&gt;(stuff, 0,2); // return sub array  start 0 and length 2
                                Console.WriteLine(b[0]); // Return 1
                            }
                    </pre>
                </p>
            </p>
        </p>
    </p>

</p>
