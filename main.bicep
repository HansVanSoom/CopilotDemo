// Small unnecessary change to trigger the workflow

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'asp-copilotdemo-westeuropa'
  location: 'westeurope'
  sku: {
    name: 'B1'
    tier: 'Basic'
    size: 'B1'
    family: 'B'
    capacity: 1
  }
}

resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: 'copilotdemo-web-hvs'
  location: 'westeurope'
  kind: 'app,linux'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
  }
  siteConfig: {
    linuxFxVersion: 'DOTNET|8.0'
  }
  tags: {
    environment: 'demo'
    project: 'copilot-demo'
    owner: 'HansVanSoom'
  }
}

