var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.StockCube_WebAPI>("stockcube-webapi");

builder.AddProject<Projects.StockCube_BlazorServer>("stockcube-blazorserver");

builder.AddProject<Projects.StockCube_Infrastructure>("stockcube-infrastructure");

builder.AddProject<Projects.StockCube_Domain>("stockcube-domain");

builder.Build().Run();
