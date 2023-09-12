using WB.EntrevistaABP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WB.EntrevistaABP.Permissions;

public class EntrevistaABPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EntrevistaABPPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EntrevistaABPPermissions.MyPermission1, L("Permission:MyPermission1"));

        var pasajerosPermissions = myGroup.AddPermission(EntrevistaABPPermissions.Pasajeros.Default, L("Permission:Pasajeros"));
        pasajerosPermissions.AddChild(EntrevistaABPPermissions.Pasajeros.Create, L("Permission:Pasajeros.Create"));
        pasajerosPermissions.AddChild(EntrevistaABPPermissions.Pasajeros.Edit, L("Permission:Pasajeros.Edit"));
        pasajerosPermissions.AddChild(EntrevistaABPPermissions.Pasajeros.Delete, L("Permission:Pasajeros.Delete"));

        var viajesPermissions = myGroup.AddPermission(EntrevistaABPPermissions.Viajes.Default, L("Permission:Viajes"));
        viajesPermissions.AddChild(EntrevistaABPPermissions.Viajes.Create, L("Permission:Viajes.Create"));
        viajesPermissions.AddChild(EntrevistaABPPermissions.Viajes.Edit, L("Permission:Viajes.Edit"));
        viajesPermissions.AddChild(EntrevistaABPPermissions.Viajes.Delete, L("Permission:Viajes.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EntrevistaABPResource>(name);
    }
}
