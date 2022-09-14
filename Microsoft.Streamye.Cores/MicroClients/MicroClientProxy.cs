using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.Streamye.Cores.DynamicMiddleware;
using Microsoft.Streamye.Cores.Exceptions;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Newtonsoft.Json;

namespace Microsoft.Streamye.Cores.MicroClients
{
    public class MicroClientProxy : IInterceptor
    {
        private IDynamicMiddlewareService _dynamicMiddlewareService;

        //注入dynamic middle ware service
        public MicroClientProxy(IDynamicMiddlewareService dynamicMiddlewareService)
        {
            _dynamicMiddlewareService = dynamicMiddlewareService;
        }

        // 动态代理
        public void Intercept(IInvocation invocation)
        {
            //拿到接口的方法, 注意是被调用的这个接口的某个方法
            MethodInfo methodInfo = invocation.Method;

            //拿到特性
            var attributes = methodInfo.GetCustomAttributes();

            //遍历特性
            foreach (var attribute in attributes)
            {
                Type type = methodInfo.DeclaringType;

                //拿到microclient
                MicroClient microClient = type.GetCustomAttribute<MicroClient>();
                if (microClient == null)
                {
                    throw new FrameException($"MicroClient 不能为空");
                }

                //封装参数
                ProxyMethodParameter proxyMethodParameter =
                    new ProxyMethodParameter(methodInfo.GetParameters(), invocation.Arguments);
                dynamic dynamicParams = ArgumentsConvert(proxyMethodParameter);

                //判断特性类型，并调用
                if (attribute is GetPath getPath)
                {
                    //路径变量替换
                    string path = PathParse(getPath.Path, dynamicParams);
                    //调用远端接口
                    dynamic result = _dynamicMiddlewareService.GetAsync(microClient.UrlSchema, microClient.ServiceName
                        , path, dynamicParams);

                    //返回值转换成 methodinfo中的返回值
                    Type returnType = methodInfo.ReturnType;
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
            }
        }

        private dynamic ResultConvert(dynamic result, Type returnType)
        {
            //判空
            if (returnType == typeof(void))
            {
                return null;
            }

            //反序列化得到值
            string resultJson = JsonConvert.SerializeObject(result);
            dynamic returnResult = JsonConvert.DeserializeObject(resultJson, returnType);
            return returnResult;
        }


        private dynamic ArgumentsConvert(ProxyMethodParameter proxyMethodParameter)
        {
            //将得到的多个参数放入dicts，并将dict 作为 dynamic 返回
            //1. 如果参数只有一个， 就特殊返回 2.参数如果有多个，则加入dicts然后返回
            dynamic dynamicParams = new Dictionary<string, object>();

            IDictionary<string, object> paramPairs = new Dictionary<string, object>();
            foreach (var parameterInfo in proxyMethodParameter.ParameterInfos)
            {
                object parameterValue = proxyMethodParameter.Arguments[parameterInfo.Position];
                Type parameterType = parameterInfo.ParameterType;

                if (proxyMethodParameter.Arguments.Length == 1)
                {
                    //如果是值类型
                    if (parameterType.IsValueType)
                    {
                        //说明是在参数上拿到的
                        PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                        if (pathVariable != null)
                        {
                            //设置路径变量名称 ？？难道不应该从 GetPath中的path上拿数据吗
                            paramPairs.Add(pathVariable.Name, parameterValue);
                        }
                        else
                        {
                            paramPairs.Add(parameterInfo.Name, parameterValue);
                        }

                        // 1.1.3 设置为动态返回
                        dynamicParams = paramPairs;
                    }
                    else
                    {
                        //如果是引用类型
                        dynamicParams = parameterValue;
                    }
                }
                else
                {
                    //如果有多个值
                    PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                    if (pathVariable != null)
                    {
                        // 设置路径变量名称
                        paramPairs.Add(pathVariable.Name, parameterValue);
                    }
                    else
                    {
                        paramPairs.Add(parameterInfo.Name, parameterValue);
                    }

                    //设置为动态返回
                    dynamicParams = paramPairs;
                }
            }

            return dynamicParams;
        }

        //路径替换
        private string PathParse(string path, dynamic paramPairs)
        {
            if (paramPairs is IDictionary<string, object>)
            {
                string PathPrefix = "{";
                string PathSuffix = "}";
                foreach (var key in paramPairs.Keys)
                {
                    string pathvariable = PathPrefix + key + PathSuffix;

                    if (path.Contains(pathvariable))
                    {
                        path = path.Replace(pathvariable, Convert.ToString(paramPairs[key]));
                    }
                }
            }

            return path;
        }
    }


    class ProxyMethodParameter
    {
        //参数类型
        public ParameterInfo[] ParameterInfos;

        //参数值
        public object[] Arguments;

        public ProxyMethodParameter(ParameterInfo[] parameterInfos, object[] arguments)
        {
            ParameterInfos = parameterInfos;
            Arguments = arguments;
        }
    }
}