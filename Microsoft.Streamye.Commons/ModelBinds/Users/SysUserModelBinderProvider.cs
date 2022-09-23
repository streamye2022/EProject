using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Microsoft.Streamye.Commons.ModelBinds.Users
{
    public class SysUserModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(SysUser))
            {
                return new BinderTypeModelBinder(typeof(SysUserModelBinder));
            }

            return null;
        }
    }
}