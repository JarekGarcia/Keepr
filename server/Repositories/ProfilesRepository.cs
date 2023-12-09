


namespace Keepr.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        COUNT(vk.id) AS kept,
        acc.* 
        FROM keeps 
        JOIN accounts acc ON acc.id = keeps.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        WHERE keeps.creatorId = @profileId
        GROUP BY (keeps.id)
        ;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    internal Profile GetUsersProfile(string profileId)
    {
        string sql = @"
        SELECT * FROM accounts acc WHERE acc.id = @profileId;";

        Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
        return profile;
    }

    internal List<Vault> GetUsersVaults(string profileId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        acc.* 
        FROM vaults 
        JOIN accounts acc ON acc.id = vaults.creatorId
        WHERE vaults.creatorId = @profileId;";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { profileId }).ToList();
        return vaults;
    }
}