var app = WebApplication.Create();
app.MapGet("/", () => Results.Content("""
        <!DOCTYPE html>
        <html>
          <head>
            <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
          </head>
          <body class="container">
            <h1>hx-live reactive DOM scripting</h1>
            <div data-count="0">
              <button hx-on:click="data.count++">Add</button>
              <button hx-on:click="data.count--">Deduct</button>
              <br/><br/>
              <output :text="data.count">0</output>
              <progress max="10" value="0" hx-live="this.value = Math.max(0, Math.min(data.count, 10))"></progress>
              <small :text="`${Math.max(0, Math.min(data.count, 10))} / 10`">0 / 10</small>
              <br/><br/>
              <input placeholder="Debounced text" hx-on:input="await debounce(300); q('#echo').textContent = this.value">
              <br/><br/>
              <p id="echo"></p>
            </div>
        
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/htmx.min.js"></script>
            <script src="https://cdn.jsdelivr.net/npm/htmx.org@4.0.0/dist/ext/hx-live.min.js"></script>
          </body>
        </html>
    """, "text/html"));

app.Run();
