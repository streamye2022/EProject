using System;
using System.Collections.Generic;

namespace Microsoft.Streamye.Commons.AutoMappers
{
    public static class AutoMapperHelper
    {
        //完全的映射
        public static TDestination AutoMapTo<TDestination>(this object obj)
        {
            if (obj == null) return default(TDestination);
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap(obj.GetType(), typeof(TDestination)));
            return config.CreateMapper().Map<TDestination>(obj);
        }

        //可自定义映射规则
        public static TDestination AutoMapTo<TSource, TDestination>(this TSource obj,
            Action<AutoMapper.IMapperConfigurationExpression> cfgExp)
            where TSource : class
            where TDestination : class

        {
            if (obj == null) return default(TDestination);
            var config = new AutoMapper.MapperConfiguration(cfgExp != null
                ? cfgExp
                : cfg =>
                    cfg.CreateMap<TSource, TDestination>());

            return config.CreateMapper().Map<TDestination>(obj);
        }

        //不自定义映射规则
        public static TDestination AutoMapTo<TSource, TDestination>(this TSource obj)
            where TSource : class
            where TDestination : class

        {
            if (obj == null) return default(TDestination);
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap<TSource, TDestination>());

            return config.CreateMapper().Map<TDestination>(obj);
        }

        //集合类型转换
        public static IEnumerable<TDestination> AutoMapTo<TSource, TDestination>(this IEnumerable<TSource> obj)
            where TSource : class
            where TDestination : class

        {
            if (obj == null) return new List<TDestination>();
            var config = new AutoMapper.MapperConfiguration(cfg =>
                cfg.CreateMap(typeof(TSource), typeof(TDestination)));

            return config.CreateMapper().Map<IEnumerable<TDestination>>(obj);
        }
    }
}