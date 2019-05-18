# multi tenant configuration file

Orchard Core Framework (OCF) support multi-tenancy via url, either by host or prefix. This sample shows how OCF provides the infrastructure for each tenant to have its own configuration file.

The tenants are configured at `App_Data/tenants.json`. Additional tenant specific configuration information can be found at `App_Data/Sites/{TenantName}/appSettings.json`.

All these information is accessible via `OrchardCore.Environment.Shell.ShellSettings`.
