# Consuming a GraphQL Endpoint

Install the Strawberry Shake Tools

`
dotnet new tool-manifest
dotnet tool install StrawberryShake.Tools --local
`

Add the StrawberryShake Dependencies to the project:
`
dotnet add package StrawberryShake.Transport.Http
dotnet add package StrawberryShake.CodeGeneration.CSharp.Analyzers
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Http
`

Generate the respective client for your Console:

`dotnet graphql init https://bajun.explorer.subsquid.io/graphql/ -n BajunClient`

Go to the newly created `.graphqlrc.json` file and add your project's respective namespace. For example: `"namespace": "Ajuna.SubsquidApi.Console"`

Next step includes creating your first query in a `.graphql` file. 

After building the solution, you will see the newly created "Generated" folder. 

You are now ready to start sending Queries to the GraphQL Endpoint

