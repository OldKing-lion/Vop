﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vop.Api;
using Vop.Api.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入拓展类
    /// </summary>
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// 添加依赖注入接口
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // 注册类

            return services;
        }

        /// <summary>
        /// 添加接口代理
        /// </summary>
        /// <typeparam name="TDispatchProxy">代理类</typeparam>
        /// <typeparam name="IDispatchProxy">被代理接口依赖</typeparam>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddScopedDispatchProxyForInterface<TDispatchProxy, IDispatchProxy>(this IServiceCollection services) where TDispatchProxy : DispatchProxy
        {
            // 注册代理类

            return services;
        }


        static void AddType(IServiceCollection services, Type type)
        {
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);

            if (lifeTime == null)
            {
                return;
            }

            var serviceTypes = GetExposedServices(type);

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = ServiceDescriptor.Describe(serviceType, type, lifeTime.Value);

                if (dependencyAttribute?.ReplaceServices == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (dependencyAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
        }

        static void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = AssemblyHelper
                .GetAllTypes(assembly)
                .Where(
                    type => type != null &&
                            type.IsClass &&
                            !type.IsAbstract &&
                            !type.IsGenericType
                ).ToArray();

            AddTypes(services, types);
        }

        static void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (var type in types)
            {
                AddType(services, type);
            }
        }

        static bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        static DependencyAttribute GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        static List<Type> GetExposedServices(Type type)
        {
            ExposeServicesAttribute DefaultExposeServicesAttribute =
            new ExposeServicesAttribute
            {
                IncludeDefaults = true,
                IncludeSelf = true
            };

            var attrs = type.GetCustomAttributes<ExposeServicesAttribute>(true);
            if (attrs == null || attrs.Count() == 0) attrs = new List<ExposeServicesAttribute> { DefaultExposeServicesAttribute };
            return attrs
                .SelectMany(p => p.GetExposedServiceTypes(type))
                .ToList();
        }

        static ServiceLifetime? GetLifeTimeOrNull(Type type, DependencyAttribute dependencyAttribute)
        {
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy(type);
        }

        static ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        {
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }
    }
}