This demo adds structured compact JSON logs and correlation IDs for a Grafana + Loki drill-down demo.

Steps to finish setup:

1. Install Serilog packages
   - For each project (AssetManagementWEBAPI and AssetManagementBlazor) run:
     dotnet add package Serilog.AspNetCore
     dotnet add package Serilog.Sinks.Console
     dotnet add package Serilog.Formatting.Compact
     dotnet add package Serilog.Enrichers.Environment

2. (Optional) Install Grafana Loki sink
   - dotnet add package Serilog.Sinks.Grafana.Loki
   - After installing, uncomment the .WriteTo.GrafanaLoki(...) lines in Program.cs files.

3. Run a local Grafana + Loki stack (Loki HTTP on port 3100). Make sure CORS/network allows app to send logs to Loki.

4. Start both projects. The Blazor server will include `X-Correlation-ID` header on outbound HTTP calls; the API will echo the header in the response and include it in logs.

5. Demo flow: open app UI, trigger actions that call API. Use Grafana Explore / Loki to query logs by label `source`.
   Example query to find client logs with a correlation id:
     {source="assetmanagement-client"} | json | CorrelationId = "<id>"

   Then find service logs with same CorrelationId:
     {source="assetmanagement-service"} | json | CorrelationId = "<id>"

Notes:
- The demo uses `RenderedCompactJsonFormatter` to emit compact JSON for easy parsing in Loki/Grafana.
- Correlation id header used: `X-Correlation-ID`.
- For production, consider a more robust correlation propagation (diagnostic source, activity id, distributed tracing) and secure log shipping.
