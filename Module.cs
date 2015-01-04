using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner
{
    static class Module
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AttributeChanger>().SingleInstance();
            builder.RegisterType<IconMaker>().SingleInstance();
            builder.RegisterType<TheTvDbRetriever>().As<IImageRetriever>().SingleInstance();
            builder.RegisterType<ImageCropper>().SingleInstance();
            builder.RegisterType<ImageResizer>().SingleInstance();
            builder.RegisterType<IconConverter>().SingleInstance();
            builder.RegisterType<FolderIconChanger>().SingleInstance();
            builder.RegisterType<FolderDecorator>().SingleInstance();
            builder.RegisterType<FolderUndecorator>().SingleInstance();
            return builder.Build();
        }

    }
}
