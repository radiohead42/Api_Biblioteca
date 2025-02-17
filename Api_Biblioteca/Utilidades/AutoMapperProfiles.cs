﻿using Api_Biblioteca.DTOs;
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

            CreateMap<AutorLibro, LibroDTO>()
                .ForMember(dto => dto.Id, config => config.MapFrom(ent => ent.LibroId))
                .ForMember(dto => dto.Titulo, config => config.MapFrom(ent => ent.Libro!.Titulo));

            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(ent => ent.Autores, config => 
                config.MapFrom(dto => dto.AutoresIds.Select(id => new AutorLibro { AutorId = id })));

            CreateMap<Libro, LibroConAutoresDTO>();

            CreateMap<AutorLibro, AutorDTO>()
                .ForMember(dto => dto.Id, config => config.MapFrom(ent => ent.LibroId))
                .ForMember(dto => dto.NombreCompleto, config => 
                            config.MapFrom(ent => MapearNombreYApellidoAutor(ent.Autor!)));

            CreateMap<LibroCreacionDTO, AutorLibro>()
                .ForMember(ent => ent.Libro, config => config.MapFrom(dto => new Libro { Titulo = dto.Titulo }));

            //CreateMap<Libro, LibroConAutorDTO>()
            //    .ForMember(dto => dto.AutorNombre, config =>
            //        config.MapFrom(ent => MapearNombreYApellidoAutor(ent.Autor!)));

            CreateMap<ComentarioCreacionDTO,  Comentario>();
            CreateMap<Comentario, ComentarioDTO>()
                .ForMember(dto => dto.UsuarioEmail, config => config.MapFrom(ent => ent.Usuario!.Email));
            CreateMap<ComentarioPatchDTO, Comentario>().ReverseMap();

            CreateMap<Usuario, UsuarioDTO>();

        }

        private string MapearNombreYApellidoAutor(Autor autor) => $"{autor.Nombres} {autor.Apellidos}";

    }
}
