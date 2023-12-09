namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;

    public ProfilesController(ProfilesService profilesService)
    {
        _profilesService = profilesService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetUsersProfile(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetUsersProfile(profileId);
            return profile;
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = _profilesService.GetUsersKeeps(profileId);
            return keeps;
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetUsersVaults(string profileId)
    {
        try
        {
            List<Vault> vaults = _profilesService.GetUsersVaults(profileId);
            return vaults;
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }
}