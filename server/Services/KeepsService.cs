



namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repository;

    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }

    internal string DeleteKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("Not your keep to delete!");
        }
        _repository.DeleteKeep(keepId);
        return $"{keep.Name} has been deleted!";
    }

    internal Keep EditKeep(int keepId, string userId, Keep keepData)
    {
        Keep ogKeep = GetKeepById(keepId);
        if (ogKeep.CreatorId != userId)
        {
            throw new Exception($"Not your keep to edit {ogKeep.Name}");
        }

        ogKeep.Name = keepData.Name ?? ogKeep.Name;
        ogKeep.Img = keepData.Img ?? ogKeep.Img;
        ogKeep.Description = keepData.Description ?? ogKeep.Description;

        Keep keep = _repository.EditKeep(ogKeep);
        return keep;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception($"invalid Id:{keepId}");
        }
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = _repository.GetKeeps();
        return keeps;
    }
}