@secure()
param sqlAdminPassword string
param entraAdminObjectId string
param entraAdminLogin string

targetScope = 'subscription'

var location = 'westus'
var resourceGroupName = 'todo-saas-dev'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: resourceGroupName
  location: location
}

module monitoring './monitoring.bicep' = {
  name: 'todo-api-monitoring'
  scope: resourceGroup
  params: {
    location: location
    appInsightsName: 'todo-api-insights'
    logAnalyticsWorkspaceName: 'todo-api-logs'
  }
}

module appService './appservice.bicep' = {
  name: 'todo-api-appservice'
  scope: resourceGroup
  params: {
    location: location
    appInsightsConnectionString: monitoring.outputs.appInsightsConnectionString
  }
}

module sql './sql.bicep' = {
  name: 'todo-api-sql'
  scope: resourceGroup
  params: {
    location: location
    sqlServerName: 'todo-sql-dev-sauce-sqlserver'
    databaseName: 'TodoDb'
    sqlAdminPassword: sqlAdminPassword
    entraAdminObjectId: entraAdminObjectId
    entraAdminLogin: entraAdminLogin
  }
}
