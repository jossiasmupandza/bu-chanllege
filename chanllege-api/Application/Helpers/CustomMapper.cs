using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;

namespace Application.Helpers
{
    public class CustomMapper : ICustomMapper
    {
        private readonly Mapper _mapper;

        public CustomMapper()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Quiz, QuizDto>();
                cfg.CreateMap<Question, QuestionDto>();
            });
            
            _mapper = new Mapper(mapConfig);
        }
        
        public Mapper GetMapper()
        {
            return _mapper;
        }
    }
}