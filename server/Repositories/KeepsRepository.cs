namespace Keepr.Services;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO 
        keeps (name, description, img, creatorId)
        VALUES (@Name, @Description, @Img, @creatorId);

        SELECT 
        keeps.*,
        COUNT(vk.id) AS kept,
        acc.*
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        WHERE keeps.id = LAST_INSERT_ID()
        GROUP BY (keeps.id)
        ;";


        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;
    }

    internal void DeleteKeep(int keepId)
    {
        string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
        _db.Execute(sql, new { keepId });
    }

    internal Keep EditKeep(Keep ogKeep)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        img = @Img,
        description = @Description,
        views = @Views
        WHERE id = @Id LIMIT 1;

        SELECT 
        keeps.*,
        COUNT(vk.id) AS kept,
        acc.* 
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        WHERE keeps.id = @Id
        GROUP BY (keeps.id)
        ;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, ogKeep).FirstOrDefault();
        return keep;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        COUNT(vk.id) AS kept,
        acc.* 
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        WHERE keeps.id = @keepId
        GROUP BY (keeps.id);";


        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT 
        keeps.*,
        COUNT(vk.id) AS kept,
        acc.*
        FROM keeps
        JOIN accounts acc ON acc.id = keeps.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        GROUP BY (keeps.id);";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder).ToList();
        return keeps;
    }

    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }

}