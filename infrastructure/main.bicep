targetScope = 'subscription'

var location = 'westus'
var resourceGroupName = 'todo-saas-dev'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: resourceGroupName
  location: location
}

module appService './appservice.bicep' = {
  name: 'todo-api-appservice'
  scope: resourceGroup
  params: {
    location: location
  }
}