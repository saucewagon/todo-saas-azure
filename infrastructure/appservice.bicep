param location string

resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'todo-api-plan'
  location: location
  sku: {
    name: 'F1'
    tier: 'Free'
  }
}

resource appService 'Microsoft.Web/sites@2023-12-01' = {
  name: 'todo-api-dev-sauce'
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}