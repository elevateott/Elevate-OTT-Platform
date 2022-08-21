using AutoMapper.Extensions.EnumMapping;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using ElevateOTT.Application.Features.Content.Authors.Commands.CreateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Commands.UpdateAuthor;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthorForEdit;
using ElevateOTT.Application.Features.Content.Authors.Queries.GetAuthors;
using ElevateOTT.Application.Features.Content.Videos.Commands.CreateVideo;
using ElevateOTT.Application.Features.Content.Videos.Commands.UpdateVideo;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideoForEdit;
using ElevateOTT.Application.Features.Content.Videos.Queries.GetVideos;
using ElevateOTT.Domain.Common.DTOs;
using ElevateOTT.Domain.Entities.Content;

namespace ElevateOTT.Application.Common.Mappings;

public class MappingProfile : Profile 
{
    #region Public Constructors

    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        CreateMap<AuthorModel, AuthorItem>().ReverseMap();
        CreateMap<AuthorModel, AuthorForEdit>().ReverseMap();
        CreateMap<AuthorModel, CreateAuthorCommand>().ReverseMap();
        CreateMap<AuthorModel, UpdateAuthorCommand>().ReverseMap();
        CreateMap<AuthorModel, AuthorDto>().ReverseMap();

        CreateMap<VideoModel, VideoItem>().ReverseMap();
        CreateMap<VideoModel, VideoForEdit>().ReverseMap();
        CreateMap<VideoModel, CreateVideoCommand>().ReverseMap();
        CreateMap<VideoModel, UpdateVideoCommand>().ReverseMap();

        CreateMap<AssetImageModel, AssetImageDto>().ReverseMap();

        CreateMap<CategoryModel, CategoryDto>().ReverseMap();

        CreateMap<TagModel, TagDto>().ReverseMap();



        // TODO
        // ignore enum types
        // manually cast to equivalent int prop
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
