//Small unnecessary change to trigger the workflow!

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'asp-copilotdemo-westeuropa'
  location: 'westeurope'
  sku: {
    name: 'F1'
    tier: 'Free'
    size: 'F1'
    family: 'F'
    capacity: 1
  }
}

resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: 'copilotdemo-web-hvs'
  location: 'westeurope'
  kind: 'app,linux' // Optional: explicitly define it's a Linux app
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNET:9.0' // Tell Azure to run .NET 9 on Linux
    }
  }
  tags: {
    environment: 'demo'
    project: 'copilot-demo'
    owner: 'HansVanSoom'
  }
}

