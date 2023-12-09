


namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repository.CreateVault(vaultData);
        return vault;
    }

    internal string DeleteVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("Not your Vault to delete...");
        }
        _repository.DeleteVault(vaultId);
        return $"{vault.Name} has been Deleted!";
    }

    internal Vault EditVault(int vaultId, string userId, Vault vaultData)
    {
        Vault ogVault = GetVaultById(vaultId);
        if (ogVault.CreatorId != userId)
        {
            throw new Exception("Not your vault to edit");
        }

        ogVault.Name = vaultData.Name ?? ogVault.Name;
        ogVault.IsPrivate = vaultData.IsPrivate ?? ogVault.IsPrivate;

        Vault vault = _repository.EditVault(ogVault);
        return vault;
    }

    internal List<Keep> GetKeepsByVaultId(int vaultId)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"invalid vault Id:{vaultId}");
        }
        List<Keep> keeps = _repository.GetKeepsByVaultId(vaultId);
        return keeps;
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        List<Vault> vaults = _repository.GetMyVaults(userId);
        return vaults;
    }

    internal Vault GetVaultById(int vaultId)
    {
        Vault vault = _repository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"Invalid Id: {vaultId}");
        }
        return vault;
    }
}