using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder();

var app = builder.Build();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($$"""
    <html>
        <head>
          <script type="module" src="https://cdn.jsdelivr.net/gh/starfederation/datastar@v1.0.2/bundles/datastar.js"></script>
          <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body class="container">
            <h1>data-bind</h1>
            <p>
                Please enter information to the form
            </p>
            <div class="grid">
                <div>
                    <form>
                    <fieldset>
                        <label>
                        First name
                        <input
                            data-bind:first-name
                            name="first_name"
                            placeholder="First name"
                            autocomplete="given-name"
                        />
                        </label>
                        <label>
                        Email
                        <input
                            data-bind:email
                            type="email"
                            name="email"
                            placeholder="Email"
                            autocomplete="email"
                        />
                        </label>
                        <label>
                            <input data-bind:is-married type="checkbox"/>
                            Is Married
                        </label>
                        <label>
                            Nationality
                            <select data-bind:nationality aria-label="Select your favorite cuisine..." required>
                                <option selected disabled value="">
                                    Select your nationality
                                </option>
                                <option>Italian</option>
                                <option>Indonesian</option>
                                <option>Indian</option>
                            </select>
                        <label>
                        <label>
                            Bio
                            <textarea
                                data-bind:bio
                                aria-label="Professional short bio"
                                ></textarea>
                        </label>

                        <fieldset>
                            <legend>Language preference:</legend>
                            <label>
                                <input type="radio" name="language" checked data-bind:language-preference value="en"/>
                                English
                            </label>
                            <label>
                                <input type="radio" name="language"  data-bind:language-preference value="fr"/>
                                French
                            </label>
                            <label>
                                <input type="radio" name="language"  data-bind:language-preference value="cn"/>
                                Mandarin
                            </label>
                            <label>
                                <input type="radio" name="language"  data-bind:language-preference value="th"/>
                                Thai
                            </label>
                        </fieldset>

                        <label>
                            Age
                            <input type="range" data-bind:age value="40" min="0" max="99" step="1" />
                        </label>
                    </fieldset>

                    <input
                        type="submit"
                        value="Subscribe"
                    />
                    </form>
                </div>
                <div>
                </div>
            </div>
            <br/>
            <h3>All signals on this page</h3>
            <pre data-json-signals></pre>
        </body>
    </html>
    """);
});
 
app.Run();
