


namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO 
        vaultKeeps(vaultId, keepId, creatorId)
        VALUES(@VaultId, @KeepId, @CreatorId);

        SELECT 
        vk.*,
        acc.* 
        FROM vaultKeeps vk
        JOIN accounts acc ON acc.id = vk.creatorId
        WHERE vk.id = LAST_INSERT_ID();";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultkeep, profile) =>
        {
            vaultkeep.CreatorId = profile.Id;
            return vaultkeep;
        }, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultKeeps WHERE vaultKeeps.id = @vaultKeepId;";
        _db.Execute(sql, new { vaultKeepId });
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = @"
        SELECT 
        vk.*,
        acc.* 
        FROM vaultKeeps vk
        JOIN accounts acc ON acc.id = vk.creatorId
        WHERE vk.id = @vaultKeepId;";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultkeep, profile) =>
        {
            vaultkeep.CreatorId = profile.Id;
            return vaultkeep;
        }, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }
}

//     private VaultKeep VaultKeepBuilder(VaultKeep vaultKeep, Profile profile)
//     {
//         vaultKeep.Creator = profile;
//         return vaultKeep;
//     }
// }