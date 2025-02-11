using Api_Biblioteca.DTOs;
using Api_Biblioteca.Entidades;
using AutoMapper;

namespace Api_Biblioteca.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Configurar el campo de salida del dto con la informacion del modelo
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom
                (autor => MapearNombreYApellidoAutor(autor)));

            CreateMap<Autor, AutorConLibrosDTO>()
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom
                (autor => MapearNombreYApellidoAutor(autor)));

            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorPatchDTO>().ReverseMap();

            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(ent => ent.Autores, config => 
                config.MapFrom(dto => dto.AutoresIds.Select(id => new AutorLibro { AutorId = id })));

            //CreateMap<Libro, LibroConAutorDTO>()
            //    .ForMember(dto => dto.AutorNombre, config =>
            //        config.MapFrom(ent => MapearNombreYApellidoAutor(ent.Autor!)));

            CreateMap<ComentarioCreacionDTO,  Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<ComentarioPatchDTO, Comentario>().ReverseMap();

        }

        private string MapearNombreYApellidoAutor(Autor autor) => $"{autor.Nombres} {autor.Apellidos}";

    }
}
