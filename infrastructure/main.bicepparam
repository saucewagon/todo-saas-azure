using './main.bicep'

param sqlAdminPassword = readEnvironmentVariable('SQL_ADMIN_PASSWORD')
param entraAdminObjectId = readEnvironmentVariable('ENTRA_ADMIN_OBJECT_ID')
param entraAdminLogin = readEnvironmentVariable('ENTRA_ADMIN_LOGIN')
