


namespace Keepr.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _repository;

    public ProfilesService(ProfilesRepository repository)
    {
        _repository = repository;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        List<Keep> keeps = _repository.GetUsersKeeps(profileId);
        return keeps;
    }

    internal Profile GetUsersProfile(string profileId)
    {
        Profile profile = _repository.GetUsersProfile(profileId);
        if (profile == null)
        {
            throw new Exception($"Invalid profile Id:{profileId}");
        }
        return profile;
    }

    internal List<Vault> GetUsersVaults(string profileId)
    {
        List<Vault> vaults = _repository.GetUsersVaults(profileId);
        return vaults;
    }
}