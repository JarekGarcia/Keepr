namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultsService _vaultsService;

    public VaultsController(Auth0Provider auth0Provider, VaultsService vaultsService)
    {
        _auth0Provider = auth0Provider;
        _vaultsService = vaultsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetVaultByIdAndValidate(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> EditVault(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.EditVault(vaultId, userInfo?.Id, vaultData);
            return Ok(vault);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<string>> DeleteVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string Message = _vaultsService.DeleteVault(vaultId, userInfo?.Id);
            return Ok(Message);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<Keep> keeps = _vaultsService.GetKeepsByVaultId(vaultId, userInfo?.Id);
            return keeps;
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }



}