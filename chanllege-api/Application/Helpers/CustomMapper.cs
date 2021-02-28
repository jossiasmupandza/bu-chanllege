using Application.Dtos;
using Application.Interfaces;
using AutoMapper;

namespace Application.Helpers
{
    public class CustomMapper : ICustomMapper
    {
        private readonly Mapper _mapper;

        public CustomMapper()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Quiz, QuizDto>();
            });
            
            _mapper = new Mapper(mapConfig);
        }
        
        public Mapper GetMapper()
        {
            return _mapper;
        }
    }
}