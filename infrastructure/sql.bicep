param location string

param sqlServerName string
param databaseName string
param entraAdminObjectId string
param entraAdminLogin string

@secure()
param sqlAdminPassword string




resource sqlServer 'Microsoft.Sql/servers@2025-01-01' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: 'todoadmin'
    administratorLoginPassword: sqlAdminPassword
    administrators: {
        administratorType: 'ActiveDirectory'
        login: entraAdminLogin
        principalType: 'User'
        sid: entraAdminObjectId
        tenantId: subscription().tenantId
    }
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
