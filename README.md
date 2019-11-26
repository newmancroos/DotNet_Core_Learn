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
               </p>
        </p>
    </p>
</p>
