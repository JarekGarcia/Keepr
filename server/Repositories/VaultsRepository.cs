





namespace Keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO
        vaults(name, description, img, isPrivate, creatorId)
        VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);

        SELECT 
        vaults.*,
        acc.* 
        FROM vaults
        JOIN accounts acc ON acc.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
        return vault;
    }

    internal void DeleteVault(int vaultId)
    {
        string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
        _db.Execute(sql, new { vaultId });
    }

    internal Vault EditVault(Vault ogVault)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        isPrivate = @IsPrivate
        WHERE id = @Id LIMIT 1;

        SELECT 
        vaults.*,
        acc.* 
        FROM vaults
        JOIN accounts acc ON acc.id = vaults.creatorId
        WHERE vaults.id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, ogVault).FirstOrDefault();
        return vault;
    }

    internal List<Keep> GetKeepsByVaultId(int vaultId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        acc.*,
        vk.*
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        JOIN vaultKeeps vk ON vk.vaultId = @vaultId
        WHERE keeps.id = vk.keepId
        ;";

        List<Keep> keeps = _db.Query<Keep, Profile, VaultKeep, Keep>(sql, (keeps, profile, vaultkeeps) =>
        {
            keeps.Id = vaultkeeps.KeepId;
            keeps.Creator = profile;
            keeps.VaultKeepId = vaultkeeps.Id;
            return keeps;
        }, new { vaultId }).ToList();
        return keeps;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        acc.* 
        FROM vaults
        JOIN accounts acc ON acc.id = vaults.creatorId
        WHERE vaults.id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { vaultId }).FirstOrDefault();
        return vault;
    }

    private Vault VaultBuilder(Vault vault, Profile profile)
    {
        vault.Creator = profile;
        return vault;
    }
    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        acc.* 
        FROM vaults 
        JOIN accounts acc ON acc.id = vaults.creatorId
        WHERE vaults.creatorId = @userId;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { userId }).ToList();
        return vaults;
    }
}