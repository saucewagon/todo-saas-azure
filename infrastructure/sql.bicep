param location string

param sqlServerName string
param databaseName string
@secure()
param sqlAdminPassword string


resource sqlServer 'Microsoft.Sql/servers@2025-01-01' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: 'todoadmin'
    administratorLoginPassword: sqlAdminPassword
  }
}

resource database 'Microsoft.Sql/servers/databases@2025-01-01' = {
  parent: sqlServer
  name: databaseName
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
}