# Log into Grafana Loki 

This example shows how to log to [Grafana Loki](https://grafana.com/oss/loki/). The Docker is used for the software hosting.

 - open a command-line window in the folder and execute:
 
 ```sh
docker compose up
 ```
  - make sure that [Grafana Loki](https://grafana.com/oss/loki/) and [Grafana Dashboard](https://grafana.com/grafana/) containers are succesfully installed
  - Compile and run the application
 ```sh
 dotnet build
 dotnet run
  ```
  - Navigate to /test endpoint. The response should be 'OK'. The console window should contain **info**, **warn**, **fail** and **crit** log messages.
  - Navigate in a browser to the Grafana Dashboard website: <http://localhost:3000/>
  - Add a loki data source <http://loki:3100>
  - Go to the 'Explore data' page and add Label filter:  ``job="LokiWebApplication"``. Press the 'Run query' button
  - Log messages should be shown