using Api.Controllers.File.Dto.Request;
using AutoMapper;
using Dal.File;
using MainLib.Extensions;

namespace Api.MappingProfile;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<UploadFileRequest, FileDal>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Name, o => o.MapFrom(s => Path.GetFileNameWithoutExtension(s.File.FileName)))
            .ForMember(d => d.Extension, o => o.MapFrom(s => Path.GetExtension(s.File.FileName)))
            .ForMember(d => d.Content, o => o.MapFrom(s => s.File.GetFileContent()))
            .ForMember(d => d. StorageType, o => o.Ignore());
    }
}