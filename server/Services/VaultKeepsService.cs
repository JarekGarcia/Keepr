


namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        Vault vault = _vaultsService.GetVaultByIdAndValidate(vaultKeepData.VaultId, userId);
        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("Not your vault to add keeps into");
        }
        VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("Not your Vault Keep to Delete!");
        }
        _repository.DeleteVaultKeep(vaultKeepId);

        return "Vault Keep has been Delete!";
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"Invalid vault keep id: {vaultKeepId}");
        }
        return vaultKeep;
    }
}