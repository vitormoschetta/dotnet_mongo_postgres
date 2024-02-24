namespace dotnet_mongodb.Application.Tag;

public interface ITagRepository
{
    void Create(TagEntity entity);
    IEnumerable<TagEntity> GetByUserEmail(string email);
    TagEntity? GetByUserEmailAndTitle(string email, string title);
}
