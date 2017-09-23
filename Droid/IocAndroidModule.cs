using Autofac;
using Core;

namespace Application.Droid
{
    public class IocAndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AndroidDatabaseConnectionFactory>().As<IDatabaseConnectionFactory>().AsSelf();
            builder.RegisterType<AndroidExceptionManager>().As<IPlatformExceptionManager>().AsSelf().SingleInstance();
            builder.RegisterType<Plugin.Toasts.ToastNotification>().As<Plugin.Toasts.IToastNotificator>().AsSelf();
        }
    }
}