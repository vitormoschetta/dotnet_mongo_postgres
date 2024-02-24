using dotnet_mongodb.Application.Shared;

namespace dotnet_mongodb.Application.Tag;

public class TagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public Output Execute(TagCreateInput input, string userEmail)
    {
        if (input.IsInvalid)
            return Output.Fail(EDomainCode.InvalidInput, input.Errors);

        var entity = input.ToEntity(userEmail);

        var recorded = _repository.GetByUserEmailAndTitle(userEmail, input.Title);

        if (recorded != null)
            return Output.Fail(EDomainCode.AlreadyExists, "Já existe uma tag com esse título");

        _repository.Create(entity);

        return Output.Ok(entity);
    }
}
