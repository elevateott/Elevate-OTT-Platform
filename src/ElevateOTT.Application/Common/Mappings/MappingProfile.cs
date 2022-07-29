using ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Mappings;

public class MappingProfile : Profile 
{
    #region Public Constructors

    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        // CreateMap<VideoModel, VideoDto>().ReverseMap();
        // CreateMap<LiveStreamModel, LiveStreamDto>().ReverseMap();
        // CreateMap<CategoryModel, CategoryDto>().ReverseMap();
        // CreateMap<CollectionModel, CollectionDto>().ReverseMap();
        // CreateMap<SubscriptionModel, SubscriptionDto>().ReverseMap();
        CreateMap<AuthorModel, AuthorItem>().ReverseMap();
        CreateMap<AuthorModel, AuthorForEdit>().ReverseMap();
        CreateMap<AuthorModel, CreateAuthorCommand>().ReverseMap();
        CreateMap<AuthorModel, UpdateAuthorCommand>().ReverseMap();
    }

    #endregion Public Constructors

    #region Private Methods

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }

    #endregion Private Methods
}
