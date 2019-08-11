using System;
using AutoMapper;
using DragonTrainer.Backend.DTOs;
using DragonTrainer.Backend.DTOs.Missions;
using DragonTrainer.Backend.DTOs.Shop;

namespace DragonTrainer.Backend.Helpers
{
    public class MapperHelper
    {
        private IMapper _mapper;

        public static MapperHelper Build()
        {
            return new MapperHelper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GameInfo, UserInfo>();
                cfg.CreateMap<PurchaseResult, UserInfo>();
                cfg.CreateMap<MissionResult, UserInfo>();
            }));
        }

        public MapperHelper(MapperConfiguration mapperCfg)
        {
            _mapper = mapperCfg.CreateMapper();
        } 
        
        public D Map<S, D>(S obj)
        {
            return _mapper.Map<D>(obj);
        }
    }
}